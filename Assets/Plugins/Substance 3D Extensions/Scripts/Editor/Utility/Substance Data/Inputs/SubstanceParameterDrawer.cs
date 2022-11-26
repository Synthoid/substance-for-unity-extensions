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
        private static readonly SubstanceParameterData[] DefaultParameters = new SubstanceParameterData[0];

        private Dictionary<string, GUIContent> graphLabels = new Dictionary<string, GUIContent>();
        private Dictionary<string, GUIContent[]> parameterLabels = new Dictionary<string, GUIContent[]>();
        private Dictionary<string, SubstanceParameterData[]> parameterMappings = new Dictionary<string, SubstanceParameterData[]>();
        private Dictionary<string, SbsInputTypeFilter> parameterFilters = new Dictionary<string, SbsInputTypeFilter>();

        public override string AssetField { get { return "guid"; } }
        public override string ValueField { get { return "name"; } }

        protected override void DrawValueField(Rect position, SerializedProperty property, SerializedProperty assetProperty, SerializedProperty valueProperty)
        {
            if(string.IsNullOrEmpty(assetProperty.stringValue)) return;

            if(!parameterFilters.TryGetValue(property.propertyPath, out SbsInputTypeFilter inputFilter))
            {
                SubstanceInputTypeFilterAttribute filterAttribute = fieldInfo.GetCustomAttribute<SubstanceInputTypeFilterAttribute>();

                //No direct filter attribute found. Check immediate parent for filter attribute.
                if(filterAttribute == null)
                {
                    SerializedProperty parentProperty = property.GetParentProperty();

                    if(parentProperty != null)
                    {
                        FieldInfo parentField = parentProperty.GetPropertyFieldInfo();

                        filterAttribute = parentField.GetCustomAttribute<SubstanceInputTypeFilterAttribute>();
                    }
                }

                inputFilter = filterAttribute != null ? filterAttribute.filter : SbsInputTypeFilter.Everything;

                parameterFilters.Add(property.propertyPath, inputFilter);
            }

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
                    property.FindPropertyRelative("valueType").intValue = (int)inputs[selectionIndex].type;
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
            property.FindPropertyRelative("valueType").intValue = (int)SubstanceValueType.Float;
            property.FindPropertyRelative("widgetType").intValue = (int)SubstanceWidgetType.NoWidget;
            property.FindPropertyRelative("rangeMin").vector4Value = Vector4.zero;
            property.FindPropertyRelative("rangeMax").vector4Value = Vector4.zero;
            property.FindPropertyRelative("rangeIntMin").SetVector4IntValue(Vector4Int.zero);
            property.FindPropertyRelative("rangeIntMax").SetVector4IntValue(Vector4Int.zero);
        }


        private GUIContent[] GetLabels(string assetGuid, SbsInputTypeFilter filter)
        {
            bool success = parameterLabels.TryGetValue(assetGuid, out GUIContent[] labels);

            if(!success)
            {
                SubstanceGraphSO substance = AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GUIDToAssetPath(assetGuid));
                System.Tuple<GUIContent[], SubstanceParameterData[]> results = SubstanceExtensionsEditorUtility.GetInputData(substance, filter);

                labels = results.Item1;

                parameterLabels.Add(assetGuid, labels);
                parameterMappings.Add(assetGuid, results.Item2);
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

                if(substance == null) return new GUIContent(string.Format(SubstanceExtensionsEditorUtility.kInputSearchWindowTitle, SubstanceExtensionsEditorUtility.kDefaultSubstanceName));

                graphLabel = new GUIContent(string.Format(SubstanceExtensionsEditorUtility.kInputSearchWindowTitle, substance.Name));

                graphLabels.Add(assetGuid, graphLabel);
            }

            return graphLabel;
        }
    }
}