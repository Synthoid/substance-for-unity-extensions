using UnityEngine;
using UnityEditor;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomEditor(typeof(SubstanceExtensionsProjectSettingsAsset))]
    public class SubstanceExtensionsProjectSettingsAssetEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty iterator = serializedObject.GetIterator();

            iterator.NextVisible(true);

            while(iterator.NextVisible(true))
            {
                EditorGUILayout.PropertyField(iterator);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}