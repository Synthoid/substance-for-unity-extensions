using UnityEngine;
using UnityEditor;
using SOS.SubstanceExtensions.Examples;

namespace SOS.SubstanceExtensionsEditor.Examples
{
    [CustomEditor(typeof(SetOutputSizeExample))]
    public class SetOutputSizeExampleEditor : Editor
    {
        private SerializedProperty m_Script = null;
        private SerializedProperty m_Substance = null;
        private SerializedProperty m_SetMethod = null;
        private SerializedProperty m_OutputSizeParameter = null;
        private SerializedProperty m_OutputSize = null;
        private SerializedProperty m_RawOutputSize = null;

        private void OnEnable()
        {
            m_Script = serializedObject.FindProperty("m_Script");
            m_Substance = serializedObject.FindProperty("substance");
            m_SetMethod = serializedObject.FindProperty("setMethod");
            m_OutputSizeParameter = serializedObject.FindProperty("outputSizeParameter");
            m_OutputSize = serializedObject.FindProperty("outputSize");
            m_RawOutputSize = serializedObject.FindProperty("rawOutputSize");
        }


        public override void OnInspectorGUI()
        {
            using(new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUILayout.PropertyField(m_Script);
            }

            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Substance);
            EditorGUILayout.PropertyField(m_SetMethod);

            switch((SetOutputSizeExample.OutputSizeMethod)m_SetMethod.intValue)
            {
                case SetOutputSizeExample.OutputSizeMethod.Enum:
                    EditorGUILayout.PropertyField(m_OutputSize);
                    break;
                case SetOutputSizeExample.OutputSizeMethod.Vector2Int:
                    EditorGUILayout.PropertyField(m_RawOutputSize);
                    break;
                default:
                    EditorGUILayout.PropertyField(m_OutputSizeParameter);
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}