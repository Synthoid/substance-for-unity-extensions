using UnityEngine;
using UnityEditor;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensionsEditor
{
    [CreateAssetMenu(fileName="Sbsar Inputs", menuName="SOS/Substance/Test/Input Test Asset")]
    public class SbsarInputTestAsset : ScriptableObject
    {
        [SerializeReference]
        public ISubstanceInput[] inputs = new ISubstanceInput[0];

        private static SbsarInputTestAsset instance = null;

        public static SbsarInputTestAsset Instance
        {
            get
            {
                if(instance == null)
                {
                    string[] guids = AssetDatabase.FindAssets("t:" + typeof(SbsarInputTestAsset).FullName);

                    if(guids.Length > 0)
                    {
                        instance = AssetDatabase.LoadAssetAtPath<SbsarInputTestAsset>(AssetDatabase.GUIDToAssetPath(guids[0]));
                    }
                }

                return instance;
            }
        }
    }
}