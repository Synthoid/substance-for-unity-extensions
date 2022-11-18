using UnityEngine;
using UnityEditor;

namespace SOS.SubstanceExtensions.Tests
{
    [CustomEditor(typeof(SubstanceGraphTestAsset))]
    public class SubstanceGraphTestAssetEditor : Editor
    {
        private SerializedProperty m_Script = null;
        private SerializedProperty m_Substance = null;
        //Defaults
        private SerializedProperty m_DefaultStringValue = null;
        private SerializedProperty m_DefaultBoolValue = null;
        private SerializedProperty m_DefaultEnumValue = null;
        private SerializedProperty m_DefaultEnumCastValue = null;
        private SerializedProperty m_DefaultIntValue = null;
        private SerializedProperty m_DefaultInt2Value = null;
        private SerializedProperty m_DefaultInt3Value = null;
        private SerializedProperty m_DefaultInt4Value = null;
        private SerializedProperty m_DefaultFloatValue = null;
        private SerializedProperty m_DefaultFloat2Value = null;
        private SerializedProperty m_DefaultFloat3Value = null;
        private SerializedProperty m_DefaultFloat4Value = null;
        private SerializedProperty m_DefaultTextureValue = null;
        private SerializedProperty m_DefaultTextureNullValue = null;
        private SerializedProperty m_DefaultOutputSizeValue = null;
        private SerializedProperty m_DefaultRandomSeedValue = null;
        //Set
        private SerializedProperty m_SetStringValue = null;
        private SerializedProperty m_SetBoolValue = null;
        private SerializedProperty m_SetEnumValue = null;
        private SerializedProperty m_SetEnumCastValue = null;
        private SerializedProperty m_SetIntValue = null;
        private SerializedProperty m_SetInt2Value = null;
        private SerializedProperty m_SetInt3Value = null;
        private SerializedProperty m_SetInt4Value = null;
        private SerializedProperty m_SetFloatValue = null;
        private SerializedProperty m_SetFloat2Value = null;
        private SerializedProperty m_SetFloat3Value = null;
        private SerializedProperty m_SetFloat4Value = null;
        private SerializedProperty m_SetTextureValue = null;
        private SerializedProperty m_SetTextureNullValue = null;
        private SerializedProperty m_SetOutputSizeValue = null;
        private SerializedProperty m_SetRandomSeedValue = null;
        //Outputs
        private SerializedProperty m_OutputMap = null;
        private SerializedProperty m_OutputValue = null;

        private readonly GUIContent DefaultLabel = new GUIContent("Defaults", "Default values for unit tests of various get and set methods.");
        private readonly GUIContent SetLabel = new GUIContent("Set", "Values for unit tests of various set methods.");
        private readonly GUIContent OutputsLabel = new GUIContent("Outputs", "Values for unit tests of various map output related methods.");

        private void OnEnable()
        {
            m_Script = serializedObject.FindProperty("m_Script");
            m_Substance = serializedObject.FindProperty("substance");
            //Default
            m_DefaultStringValue = serializedObject.FindProperty("defaultStringValue");
            m_DefaultBoolValue = serializedObject.FindProperty("defaultBoolValue");
            m_DefaultEnumValue = serializedObject.FindProperty("defaultEnumValue");
            m_DefaultEnumCastValue = serializedObject.FindProperty("defaultEnumCastValue");
            m_DefaultIntValue = serializedObject.FindProperty("defaultIntValue");
            m_DefaultInt2Value = serializedObject.FindProperty("defaultInt2Value");
            m_DefaultInt3Value = serializedObject.FindProperty("defaultInt3Value");
            m_DefaultInt4Value = serializedObject.FindProperty("defaultInt4Value");
            m_DefaultFloatValue = serializedObject.FindProperty("defaultFloatValue");
            m_DefaultFloat2Value = serializedObject.FindProperty("defaultFloat2Value");
            m_DefaultFloat3Value = serializedObject.FindProperty("defaultFloat3Value");
            m_DefaultFloat4Value = serializedObject.FindProperty("defaultFloat4Value");
            m_DefaultTextureValue = serializedObject.FindProperty("defaultTextureValue");
            m_DefaultTextureNullValue = serializedObject.FindProperty("defaultTextureNullValue");
            m_DefaultOutputSizeValue = serializedObject.FindProperty("defaultOutputSizeValue");
            m_DefaultRandomSeedValue = serializedObject.FindProperty("defaultRandomSeedValue");
            //Set
            m_SetStringValue = serializedObject.FindProperty("setStringValue");
            m_SetBoolValue = serializedObject.FindProperty("setBoolValue");
            m_SetEnumValue = serializedObject.FindProperty("setEnumValue");
            m_SetEnumCastValue = serializedObject.FindProperty("setEnumCastValue");
            m_SetIntValue = serializedObject.FindProperty("setIntValue");
            m_SetInt2Value = serializedObject.FindProperty("setInt2Value");
            m_SetInt3Value = serializedObject.FindProperty("setInt3Value");
            m_SetInt4Value = serializedObject.FindProperty("setInt4Value");
            m_SetFloatValue = serializedObject.FindProperty("setFloatValue");
            m_SetFloat2Value = serializedObject.FindProperty("setFloat2Value");
            m_SetFloat3Value = serializedObject.FindProperty("setFloat3Value");
            m_SetFloat4Value = serializedObject.FindProperty("setFloat4Value");
            m_SetTextureValue = serializedObject.FindProperty("setTextureValue");
            m_SetTextureNullValue = serializedObject.FindProperty("setTextureNullValue");
            m_SetOutputSizeValue = serializedObject.FindProperty("setOutputSizeValue");
            m_SetRandomSeedValue = serializedObject.FindProperty("setRandomSeedValue");
            //Outputs
            m_OutputMap = serializedObject.FindProperty("outputMap");
            m_OutputValue = serializedObject.FindProperty("outputValue");
        }


        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(m_Script);
            EditorGUI.EndDisabledGroup();

            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Substance);

            DrawDefaultSection();
            DrawSetSection();
            DrawOutputSection();

            serializedObject.ApplyModifiedProperties();
        }


        private void DrawDefaultSection()
        {
            m_DefaultEnumCastValue.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(m_DefaultEnumCastValue.isExpanded, DefaultLabel);
            EditorGUILayout.EndFoldoutHeaderGroup();

            if(!m_DefaultEnumCastValue.isExpanded) return;

            EditorGUILayout.PropertyField(m_DefaultStringValue);
            EditorGUILayout.PropertyField(m_DefaultBoolValue);
            EditorGUILayout.PropertyField(m_DefaultEnumValue);
            EditorGUILayout.PropertyField(m_DefaultEnumCastValue);
            EditorGUILayout.PropertyField(m_DefaultIntValue);
            EditorGUILayout.PropertyField(m_DefaultInt2Value);
            EditorGUILayout.PropertyField(m_DefaultInt3Value);
            EditorGUILayout.PropertyField(m_DefaultInt4Value);
            EditorGUILayout.PropertyField(m_DefaultFloatValue);
            EditorGUILayout.PropertyField(m_DefaultFloat2Value);
            EditorGUILayout.PropertyField(m_DefaultFloat3Value);
            EditorGUILayout.PropertyField(m_DefaultFloat4Value);
            EditorGUILayout.PropertyField(m_DefaultTextureValue);
            EditorGUILayout.PropertyField(m_DefaultTextureNullValue);
            EditorGUILayout.PropertyField(m_DefaultOutputSizeValue);
            EditorGUILayout.PropertyField(m_DefaultRandomSeedValue);

            EditorGUILayout.Space();
        }


        private void DrawSetSection()
        {
            m_SetEnumCastValue.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(m_SetEnumCastValue.isExpanded, SetLabel);
            EditorGUILayout.EndFoldoutHeaderGroup();

            if(!m_SetEnumCastValue.isExpanded) return;

            EditorGUILayout.PropertyField(m_SetStringValue);
            EditorGUILayout.PropertyField(m_SetBoolValue);
            EditorGUILayout.PropertyField(m_SetEnumValue);
            EditorGUILayout.PropertyField(m_SetEnumCastValue);
            EditorGUILayout.PropertyField(m_SetIntValue);
            EditorGUILayout.PropertyField(m_SetInt2Value);
            EditorGUILayout.PropertyField(m_SetInt3Value);
            EditorGUILayout.PropertyField(m_SetInt4Value);
            EditorGUILayout.PropertyField(m_SetFloatValue);
            EditorGUILayout.PropertyField(m_SetFloat2Value);
            EditorGUILayout.PropertyField(m_SetFloat3Value);
            EditorGUILayout.PropertyField(m_SetFloat4Value);
            EditorGUILayout.PropertyField(m_SetTextureValue);
            EditorGUILayout.PropertyField(m_SetTextureNullValue);
            EditorGUILayout.PropertyField(m_SetOutputSizeValue);
            EditorGUILayout.PropertyField(m_SetRandomSeedValue);

            EditorGUILayout.Space();
        }


        private void DrawOutputSection()
        {
            m_Substance.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(m_Substance.isExpanded, OutputsLabel);
            EditorGUILayout.EndFoldoutHeaderGroup();

            if(!m_Substance.isExpanded) return;

            EditorGUILayout.PropertyField(m_OutputMap);
            EditorGUILayout.PropertyField(m_OutputValue);

            EditorGUILayout.Space();
        }
    }
}