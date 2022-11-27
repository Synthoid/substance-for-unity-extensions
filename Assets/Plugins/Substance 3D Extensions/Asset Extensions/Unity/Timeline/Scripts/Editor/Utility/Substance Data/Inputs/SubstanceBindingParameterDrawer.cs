using UnityEngine;
using UnityEditor;
using Adobe.Substance;
using SOS.SubstanceExtensions.Timeline;
using SOS.SubstanceExtensions;
using System.Collections.Generic;
using System.Reflection;

namespace SOS.SubstanceExtensionsEditor.Timeline
{
    [CustomPropertyDrawer(typeof(SubstanceBindingParameter))]
    public class SubstanceBindingParameterDrawer : TrackBindingReferenceDrawer<SubstanceGraphSO>
    {
        private static readonly SubstanceParameterData[] DefaultParameters = new SubstanceParameterData[0];

        private Dictionary<int, GUIContent> graphLabels = new Dictionary<int, GUIContent>();
        private Dictionary<int, GUIContent[]> parameterLabels = new Dictionary<int, GUIContent[]>();
        private Dictionary<int, SubstanceParameterData[]> parameterMappings = new Dictionary<int, SubstanceParameterData[]>();
        private Dictionary<string, SbsInputTypeFilter> parameterFilters = new Dictionary<string, SbsInputTypeFilter>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SubstanceGraphSO substance = GetBindingCast();

            if(substance == null)
            {
                EditorGUI.HelpBox(position, "No track binding assigned. Cannot select inputs.", MessageType.Warning);
                return;
            }

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
            int substanceInstanceId = substance == null ? 0 : substance.GetInstanceID();
            string currentValue = property.FindPropertyRelative("name").stringValue;
            GUIContent[] labels = GetLabels(substance, substanceInstanceId, inputFilter);
            SubstanceParameterData[] inputs = GetParameters(substance, substanceInstanceId);

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
                property.FindPropertyRelative("name").stringValue = labels[index].tooltip;
                ResetParameterProperty(property);
            }

            SubstanceExtensionsEditorUtility.DrawPopupSearchWindow(position, label, index, labels, (int selectionIndex) =>
            {
                if(index == selectionIndex) return;

                property.FindPropertyRelative("name").stringValue = labels[selectionIndex].tooltip;

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

                property.serializedObject.ApplyModifiedProperties();
            },
            GetGraphLabel(substance, substanceInstanceId));
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


        private GUIContent[] GetLabels(SubstanceGraphSO substance, int instanceId, SbsInputTypeFilter filter)
        {
            bool success = parameterLabels.TryGetValue(instanceId, out GUIContent[] labels);

            if(!success)
            {
                System.Tuple<GUIContent[], SubstanceParameterData[]> results = SubstanceExtensionsEditorUtility.GetInputData(substance, filter);

                labels = results.Item1;

                parameterLabels.Add(instanceId, labels);
                parameterMappings.Add(instanceId, results.Item2);
            }

            return labels;
        }


        private SubstanceParameterData[] GetParameters(SubstanceGraphSO substance, int instanceId)
        {
            bool success = parameterMappings.TryGetValue(instanceId, out SubstanceParameterData[] parameters);

            return success ? parameters : DefaultParameters;
        }


        private GUIContent GetGraphLabel(SubstanceGraphSO substance, int instanceId)
        {
            bool success = graphLabels.TryGetValue(instanceId, out GUIContent graphLabel);

            if(!success)
            {
                if(substance == null) return new GUIContent(string.Format(SubstanceExtensionsEditorUtility.kInputSearchWindowTitle, SubstanceExtensionsEditorUtility.kDefaultSubstanceName));

                graphLabel = new GUIContent(string.Format(SubstanceExtensionsEditorUtility.kInputSearchWindowTitle, substance.Name));

                graphLabels.Add(instanceId, graphLabel);
            }

            return graphLabel;
        }
    }
}