using UnityEngine;
using UnityEditor;
using SOS.SubstanceExtensions;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensionsEditor
{
    public static class SubstanceExtensionsEditorUtility
    {
        #region Temp

        public class Labels
        {
            public class Controls
            {
                public static readonly GUIContent LinkedLabel = new GUIContent(EditorGUIUtility.IconContent("d_Linked").image);
                public static readonly GUIContent UnlinkedLabel = new GUIContent(EditorGUIUtility.IconContent("d_Unlinked").image);
            }
        }

        #endregion

        #region SerializedProperty

        public static Vector4Int GetVector4IntValue(this SerializedProperty vectorProperty)
        {
            return new Vector4Int(vectorProperty.FindPropertyRelative("x").intValue,
                vectorProperty.FindPropertyRelative("y").intValue,
                vectorProperty.FindPropertyRelative("z").intValue,
                vectorProperty.FindPropertyRelative("w").intValue);
        }

        public static void SetVector4IntValue(this SerializedProperty vectorProperty, Vector4Int vector)
        {
            vectorProperty.FindPropertyRelative("x").intValue = vector.x;
            vectorProperty.FindPropertyRelative("y").intValue = vector.y;
            vectorProperty.FindPropertyRelative("z").intValue = vector.z;
            vectorProperty.FindPropertyRelative("w").intValue = vector.w;
        }

        /// <summary>
        /// Get the <see cref="SubstanceMaterialInstanceSO"/> referenced by a <see cref="SubstanceParameter"/> field.
        /// </summary>
        /// <param name="property"><see cref="SubstanceParameter"/> property to get the target reference for.</param>
        public static SubstanceMaterialInstanceSO GetGUIDReferenceSubstance(this SerializedProperty property)
        {
            string guid = property.FindPropertyRelative("guid").stringValue;

            if(string.IsNullOrEmpty(guid)) return null;

            return AssetDatabase.LoadAssetAtPath<SubstanceMaterialInstanceSO>(AssetDatabase.GUIDToAssetPath(guid));
        }

        #endregion

        #region Controls


        public static bool DrawLinkedButton(Rect position, bool isLinked)
        {
            return DrawLinkedButton(position, isLinked, Labels.Controls.LinkedLabel, Labels.Controls.UnlinkedLabel);
        }


        public static bool DrawLinkedButton(Rect position, bool isLinked, GUIContent linkedLabel, GUIContent unlinkedLabel)
        {
            Color cachedGUI = GUI.backgroundColor;

            GUI.color *= isLinked ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1f);

            if(GUI.Button(position, isLinked ? linkedLabel : unlinkedLabel))
            {
                isLinked = !isLinked;
            }

            GUI.color = cachedGUI;

            return isLinked;
        }

        #endregion
    }
}