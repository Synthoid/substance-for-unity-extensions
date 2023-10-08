using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SOS.SubstanceExtensions;
using Adobe.Substance;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(SubstanceOutput))]
    public class SubstanceOutputDrawer : GUIDReferenceDrawer<SubstanceGraphSO>
    {
        protected const string kSearchWindowTitle = "{0} Outputs";
        protected const string kDefaultSubstanceName = "<No Substance>";

        private static readonly SubstanceOutputData[] DefaultOutputs = new SubstanceOutputData[0];

        private Dictionary<string, GUIContent> graphLabels = new Dictionary<string, GUIContent>();
        private Dictionary<string, GUIContent[]> outputLabels = new Dictionary<string, GUIContent[]>();
        private Dictionary<string, SubstanceOutputData[]> outputMappings = new Dictionary<string, SubstanceOutputData[]>();

        public override string AssetField { get { return "guid"; } }
        public override string ValueField { get { return "name"; } }

        protected override void DrawValueField(Rect position, SerializedProperty property, SerializedProperty assetProperty, SerializedProperty valueProperty)
        {
            if(string.IsNullOrEmpty(assetProperty.stringValue)) return;

            int index = -1;
            string assetGuid = assetProperty.stringValue;
            string currentValue = valueProperty.stringValue;
            GUIContent[] labels = GetLabels(assetGuid);
            SubstanceOutputData[] outputs = GetOutputs(assetGuid);

            for(int i = 0; i < labels.Length; i++)
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
                ResetOutputProperty(property);
            }

            SubstanceExtensionsEditorUtility.DrawPopupSearchWindow(position, GUIContent.none, index, labels, (int selectionIndex) =>
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

                    property.FindPropertyRelative("index").intValue = outputs[selectionIndex].index;
                }

                property.serializedObject.ApplyModifiedProperties();
            },
            GetGraphLabel(assetGuid));
        }


        private void ResetOutputProperty(SerializedProperty property)
        {
            property.FindPropertyRelative("index").intValue = 0;
        }


        private GUIContent[] GetLabels(string assetGuid)
        {
            outputLabels.TryGetValue(assetGuid, out GUIContent[] labels);

            if(labels == null)
            {
                SubstanceGraphSO substance = AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GUIDToAssetPath(assetGuid));
                List<GUIContent> newLabels = new List<GUIContent>() { new GUIContent("<None>", "") };
                List<SubstanceOutputData> parameters = new List<SubstanceOutputData>() { new SubstanceOutputData() };

                if(substance != null)
                {
                    List<SubstanceOutputTexture> outputs = substance.Output;

                    for(int j = 0; j < outputs.Count; j++)
                    {
                        int index = j;

                        GUIContent label = new GUIContent(string.Format("{0} ({1}) [{2}]",
                            outputs[index].Description.Label,
                            outputs[index].Description.Identifier,
                            outputs[index].Description.Channel),
                            outputs[index].Description.Identifier);

                        newLabels.Add(label);
                        parameters.Add(new SubstanceOutputData(outputs[index]));
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