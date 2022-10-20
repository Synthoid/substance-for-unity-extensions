using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SOS.SubstanceExtensions;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(SubstanceParameter))]
    public class SubstanceParameterDrawer : GUIDReferenceDrawer<SubstanceGraphSO>
    {
        private static readonly GUIContent SearchWindowTitle = new GUIContent("Substance Inputs");
        private static readonly SubstanceParameterData[] DefaultParameters = new SubstanceParameterData[0];

        private Dictionary<string, GUIContent[]> parameterLabels = new Dictionary<string, GUIContent[]>();
        private Dictionary<string, SubstanceParameterData[]> parameterMappings = new Dictionary<string, SubstanceParameterData[]>();

        public override string AssetField { get { return "guid"; } }
        public override string ValueField { get { return "name"; } }

        protected override void DrawValueField(Rect position, SerializedProperty property, SerializedProperty assetProperty, SerializedProperty valueProperty)
        {
            if(string.IsNullOrEmpty(assetProperty.stringValue)) return;

            int index = -1;
            string graphGuid = property.FindPropertyRelative("graphGuid").stringValue;
            string assetGuid = assetProperty.stringValue;
            string currentValue = valueProperty.stringValue;
            GUIContent[] labels = GetLabels(assetGuid);
            SubstanceParameterData[] inputs = GetParameters(assetGuid);

            for(int i=0; i < labels.Length; i++)
            {
                //Get current label index, accounting for for labels with the same values across multiple graphs, ie $outputSize
                if(labels[i].tooltip == currentValue &&
                    inputs[i].graphGuid == graphGuid)
                {
                    index = i;
                    break;
                }
            }

            if(index < 0)
            {
                index = 0;
                valueProperty.stringValue = labels[index].tooltip;
                ResetParameterProperty(property);
            }

            SubstanceExtensionsEditorUtility.DrawPopupSearchWindow(position, GUIContent.none, index, labels, (int selectionIndex) =>
            {
                if(index == selectionIndex) return;

                valueProperty.stringValue = labels[selectionIndex].tooltip;

                if(selectionIndex == 0)
                {
                    ResetParameterProperty(property);
                }
                else
                {
                    property.FindPropertyRelative("graphGuid").stringValue = inputs[selectionIndex].graphGuid;
                    property.FindPropertyRelative("index").intValue = inputs[selectionIndex].index;
                    property.FindPropertyRelative("type").intValue = (int)inputs[selectionIndex].type;
                    property.FindPropertyRelative("widgetType").intValue = (int)inputs[selectionIndex].widget;
                    property.FindPropertyRelative("rangeMin").vector4Value = inputs[selectionIndex].rangeMin;
                    property.FindPropertyRelative("rangeMax").vector4Value = inputs[selectionIndex].rangeMax;
                    property.FindPropertyRelative("rangeIntMin").SetVector4IntValue(inputs[selectionIndex].rangeIntMin);
                    property.FindPropertyRelative("rangeIntMax").SetVector4IntValue(inputs[selectionIndex].rangeIntMax);
                }

                valueProperty.serializedObject.ApplyModifiedProperties();
            },
            SearchWindowTitle);
        }


        private void ResetParameterProperty(SerializedProperty property)
        {
            property.FindPropertyRelative("graphGuid").stringValue = "";
            property.FindPropertyRelative("index").intValue = 0;
            property.FindPropertyRelative("type").intValue = (int)SubstanceValueType.Float;
            property.FindPropertyRelative("widgetType").intValue = (int)SubstanceWidgetType.NoWidget;
            property.FindPropertyRelative("rangeMin").vector4Value = Vector4.zero;
            property.FindPropertyRelative("rangeMax").vector4Value = Vector4.zero;
            property.FindPropertyRelative("rangeIntMin").SetVector4IntValue(Vector4Int.zero);
            property.FindPropertyRelative("rangeIntMax").SetVector4IntValue(Vector4Int.zero);
        }


        private GUIContent[] GetLabels(string assetGuid)
        {
            parameterLabels.TryGetValue(assetGuid, out GUIContent[] labels);

            if(labels == null)
            {
                SubstanceGraphSO substance = AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GUIDToAssetPath(assetGuid));
                List<GUIContent> newLabels = new List<GUIContent>() { new GUIContent("<None>", "") };
                List<SubstanceParameterData> parameters = new List<SubstanceParameterData>() { new SubstanceParameterData() };

                if(substance != null)
                {
                    List<ISubstanceInput> inputs = substance.Input;

                    for(int j = 0; j < inputs.Count; j++)
                    {
                        if(inputs[j].IsValid)
                        {
                            int index = j;

                            GUIContent label = new GUIContent(string.Format("{0}{1} ({2}) [{3}]",
                                string.IsNullOrEmpty(inputs[index].Description.GuiGroup) ? "" : string.Format("{0}/", inputs[index].Description.GuiGroup),
                                inputs[index].Description.Label,
                                inputs[index].Description.Identifier,
                                inputs[index].Description.Type),
                                inputs[index].Description.Identifier);

                            newLabels.Add(label);
                            parameters.Add(new SubstanceParameterData(inputs[index], substance.GUID));
                        }
                    }
                }
                else
                {
                    newLabels[0].text = "None <No Substance>";
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