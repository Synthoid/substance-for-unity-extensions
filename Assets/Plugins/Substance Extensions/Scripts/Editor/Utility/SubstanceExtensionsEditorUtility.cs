using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.IO;
using SOS.SubstanceExtensions;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensionsEditor
{
    public static class SubstanceExtensionsEditorUtility
    {
        #region Utility

        public class Labels
        {
            public class Controls
            {
                public static readonly GUIContent LinkedLabel = new GUIContent(EditorGUIUtility.IconContent(EditorGUIUtility.isProSkin ? "d_Linked" : "Linked").image);
                public static readonly GUIContent UnlinkedLabel = new GUIContent(EditorGUIUtility.IconContent(EditorGUIUtility.isProSkin ? "d_Unlinked" : "Linked").image);
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
        /// Get the <see cref="SubstanceFileSO"/> referenced by a <see cref="SubstanceParameter"/> field.
        /// </summary>
        /// <param name="property"><see cref="SubstanceParameter"/> property to get the target reference for.</param>
        public static SubstanceFileSO GetGUIDReferenceSubstance(this SerializedProperty property)
        {
            string guid = property.FindPropertyRelative("guid").stringValue;

            if(string.IsNullOrEmpty(guid)) return null;

            return AssetDatabase.LoadAssetAtPath<SubstanceFileSO>(AssetDatabase.GUIDToAssetPath(guid));
        }

        #endregion

        #region Controls


        public static bool DrawLinkedButton(Rect position, bool isLinked)
        {
            return DrawLinkedButton(position, isLinked, Labels.Controls.LinkedLabel, Labels.Controls.UnlinkedLabel);
        }


        public static bool DrawLinkedButton(Rect position, bool isLinked, GUIContent linkedLabel, GUIContent unlinkedLabel)
        {
            if(GUI.Button(position, isLinked ? linkedLabel : unlinkedLabel, EditorStyles.iconButton))
            {
                isLinked = !isLinked;
            }

            return isLinked;
        }

        #endregion

        #region Search

        public static void DrawPopupSearchWindow(Rect position, GUIContent label, int index, GUIContent[] labels, System.Action<int> selectionCallback, GUIContent title = default)
        {
            if(label != GUIContent.none)
            {
                EditorGUI.PrefixLabel(position, label);
                position.Set(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, position.height);
            }

            if(EditorGUI.DropdownButton(position, labels[index], FocusType.Keyboard))
            {
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)),
                    ScriptableObject.CreateInstance<LabelSearchProvider>().Initialize(labels, selectionCallback, title));
            }
        }


        public static void DrawPopupSearchWindow(Rect position, int index, GUIContent[] labels, System.Action<int> selectionCallback, GUIContent title=default)
        {
            if(EditorGUI.DropdownButton(position, labels[index], FocusType.Keyboard))
            {
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)),
                    ScriptableObject.CreateInstance<LabelSearchProvider>().Initialize(labels, selectionCallback, title));
            }
        }

        #endregion
    }
}