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
        private const int kUpdateDelayFrameCount = 1;

        private static int updateFrameCount = 0;
        private static List<string> validPaths = new List<string>();

        public void OnPreprocessAsset()
        {
            //Only affect .sbsar files
            if(!assetPath.EndsWith(kSubstanceArchiveExtension, System.StringComparison.OrdinalIgnoreCase)) return;
            //If disable auto refresh is enabled, don't do anything.
            if(SubstanceExtensionsProjectSettingsAsset.Instance.disableAutoImports) return;

            SubstanceImporter importer = assetImporter as SubstanceImporter;

            if(importer == null) return;
            //If instancesCopy is null, the asset hasn't been imported yet so it should be a new .sbsar
            if(importer._instancesCopy == null) return;

            validPaths.Add(assetPath);
        }


        public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
        {
            if(validPaths.Count == 0) return;
            if(SubstanceExtensionsProjectSettingsAsset.Instance.disableAutoImports) return;

            //Destroy substance editors loaded into memory to prevent serialized object errors...
            bool editorsCulled = SubstanceExtensionsEditorUtility.CullSubstanceEditors();

            for(int i = 0; i < validPaths.Count; i++)
            {
                SubstanceFileSO sbsarFile = AssetDatabase.LoadAssetAtPath<SubstanceFileSO>(validPaths[i]);

                if(!SubstanceExtensionsEditorUtility.TryUpdateSubstanceInputs(sbsarFile))
                {
                    Debug.LogWarning(string.Format("Could not update some graph instances on substance: {0}", sbsarFile == null ? "<NULL>" : sbsarFile.name));
                }
            }

            AssetDatabase.SaveAssets();

            validPaths.Clear();

            if(editorsCulled)
            {
                updateFrameCount = kUpdateDelayFrameCount;

                EditorApplication.update += DoEditorUpdateDelay;
            }
        }


        private static void DoEditorUpdateDelay()
        {
            if(updateFrameCount > 0)
            {
                updateFrameCount--;
                return;
            }

            EditorApplication.update -= DoEditorUpdateDelay;

            Debug.Log("TEST");

            Selection.objects = new Object[0]; //Set selected objects to nothing, only if there was a SubstanceGraphSOEditor or SubstanceImporterEditor in memory.
        }
    }
}