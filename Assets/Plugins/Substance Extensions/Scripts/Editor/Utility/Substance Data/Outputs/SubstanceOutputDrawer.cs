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
        private static readonly SubstanceOutputData[] DefaultOutputs = new SubstanceOutputData[0];

        private Dictionary<string, GUIContent[]> outputLabels = new Dictionary<string, GUIContent[]>();
        private Dictionary<string, SubstanceOutputData[]> outputMappings = new Dictionary<string, SubstanceOutputData[]>();

        public override string AssetField { get { return "guid"; } }
        public override string ValueField { get { return "name"; } }

        protected override void DrawValueField(Rect position, SerializedProperty property, SerializedProperty assetProperty, SerializedProperty valueProperty)
        {
            if(string.IsNullOrEmpty(assetProperty.stringValue)) return;

            GUIContent[] labels = GetLabels(assetProperty.stringValue);
            int index = -1;
            string currentValue = valueProperty.stringValue;

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
                }
                else
                {
                    string assetGuid = assetProperty.stringValue;
                    SubstanceOutputData[] outputs = GetOutputs(assetGuid);

                    property.FindPropertyRelative("graphId").intValue = outputs[index].graphIndex;
                    property.FindPropertyRelative("index").intValue = outputs[index].index;
                }
            }
        }


        private GUIContent[] GetLabels(string assetGuid)
        {
            outputLabels.TryGetValue(assetGuid, out GUIContent[] labels);

            if(labels == null)
            {
                SubstanceFileSO graph = AssetDatabase.LoadAssetAtPath<SubstanceFileSO>(AssetDatabase.GUIDToAssetPath(assetGuid));
                List<GUIContent> newLabels = new List<GUIContent>() { new GUIContent("None", "") };
                List<SubstanceOutputData> parameters = new List<SubstanceOutputData>() { new SubstanceOutputData() };

                for(int i=0; i < graph.Instances.Count; i++)
                {
                    List<SubstanceOutputTexture> outputs = graph.Instances[i].Output;

                    for(int j=0; j < outputs.Count; j++)
                    {
                        int index = j;

                        GUIContent label = new GUIContent(string.Format("{0}/{1} ({2})", i.ToString("00"),
                            outputs[index].Description.Label,
                            outputs[index].Description.Channel),
                            outputs[index].Description.Identifier);

                        newLabels.Add(label);
                        parameters.Add(new SubstanceOutputData(outputs[index])); //TODO: This will break for labels with the same values, ie $outputSize
                    }
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
