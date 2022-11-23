using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using SOS.SubstanceExtensions;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(SubstanceParameter))]
    public class SubstanceParameterDrawer : GUIDReferenceDrawer<SubstanceGraphSO>
    {
        protected const string kSearchWindowTitle = "{0} Inputs";
        protected const string kDefaultSubstanceName = "<No Substance>";

        private static readonly SubstanceParameterData[] DefaultParameters = new SubstanceParameterData[0];

        private Dictionary<string, GUIContent> graphLabels = new Dictionary<string, GUIContent>();
        private Dictionary<string, GUIContent[]> parameterLabels = new Dictionary<string, GUIContent[]>();
        private Dictionary<string, SubstanceParameterData[]> parameterMappings = new Dictionary<string, SubstanceParameterData[]>();

        public override string AssetField { get { return "guid"; } }
        public override string ValueField { get { return "name"; } }

        protected override void DrawValueField(Rect position, SerializedProperty property, SerializedProperty assetProperty, SerializedProperty valueProperty)
        {
            if(string.IsNullOrEmpty(assetProperty.stringValue)) return;

            SubstanceInputTypeFilterAttribute filterAttribute = fieldInfo.GetCustomAttribute<SubstanceInputTypeFilterAttribute>();
            SbsInputTypeFilter inputFilter = filterAttribute != null ? filterAttribute.filter : SbsInputTypeFilter.Everything;

            int index = -1;
            string assetGuid = assetProperty.stringValue;
            string currentValue = valueProperty.stringValue;
            GUIContent[] labels = GetLabels(assetGuid, inputFilter);
            SubstanceParameterData[] inputs = GetParameters(assetGuid);

            for(int i=0; i < labels.Length; i++)
            {
                //Get current label index
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
            GetGraphLabel(assetGuid));
        }


        private void ResetParameterProperty(SerializedProperty property)
        {
            property.FindPropertyRelative("index").intValue = 0;
            property.FindPropertyRelative("type").intValue = (int)SubstanceValueType.Float;
            property.FindPropertyRelative("widgetType").intValue = (int)SubstanceWidgetType.NoWidget;
            property.FindPropertyRelative("rangeMin").vector4Value = Vector4.zero;
            property.FindPropertyRelative("rangeMax").vector4Value = Vector4.zero;
            property.FindPropertyRelative("rangeIntMin").SetVector4IntValue(Vector4Int.zero);
            property.FindPropertyRelative("rangeIntMax").SetVector4IntValue(Vector4Int.zero);
        }


        private GUIContent[] GetLabels(string assetGuid, SbsInputTypeFilter filter)
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
                        if(!inputs[j].IsValid) continue; //Skip invalid inputs
                        if((filter & inputs[j].ValueType.ToFilter()) == 0) continue; //Skip inputs not included in the filter.

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


        private GUIContent GetGraphLabel(string assetGuid)
        {
            bool success = graphLabels.TryGetValue(assetGuid, out GUIContent graphLabel);

            if(!success)
            {
                SubstanceGraphSO substance = AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GUIDToAssetPath(assetGuid));

                if(substance == null) return new GUIContent(string.Format(kSearchWindowTitle, kDefaultSubstanceName));

                graphLabel = new GUIContent(string.Format(kSearchWindowTitle, substance.Name));

                graphLabels.Add(assetGuid, graphLabel);
            }

            return graphLabel;
        }
    }
}