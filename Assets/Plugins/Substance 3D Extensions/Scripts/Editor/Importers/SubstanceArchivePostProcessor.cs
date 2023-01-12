using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Adobe.Substance;
using Adobe.Substance.Input;
using Adobe.Substance.Input.Description;
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

            Debug.Log(assetPath);

            Debug.Log("Asset is .sbsar!");
            //Ignore .sbsar files that haven't already been imported.
            //if(string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(assetPath, AssetPathToGUIDOptions.OnlyExistingAssets))) return;

            SubstanceImporter importer = assetImporter as SubstanceImporter;

            if(importer == null) return;

            /*Object[] assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);

            for(int i=0; i < assets.Length; i++)
            {
                Debug.Log(assets[i].GetType().FullName);
            }

            Debug.Log($"{assets.Length}: {AssetDatabase.LoadMainAssetAtPath(assetPath) is SubstanceFileSO}");

            if(AssetDatabase.LoadAssetAtPath<SubstanceFileSO>(assetPath) == null) return;*/

            Debug.Log("Asset is NOT a new .sbsar!");

            validPaths.Add(assetPath);

            /*string temp = "Assets/Tests/Scenes/RuntimeTests";

            Debug.Log(AssetDatabase.LoadMainAssetAtPath(temp) == null);

            Debug.Log(importer._fileAsset == null);
            Debug.Log(importer._fileAsset.Instances == null);

            SubstanceFileRawData rawData = importer._fileAsset.Instances[0].RawData;
            byte[] fileBytes = File.ReadAllBytes(assetPath);

            Debug.Log(rawData.FileContent == fileBytes);*/
            //TODO: After checking that it's an .sbsar file, check that an asset already exists at the path. If not, do nothing...
            //If it does exist, update the raw data scriptable object's bytes and get the list of current inputs, as well as a list of new inputs.
            //Then set the new input values to match existing old input values before assigning the new inputs to the graphs...
        }


        public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
        {
            if(validPaths.Count == 0) return;

            for(int i = 0; i < validPaths.Count; i++)
            {
                SubstanceFileSO sbsarFile = AssetDatabase.LoadAssetAtPath<SubstanceFileSO>(validPaths[i]);

                TryUpdateSubstanceInputs(sbsarFile);
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
            //TODO: Update all graphs, not just the first instance...
            SubstanceGraphSO graph = substanceFile.Instances[0];
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
            }

            //TODO: When updating graph instance inputs, remember multi graph support and setting the assets dirty
            //TODO: Have to iterate through the first graph's inputs to see if any input differest at any point (including description, min/max values, widgets, etc)
            //If so, all graph instances need to be updated
            //Then iterate through each graph's ipnuts and map existing values onto new input values
            //Then replace previous graph inputs with new list
            //Then tell the editor engine to render and update assets. Somehow...


            nativeGraph.Dispose();

            return false;
        }



    }
}