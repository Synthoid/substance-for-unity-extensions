using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SOS.SubstanceExtensions;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(SubstanceOutput))]
    public class SubstanceOutputDrawer : GUIDReferenceDrawer<SubstanceFileSO>
    {
        private static readonly GUIContent SearchWindowTitle = new GUIContent("Substance Outputs");
        private static readonly SubstanceOutputData[] DefaultOutputs = new SubstanceOutputData[0];

        private Dictionary<string, GUIContent[]> outputLabels = new Dictionary<string, GUIContent[]>();
        private Dictionary<string, SubstanceOutputData[]> outputMappings = new Dictionary<string, SubstanceOutputData[]>();

        public override string AssetField { get { return "guid"; } }
        public override string ValueField { get { return "name"; } }

        protected override void DrawValueField(Rect position, SerializedProperty property, SerializedProperty assetProperty, SerializedProperty valueProperty)
        {
            if(string.IsNullOrEmpty(assetProperty.stringValue)) return;

            int index = -1;
            int graphId = property.FindPropertyRelative("graphId").intValue;
            string assetGuid = assetProperty.stringValue;
            string currentValue = valueProperty.stringValue;
            GUIContent[] labels = GetLabels(assetGuid);
            SubstanceOutputData[] outputs = GetOutputs(assetGuid);

            for(int i = 0; i < labels.Length; i++)
            {
                if(labels[i].tooltip == currentValue && outputs[i].graphIndex == graphId)
                {
                    index = i;
                    break;
                }
            }

            if(index < 0)
            {
                index = 0;
                valueProperty.stringValue = labels[index].tooltip;
                ResetOutputProperty(property);
            }

            SubstanceExtensionsEditorUtility.DrawPopupSearchWindow(position, index, labels, (int selectionIndex) =>
            {
                if(index == selectionIndex) return;

                valueProperty.stringValue = labels[selectionIndex].tooltip;

                if(selectionIndex == 0)
                {
                    ResetOutputProperty(property);
                }
                else
                {
                    string assetGuid = assetProperty.stringValue;
                    SubstanceOutputData[] outputs = GetOutputs(assetGuid);

                    property.FindPropertyRelative("graphId").intValue = outputs[selectionIndex].graphIndex;
                    property.FindPropertyRelative("index").intValue = outputs[selectionIndex].index;
                }

                property.serializedObject.ApplyModifiedProperties();
            },
            SearchWindowTitle);

            /*EditorGUI.BeginChangeCheck();
            index = EditorGUI.Popup(position, GUIContent.none, index, labels);
            if(EditorGUI.EndChangeCheck())
            {
                valueProperty.stringValue = labels[index].tooltip;

                if(index == 0)
                {
                    property.FindPropertyRelative("graphId").intValue = 0;
                    property.FindPropertyRelative("index").intValue = 0;
                }
                else
                {
                    string assetGuid = assetProperty.stringValue;
                    SubstanceOutputData[] outputs = GetOutputs(assetGuid);

                    property.FindPropertyRelative("graphId").intValue = outputs[index].graphIndex;
                    property.FindPropertyRelative("index").intValue = outputs[index].index;
                }
            }*/
        }


        private void ResetOutputProperty(SerializedProperty property)
        {
            property.FindPropertyRelative("graphId").intValue = 0;
            property.FindPropertyRelative("index").intValue = 0;
        }


        private GUIContent[] GetLabels(string assetGuid)
        {
            outputLabels.TryGetValue(assetGuid, out GUIContent[] labels);

            if(labels == null)
            {
                SubstanceFileSO substance = AssetDatabase.LoadAssetAtPath<SubstanceFileSO>(AssetDatabase.GUIDToAssetPath(assetGuid));
                List<GUIContent> newLabels = new List<GUIContent>() { new GUIContent("None", "") };
                List<SubstanceOutputData> parameters = new List<SubstanceOutputData>() { new SubstanceOutputData() };

                if(substance != null)
                {
                    for(int i = 0; i < substance.Instances.Count; i++)
                    {
                        List<SubstanceOutputTexture> outputs = substance.Instances[i].Output;

                        for(int j = 0; j < outputs.Count; j++)
                        {
                            int index = j;

                            GUIContent label = new GUIContent(string.Format("{0}/{1} ({2})",
                                substance.Instances[i].Name,
                                outputs[index].Description.Label,
                                outputs[index].Description.Channel),
                                outputs[index].Description.Identifier);

                            newLabels.Add(label);
                            parameters.Add(new SubstanceOutputData(outputs[index])); //TODO: This will break for labels with the same values, ie $outputSize
                        }
                    }
                }
                else
                {
                    newLabels[0].text = "None <No Substance>";
                }

                labels = newLabels.ToArray();

                outputLabels.Add(assetGuid, labels);
                outputMappings.Add(assetGuid, parameters.ToArray());
            }

            return labels;
        }




        private SubstanceOutputData[] GetOutputs(string assetGuid)
        {
            bool success = outputMappings.TryGetValue(assetGuid, out SubstanceOutputData[] outputs);

            return success ? outputs : DefaultOutputs;
        }
    }
}
