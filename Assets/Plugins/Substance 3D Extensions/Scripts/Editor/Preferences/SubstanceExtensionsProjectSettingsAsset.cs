using UnityEngine;
using UnityEditor;

namespace SOS.SubstanceExtensionsEditor
{
    public class SubstanceExtensionsProjectSettingsAsset : ScriptableObject
    {
        private const string kEditorAssetsFolderGUID = "9a5abde39963be843a45e77531d57bb3";

        [Tooltip("If true, .sbsar files will not automatically update their graphs instances when their file is modified (ie by replacing the .sbsar file).")]
        public bool disableAutoImports = false;


        private static SubstanceExtensionsProjectSettingsAsset instance = null;


        public static SubstanceExtensionsProjectSettingsAsset Instance
        {
            get
            {
                if(instance == null)
                {
                    string[] guids = AssetDatabase.FindAssets("t:" + typeof(SubstanceExtensionsProjectSettingsAsset).FullName);

                    if(guids.Length > 0)
                    {
                        instance = AssetDatabase.LoadAssetAtPath<SubstanceExtensionsProjectSettingsAsset>(AssetDatabase.GUIDToAssetPath(guids[0]));
                    }
                    else
                    {
                        instance = CreateSettingsAssetInstance();
                    }
                }

                return instance;
            }
        }


        private static SubstanceExtensionsProjectSettingsAsset CreateSettingsAssetInstance()
        {
            SubstanceExtensionsProjectSettingsAsset newInstance = CreateInstance<SubstanceExtensionsProjectSettingsAsset>();

            newInstance.name = "Substance Extensions Project Settings";

            string path = AssetDatabase.GUIDToAssetPath(kEditorAssetsFolderGUID);

            if(string.IsNullOrEmpty(path))
            {
                path = string.Format("Assets/{0}.asset", newInstance.name);
            }
            else
            {
                path += string.Format("/{0}.asset", newInstance.name);
            }

            Debug.Log(string.Format("Creating Extensions settings asset at:\n", path));

            AssetDatabase.CreateAsset(newInstance, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(path);

            return newInstance;
        }
    }
}