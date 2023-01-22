using UnityEngine;
using UnityEditor;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Contains settings for susbtance extension functionality in the project.
    /// </summary>
    public class SubstanceExtensionsProjectSettingsAsset : ScriptableObject
    {
        private const string kEditorAssetsFolderGUID = "9a5abde39963be843a45e77531d57bb3";

        [Tooltip("If true, .sbsar files will not automatically update their graph instances when the file is modified (ie by replacing the .sbsar file).")]
        public bool disableAutoUpdates = false;
        [Indent, Tooltip("If true, delete unused graph output textures during .sbsar updates. If false, any outputs that are removed during an asset update will not delete their texture assets from the project.")]
        public bool deleteUnusedTextures = true;


        private static SubstanceExtensionsProjectSettingsAsset instance = null;

        /// <summary>
        /// Instance for the asset storing settings in the project. If one does not exist, this will create one.
        /// </summary>
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