using UnityEngine;
using UnityEditor;
using SOS.SubstanceExtensions;

using Labels = SOS.SubstanceExtensionsEditor.SubstanceExtensionsEditorUtility.Labels;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Handles drawing inspector elements using manual position values.
    /// For auto laid out versions of these methods, see <see cref="SubstanceExtensionsEditorGUILayout"/>.
    /// </summary>
    public static class SubstanceExtensionsEditorGUI
    {
        #region Controls

        /// <summary>
        /// Draw a button displaying a linked or unlinked icon based on the given state. This is useful when optionally linking field values.
        /// </summary>
        /// <param name="position">Position to draw the button at.</param>
        /// <param name="isLinked">State for the linkage.</param>
        /// <returns>New value for the linkage.</returns>
        public static bool DrawLinkedButton(Rect position, bool isLinked)
        {
            return DrawLinkedButton(position, isLinked, Labels.Controls.LinkedLabel, Labels.Controls.UnlinkedLabel);
        }

        /// <summary>
        /// Draw a button displaying a linked or unlinked icon based on the given state. This is useful when optionally linking field values.
        /// </summary>
        /// <param name="position">Position to draw the button at.</param>
        /// <param name="isLinked">State for the linkage.</param>
        /// <param name="linkedLabel">Content shown when values are linked.</param>
        /// <param name="unlinkedLabel">Content shown when values are NOT linked.</param>
        /// <returns>New value for the linkage.</returns>
        public static bool DrawLinkedButton(Rect position, bool isLinked, GUIContent linkedLabel, GUIContent unlinkedLabel)
        {
            if(GUI.Button(position, isLinked ? linkedLabel : unlinkedLabel, EditorStyles.iconButton))
            {
                isLinked = !isLinked;
            }

            return isLinked;
        }

        #endregion

        #region TransformMatrix

        /// <summary>
        /// Returns the height of a transform matrix GUI.
        /// </summary>
        public static float GetMatrixHeight()
        {
            return (EditorGUIUtility.singleLineHeight * 8f) + (EditorGUIUtility.standardVerticalSpacing * 7f) + (SubstanceExtensionsEditorUtility.kSpaceHeight * 2f);
        }

        /// <summary>
        /// Returns the height of a raw transform matrix GUI.
        /// </summary>
        public static float GetMatrixRawHeight()
        {
            return (EditorGUIUtility.singleLineHeight * 3f) + (EditorGUIUtility.standardVerticalSpacing * 2f) + SubstanceExtensionsEditorUtility.kSpaceHeight;
        }

        /// <summary>
        /// Draws a <see cref="Vector4"/> serialized property as a transform matrix field.
        /// </summary>
        /// <param name="position">Total position for the matrix GUI.</param>
        /// <param name="property">Serialized property for a <see cref="Vector4"/>.</param>
        /// <param name="type">Determines if the angle field uses clockwise (forward) or counterclockwise (inverse) rotation.</param>
        /// <param name="angle">Angle value the angle field.</param>
        /// <param name="stretchWidth">Value for the stretch width field. This should reflect a percentage value (ie 100 is the current stretch amount).</param>
        /// <param name="stretchHeight">Value for the stretch height field. This should reflect a percentage value (ie 100 is the current stretch amount).</param>
        /// <param name="swapMode">Bool that is switched to true if the matrix field should swap between transform and raw matrix UI.</param>
        public static void DrawTransformMatrix(Rect position, SerializedProperty property, TransformMatrixType type, ref float angle, ref float stretchWidth, ref float stretchHeight, ref bool swapMode)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            float cachedX = position.x;
            float cachedWidth = position.width;

            //Rotation
            EditorGUI.LabelField(position, Labels.Matrix.RotationLabel, EditorStyles.boldLabel);

            position.Set(position.x, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width - 86f, position.height);

            angle = EditorGUI.FloatField(position, Labels.Matrix.RotationAngleLabel, angle);

            position.Set(position.x + position.width + 2f, position.y, 84f, position.height);

            if(GUI.Button(position, Labels.Matrix.RotationApplyLabel))
            {
                property.vector4Value = type == TransformMatrixType.Forward ? property.vector4Value.RotateCW(angle) : property.vector4Value.RotateCCW(angle);
            }

            //Buttons
            position.Set(position.x + position.width - 56f, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, 56f, position.height);

            if(GUI.Button(position, Labels.Matrix.Rotation90Label_CCW))
            {
                property.vector4Value = property.vector4Value.RotateCCW(90f);
            }

            position.x -= position.width;

            if(GUI.Button(position, Labels.Matrix.Rotation180Label))
            {
                property.vector4Value = property.vector4Value.RotateCW(180f);
            }

            position.x -= position.width;

            if(GUI.Button(position, Labels.Matrix.Rotaiton90Label_CW))
            {
                property.vector4Value = property.vector4Value.RotateCW(90f);
            }

            //Stretch
            position.Set(cachedX, position.y + position.height + EditorGUIUtility.standardVerticalSpacing + SubstanceExtensionsEditorUtility.kSpaceHeight, cachedWidth, position.height);

            EditorGUI.LabelField(position, Labels.Matrix.StretchLabel, EditorStyles.boldLabel);

            //Width
            position.Set(position.x, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width - 100f, position.height);

            stretchWidth = EditorGUI.FloatField(position, Labels.Matrix.StretchWidthLabel, stretchWidth);

            position.Set(position.x + position.width, position.y, 16f, position.height);

            EditorGUI.LabelField(position, Labels.Matrix.StretchPercentLabel);

            position.Set(position.x + position.width, position.y, 84f, position.height);

            if(GUI.Button(position, Labels.Matrix.StretchApplyWidthLabel))
            {
                property.vector4Value = property.vector4Value.StretchWidth(stretchWidth);
            }

            //Height
            position.Set(cachedX, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, cachedWidth - 100f, position.height);

            stretchHeight = EditorGUI.FloatField(position, Labels.Matrix.StretchHeightLabel, stretchHeight);

            position.Set(position.x + position.width, position.y, 16f, position.height);

            EditorGUI.LabelField(position, Labels.Matrix.StretchPercentLabel);

            position.Set(position.x + position.width, position.y, 84f, position.height);

            if(GUI.Button(position, Labels.Matrix.StretchApplyHeightLabel))
            {
                property.vector4Value = property.vector4Value.StretchHeight(stretchHeight);
            }

            //Buttons
            position.Set(cachedX + cachedWidth - 32f, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, 32f, position.height);

            if(GUI.Button(position, Labels.Matrix.Div2Label))
            {
                property.vector4Value = property.vector4Value.Multiply(2f);
            }

            position.x -= position.width;

            if(GUI.Button(position, Labels.Matrix.Mul2Label))
            {
                property.vector4Value = property.vector4Value.Divide(2f);
            }

            position.Set(position.x - (48f + position.width), position.y, (48f + position.width), position.height);

            if(GUI.Button(position, Labels.Matrix.MirrorVerticalLabel))
            {
                property.vector4Value = property.vector4Value.MirrorVertical();
            }

            position.x -= position.width;

            if(GUI.Button(position, Labels.Matrix.MirrorHorizontalLabel))
            {
                property.vector4Value = property.vector4Value.MirrorHorizontal();
            }

            //Swap
            position.Set(cachedX + cachedWidth - 112f, position.y + position.height + EditorGUIUtility.standardVerticalSpacing + SubstanceExtensionsEditorUtility.kSpaceHeight, 112f, position.height);

            swapMode |= GUI.Button(position, Labels.Matrix.SwapMatrixLabel);
        }

        /// <summary>
        /// Draws a <see cref="Vector4"/> serialized property as a raw matrix field.
        /// </summary>
        /// <param name="position">Total position for the matrix GUI.</param>
        /// <param name="property">Serialized property for the matrix vector.</param>
        /// <param name="swapMode">Bool that is switched to true if the matrix field should swap between raw and transform matrix UI.</param>
        public static void DrawTransformMatrixRaw(Rect position, SerializedProperty property, ref bool swapMode)
        {
            float cachedWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 24f;

            position.Set(position.x, position.y, (position.width * 0.5f) - 8f, EditorGUIUtility.singleLineHeight);

            EditorGUI.PropertyField(position, property.FindPropertyRelative("x"), Labels.Matrix.RawMatrixX1Label);

            position.x += position.width + 16f;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("z"), Labels.Matrix.RawMatrixX2Label);

            position.Set(position.x - (position.width + 16f), position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width, position.height);

            EditorGUI.PropertyField(position, property.FindPropertyRelative("y"), Labels.Matrix.RawMatrixY1Label);

            position.x += position.width + 16f;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("w"), Labels.Matrix.RawMatrixY2Label);

            EditorGUIUtility.labelWidth = cachedWidth;

            //Swap
            position.Set(position.x + position.width - 48f, position.y + position.height + EditorGUIUtility.standardVerticalSpacing + SubstanceExtensionsEditorUtility.kSpaceHeight, 48, position.height);

            swapMode |= GUI.Button(position, Labels.Matrix.SwapRawLabel);
        }

        #endregion
    }
}