using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;
using Adobe.Substance.Input.Description;
using Adobe.SubstanceEditor;
using Adobe.SubstanceEditor.Importer;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensionsEditor
{
    public class SubstanceArchivePostProcessor : AssetPostprocessor
    {
        private const string kSubstanceArchiveExtension = ".sbsar";

        private static List<string> validPaths = new List<string>();

        public void OnPreprocessAsset()
        {
            //Only affect .sbsar files
            if(!assetPath.EndsWith(kSubstanceArchiveExtension, System.StringComparison.OrdinalIgnoreCase)) return;
            //If disable auto refresh is enabled, don't do anything.
            if(SubstanceExtensionsProjectSettingsAsset.Instance.disableAutoImports) return;

            Debug.Log(assetPath);

            Debug.Log("Asset is .sbsar!");

            SubstanceImporter importer = assetImporter as SubstanceImporter;

            if(importer == null) return;

            Debug.Log("Asset is NOT a new .sbsar!");

            validPaths.Add(assetPath);
        }


        public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
        {
            if(validPaths.Count == 0) return;
            if(SubstanceExtensionsProjectSettingsAsset.Instance.disableAutoImports) return;

            for(int i = 0; i < validPaths.Count; i++)
            {
                SubstanceFileSO sbsarFile = AssetDatabase.LoadAssetAtPath<SubstanceFileSO>(validPaths[i]);

                Debug.Log($"Post process {sbsarFile.name}\n{validPaths[i]}");

                if(!TryUpdateSubstanceInputs(sbsarFile))
                {
                    Debug.LogWarning(string.Format("Could not update some graph instances on substance: {0}", sbsarFile == null ? "<NULL>" : sbsarFile.name));
                }
                //Raw data is updated by the SubstanceImporter. Need to manually compare inputs...
                /*SubstanceFileRawData rawData = AssetDatabase.LoadAssetAtPath<SubstanceFileSO>(validPaths[i]).Instances[0].RawData;

                if(rawData == null) continue;

                byte[] fileBytes = File.ReadAllBytes(validPaths[i]);

                Debug.Log($"{fileBytes.Length} | {rawData.FileContent.Length}");

                if(rawData.FileContent.SequenceEqual(fileBytes))
                {
                    Debug.Log($"{validPaths[i]}\nBytes are the same!");
                    continue;
                }
                Debug.Log($"{validPaths[i]}\n{rawData.FileContent.SequenceEqual(fileBytes)}");*/
            }

            validPaths.Clear();
        }


        private static bool TryUpdateSubstanceInputs(SubstanceFileSO substanceFile)
        {
            if(substanceFile == null) return false;

            //Object previous = Selection.activeObject;

            //Selection.activeObject = null;
            Selection.objects = new Object[0];

            bool success = true;

            //Destroy existing SubstanceGraphSOEditor objects...
            SubstanceGraphSOEditor[] graphEditors = Resources.FindObjectsOfTypeAll<SubstanceGraphSOEditor>();

            for(int i=0; i < graphEditors.Length; i++)
            {
                Object.DestroyImmediate(graphEditors[i]);
            }

            //Destroy existing SubstanceImporterEditor objects...
            Object[] importerEditors = Resources.FindObjectsOfTypeAll(SubstanceReflectionEditorUtility.ImporterEditorType);

            for(int i=0; i < importerEditors.Length; i++)
            {
                Object.DestroyImmediate(importerEditors[i]);
            }

            //Update all graph instances associated with the file...
            SubstanceNativeGraph nativeGraph = null;

            for(int i=0; i < substanceFile.Instances.Count; i++)
            {
                nativeGraph = Engine.OpenFile(substanceFile.Instances[i].RawData.FileContent, substanceFile.Instances[i].Index);

                List<ISubstanceInput> inputs = new List<ISubstanceInput>();

                int count = nativeGraph.GetInputs(inputs);

                System.Text.StringBuilder expectingOutput = new System.Text.StringBuilder($"Expecting: {count}\n");

                for(int j=0; j < count; j++)
                {
                    expectingOutput.AppendLine($"{j}: {inputs[j].Description.Identifier}");
                }

                Debug.Log(expectingOutput.ToString());

                SubstanceInputExtensions.UpdateInputList(substanceFile.Instances[i].Input, inputs);

                //substanceFile.Instances[i].SetInputValues(inputs);
                substanceFile.Instances[i].Input = new List<ISubstanceInput>(inputs);
                //TODO: Update preset file string...

                EditorUtility.SetDirty(substanceFile.Instances[i]);

                nativeGraph.Dispose();
            }

            /*SubstanceGraphSO graph = substanceFile.Instances[0];
            SubstanceNativeGraph nativeGraph = Engine.OpenFile(graph.RawData.FileContent, graph.Index);

            int inputCount = nativeGraph.GetInputCount();
            List<ISubstanceInput> newInputs = new List<ISubstanceInput>(inputCount);
            
            for(int i=0; i < inputCount; i++)
            {
                SubstanceInputBase input = nativeGraph.GetInputObject(i);
                newInputs.Add(input);
            }

            SbsarInputTestAsset testAsset = SbsarInputTestAsset.Instance;

            if(testAsset != null)
            {
                //Hard set
                //testAsset.inputs = newInputs.ToArray();

                //Update
                int count = SubstanceInputExtensions.UpdateInputList(testAsset.inputs, newInputs);

                Debug.Log($"Updated {count}/{testAsset.inputs.Length}");
                testAsset.inputs = newInputs.ToArray();
            }*/

            //TODO: When updating graph instance inputs, remember multi graph support and setting the assets dirty
            //TODO: Have to iterate through the first graph's inputs to see if any input differest at any point (including description, min/max values, widgets, etc)
            //If so, all graph instances need to be updated
            //Then iterate through each graph's ipnuts and map existing values onto new input values
            //Then replace previous graph inputs with new list
            //Then tell the editor engine to render and update assets. Somehow...

            //Selection.activeObject = previous;

            return success;
        }



    }
}