using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions.Tests
{
    /// <summary>
    /// Base class for scriptable objects used to hold unit test values.
    /// </summary>
    public abstract class UnitTestAsset : ScriptableObject
    {
        protected static Dictionary<string, UnitTestAsset> TestAssets = new Dictionary<string, UnitTestAsset>();

        public static T GetTestAsset<T>() where T : UnitTestAsset
        {
            string typeName = typeof(T).FullName;
            bool success = TestAssets.TryGetValue(typeName, out UnitTestAsset asset);

            if(!success)
            {
                string[] guids = AssetDatabase.FindAssets("t:" + typeName);

                if(guids.Length > 0)
                {
                    asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[0]));

                    TestAssets.Add(typeName, asset);
                }
            }

            return (T)asset;
        }
    }
}