using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SOS.SubstanceExtensions;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(SubstanceParameter))]
    public class SubstanceParameterDrawer : GUIDReferenceDrawer<SubstanceMaterialInstanceSO>
    {
        private static readonly SubstanceParameterData[] DefaultParameters = new SubstanceParameterData[0];

        private Dictionary<string, GUIContent[]> parameterLabels = new Dictionary<string, GUIContent[]>();
        private Dictionary<string, SubstanceParameterData[]> parameterMappings = new Dictionary<string, SubstanceParameterData[]>();

        public override string AssetField { get { return "guid"; } }
        public override string ValueField { get { return "name"; } }

        protected override void DrawValueField(Rect position, SerializedProperty property, SerializedProperty assetProperty, SerializedProperty valueProperty)
        {
            if(string.IsNullOrEmpty(assetProperty.stringValue)) return;

            GUIContent[] labels = GetLabels(assetProperty.stringValue);
            int index = -1;
            string currentValue = valueProperty.stringValue;

            for(int i=0; i < labels.Length; i++)
            {
                if(labels[i].tooltip == currentValue)
                {
                    index = i;
                    break;
                }
            }

            if(index < 0)
            {
                index = 0;
                valueProperty.stringValue = labels[index].tooltip;
            }

            EditorGUI.BeginChangeCheck();
            index = EditorGUI.Popup(position, GUIContent.none, index, labels);
            if(EditorGUI.EndChangeCheck())
            {
                valueProperty.stringValue = labels[index].tooltip;

                if(index == 0)
                {
                    property.FindPropertyRelative("graphId").intValue = 0;
                    property.FindPropertyRelative("index").intValue = 0;
                    property.FindPropertyRelative("type").intValue = (int)SubstanceValueType.Float;
                    property.FindPropertyRelative("widgetType").intValue = (int)SubstanceWidgetType.NoWidget;
                    property.FindPropertyRelative("rangeMin").vector4Value = Vector4.zero;
                    property.FindPropertyRelative("rangeMax").vector4Value = Vector4.zero;
                    property.FindPropertyRelative("rangeIntMin").SetVector4IntValue(Vector4Int.zero);
                    property.FindPropertyRelative("rangeIntMax").SetVector4IntValue(Vector4Int.zero);
                }
                else
                {
                    string assetGuid = assetProperty.stringValue;
                    SubstanceParameterData[] inputs = GetParameters(assetGuid);

                    property.FindPropertyRelative("graphId").intValue = inputs[index].graphIndex;
                    property.FindPropertyRelative("index").intValue = inputs[index].index;
                    property.FindPropertyRelative("type").intValue = (int)inputs[index].type;
                    property.FindPropertyRelative("widgetType").intValue = (int)inputs[index].widget;
                    property.FindPropertyRelative("rangeMin").vector4Value = inputs[index].rangeMin;
                    property.FindPropertyRelative("rangeMax").vector4Value = inputs[index].rangeMax;
                    property.FindPropertyRelative("rangeIntMin").SetVector4IntValue(inputs[index].rangeIntMin);
                    property.FindPropertyRelative("rangeIntMax").SetVector4IntValue(inputs[index].rangeIntMax);
                }
            }
        }


        private GUIContent[] GetLabels(string assetGuid)
        {
            parameterLabels.TryGetValue(assetGuid, out GUIContent[] labels);

            if(labels == null)
            {
                SubstanceMaterialInstanceSO graph = AssetDatabase.LoadAssetAtPath<SubstanceMaterialInstanceSO>(AssetDatabase.GUIDToAssetPath(assetGuid));
                List<GUIContent> newLabels = new List<GUIContent>() { new GUIContent("None", "") };
                List<SubstanceParameterData> parameters = new List<SubstanceParameterData>() { new SubstanceParameterData() };

                for(int i=0; i < graph.Graphs.Count; i++)
                {
                    List<ISubstanceInput> inputs = graph.Graphs[i].Input;

                    for(int j=0; j < inputs.Count; j++)
                    {
                        if(inputs[j].IsValid)
                        {
                            int index = j;

                            GUIContent label = new GUIContent(string.Format("{0}{1}/{2} ({3})", i.ToString("00"),
                                string.IsNullOrEmpty(inputs[index].Description.GuiGroup) ? "" : string.Format("/{0}", inputs[index].Description.GuiGroup),
                                inputs[index].Description.Label,
                                inputs[index].Description.Type),
                                inputs[index].Description.Identifier);
                            
                            newLabels.Add(label);
                            parameters.Add(new SubstanceParameterData(inputs[index])); //TODO: This will break for labels with the same values, ie $outputSize
                            //TODO: Need a way of associating labels with parameters that accounts for possible duplicate names and indexes across sub-graphs...
                        }
                    }
                }


                labels = newLabels.ToArray();

                parameterLabels.Add(assetGuid, labels);
                parameterMappings.Add(assetGuid, parameters.ToArray());
            }

            return labels;
        }


        private SubstanceParameterData[] GetParameters(string assetGuid)
        {
            bool success = parameterMappings.TryGetValue(assetGuid, out SubstanceParameterData[] parameters);

            return success ? parameters : DefaultParameters;
        }
    }
}