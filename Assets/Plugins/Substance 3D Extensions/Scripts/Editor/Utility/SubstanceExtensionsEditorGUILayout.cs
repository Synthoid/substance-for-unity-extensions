using UnityEngine;
using UnityEditor;
using SOS.SubstanceExtensions;

using Labels = SOS.SubstanceExtensionsEditor.SubstanceExtensionsEditorUtility.Labels;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Auto laid out version of <see cref="SubstanceExtensionsEditorGUI"/>.
    /// </summary>
    public static class SubstanceExtensionsEditorGUILayout
    {
        #region TransformMatrix

        /// <summary>
        /// Draws a <see cref="Vector4"/> serialized property as a transform matrix field.
        /// </summary>
        /// <param name="property">Serialized property for a <see cref="Vector4"/>.</param>
        /// <param name="label">Label for the matrix's foldout.</param>
        /// <param name="type">Determines if the angle field uses clockwise (forward) or counterclockwise (inverse) rotation.</param>
        /// <param name="angle">Angle value the angle field.</param>
        /// <param name="stretchWidth">Value for the stretch width field. This should reflect a percentage value (ie 100 is the current stretch amount).</param>
        /// <param name="stretchHeight">Value for the stretch height field. This should reflect a percentage value (ie 100 is the current stretch amount).</param>
        /// <param name="swapMode">Bool that is switched to true if the matrix field should swap between transform and raw matrix UI.</param>
        public static void DrawTransformMatrix(SerializedProperty property, GUIContent label, TransformMatrixType type, ref float angle, ref float stretchWidth, ref float stretchHeight, ref bool swapMode)
        {
            bool showContents = true;

            if(label != GUIContent.none)
            {
                property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label, true);
                showContents = property.isExpanded;
            }

            if(showContents)
            {
                //Rotation
                EditorGUILayout.LabelField(Labels.Matrix.RotationLabel, EditorStyles.boldLabel);

                Rect fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.width -= 86f;

                angle = EditorGUI.FloatField(fieldPosition, Labels.Matrix.RotationAngleLabel, angle);

                fieldPosition.Set(fieldPosition.x + fieldPosition.width + 2f, fieldPosition.y, 84f, fieldPosition.height);

                if(GUI.Button(fieldPosition, Labels.Matrix.RotationApplyLabel))
                {
                    property.vector4Value = type == TransformMatrixType.Forward ? property.vector4Value.RotateCW(angle) : property.vector4Value.RotateCCW(angle);
                }

                //Buttons
                fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.Set(fieldPosition.x + fieldPosition.width - 56f, fieldPosition.y, 56f, fieldPosition.height);

                if(GUI.Button(fieldPosition, Labels.Matrix.Rotation90Label_CCW))
                {
                    property.vector4Value = property.vector4Value.RotateCCW(90f);
                }

                fieldPosition.x -= fieldPosition.width;

                if(GUI.Button(fieldPosition, Labels.Matrix.Rotation180Label))
                {
                    property.vector4Value = property.vector4Value.RotateCW(180f);
                }

                fieldPosition.x -= fieldPosition.width;

                if(GUI.Button(fieldPosition, Labels.Matrix.Rotaiton90Label_CW))
                {
                    property.vector4Value = property.vector4Value.RotateCW(90f);
                }

                //Stretch
                EditorGUILayout.Space();

                EditorGUILayout.LabelField(Labels.Matrix.StretchLabel, EditorStyles.boldLabel);

                //Width
                fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.width -= 100f;

                stretchWidth = EditorGUI.FloatField(fieldPosition, Labels.Matrix.StretchWidthLabel, stretchWidth);

                fieldPosition.Set(fieldPosition.x + fieldPosition.width, fieldPosition.y, 16f, fieldPosition.height);

                EditorGUI.LabelField(fieldPosition, Labels.Matrix.StretchPercentLabel);

                fieldPosition.Set(fieldPosition.x + fieldPosition.width, fieldPosition.y, 84f, fieldPosition.height);

                if(GUI.Button(fieldPosition, Labels.Matrix.StretchApplyWidthLabel))
                {
                    property.vector4Value = property.vector4Value.StretchWidth(stretchWidth);
                }

                //Height
                fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.width -= 100f;

                stretchHeight = EditorGUI.FloatField(fieldPosition, Labels.Matrix.StretchHeightLabel, stretchHeight);

                fieldPosition.Set(fieldPosition.x + fieldPosition.width, fieldPosition.y, 16f, fieldPosition.height);

                EditorGUI.LabelField(fieldPosition, Labels.Matrix.StretchPercentLabel);

                fieldPosition.Set(fieldPosition.x + fieldPosition.width, fieldPosition.y, 84f, fieldPosition.height);

                if(GUI.Button(fieldPosition, Labels.Matrix.StretchApplyHeightLabel))
                {
                    property.vector4Value = property.vector4Value.StretchHeight(stretchHeight);
                }

                //Buttons
                fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.Set(fieldPosition.x + fieldPosition.width - 32f, fieldPosition.y, 32f, fieldPosition.height);

                if(GUI.Button(fieldPosition, Labels.Matrix.Div2Label))
                {
                    property.vector4Value = property.vector4Value.Multiply(2f);
                }

                fieldPosition.x -= fieldPosition.width;

                if(GUI.Button(fieldPosition, Labels.Matrix.Mul2Label))
                {
                    property.vector4Value = property.vector4Value.Divide(2f);
                }

                fieldPosition.Set(fieldPosition.x - (48f + fieldPosition.width), fieldPosition.y, (48f + fieldPosition.width), fieldPosition.height);

                if(GUI.Button(fieldPosition, Labels.Matrix.MirrorVerticalLabel))
                {
                    property.vector4Value = property.vector4Value.MirrorVertical();
                }

                fieldPosition.x -= fieldPosition.width;

                if(GUI.Button(fieldPosition, Labels.Matrix.MirrorHorizontalLabel))
                {
                    property.vector4Value = property.vector4Value.MirrorHorizontal();
                }

                //Swap
                EditorGUILayout.Space();

                fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.Set(fieldPosition.x + fieldPosition.width - 112f, fieldPosition.y, 112f, fieldPosition.height);

                swapMode |= GUI.Button(fieldPosition, Labels.Matrix.SwapMatrixLabel);
            }
        }

        /// <summary>
        /// Draws a <see cref="Vector4"/> as a transform matrix field.
        /// </summary>
        /// <param name="value"><see cref="Vector4"/> to draw as a matrix field.</param>
        /// <param name="type">Determines if the angle field uses clockwise (forward) or counterclockwise (inverse) rotation.</param>
        /// <param name="angle">Angle value the angle field.</param>
        /// <param name="stretchWidth">Value for the stretch width field. This should reflect a percentage value (ie 100 is the current stretch amount).</param>
        /// <param name="stretchHeight">Value for the stretch height field. This should reflect a percentage value (ie 100 is the current stretch amount).</param>
        /// <param name="swapMode">Bool that is switched to true if the matrix field should swap between transform and raw matrix UI.</param>
        public static Vector4 DrawTransformMatrix(Vector4 value, TransformMatrixType type, ref float angle, ref float stretchWidth, ref float stretchHeight, ref bool swapMode)
        {
            //Rotation
            EditorGUILayout.LabelField(Labels.Matrix.RotationLabel, EditorStyles.boldLabel);

            Rect fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.width -= 86f;

            angle = EditorGUI.FloatField(fieldPosition, Labels.Matrix.RotationAngleLabel, angle);

            fieldPosition.Set(fieldPosition.x + fieldPosition.width + 2f, fieldPosition.y, 84f, fieldPosition.height);

            if(GUI.Button(fieldPosition, Labels.Matrix.RotationApplyLabel))
            {
                value = type == TransformMatrixType.Forward ? value.RotateCW(angle) : value.RotateCCW(angle);
            }

            //Buttons
            fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.Set(fieldPosition.x + fieldPosition.width - 56f, fieldPosition.y, 56f, fieldPosition.height);

            if(GUI.Button(fieldPosition, Labels.Matrix.Rotation90Label_CCW))
            {
                value = value.RotateCCW(90f);
            }

            fieldPosition.x -= fieldPosition.width;

            if(GUI.Button(fieldPosition, Labels.Matrix.Rotation180Label))
            {
                value = value.RotateCW(180f);
            }

            fieldPosition.x -= fieldPosition.width;

            if(GUI.Button(fieldPosition, Labels.Matrix.Rotaiton90Label_CW))
            {
                value = value.RotateCW(90f);
            }

            //Stretch
            EditorGUILayout.Space();

            EditorGUILayout.LabelField(Labels.Matrix.StretchLabel, EditorStyles.boldLabel);

            //Width
            fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.width -= 100f;

            stretchWidth = EditorGUI.FloatField(fieldPosition, Labels.Matrix.StretchWidthLabel, stretchWidth);

            fieldPosition.Set(fieldPosition.x + fieldPosition.width, fieldPosition.y, 16f, fieldPosition.height);

            EditorGUI.LabelField(fieldPosition, Labels.Matrix.StretchPercentLabel);

            fieldPosition.Set(fieldPosition.x + fieldPosition.width, fieldPosition.y, 84f, fieldPosition.height);

            if(GUI.Button(fieldPosition, Labels.Matrix.StretchApplyWidthLabel))
            {
                value = value.StretchWidth(stretchWidth);
            }

            //Height
            fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.width -= 100f;

            stretchHeight = EditorGUI.FloatField(fieldPosition, Labels.Matrix.StretchHeightLabel, stretchHeight);

            fieldPosition.Set(fieldPosition.x + fieldPosition.width, fieldPosition.y, 16f, fieldPosition.height);

            EditorGUI.LabelField(fieldPosition, Labels.Matrix.StretchPercentLabel);

            fieldPosition.Set(fieldPosition.x + fieldPosition.width, fieldPosition.y, 84f, fieldPosition.height);

            if(GUI.Button(fieldPosition, Labels.Matrix.StretchApplyHeightLabel))
            {
                value = value.StretchHeight(stretchHeight);
            }

            //Buttons
            fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.Set(fieldPosition.x + fieldPosition.width - 32f, fieldPosition.y, 32f, fieldPosition.height);

            if(GUI.Button(fieldPosition, Labels.Matrix.Div2Label))
            {
                value = value.Multiply(2f);
            }

            fieldPosition.x -= fieldPosition.width;

            if(GUI.Button(fieldPosition, Labels.Matrix.Mul2Label))
            {
                value = value.Divide(2f);
            }

            fieldPosition.Set(fieldPosition.x - (48f + fieldPosition.width), fieldPosition.y, (48f + fieldPosition.width), fieldPosition.height);

            if(GUI.Button(fieldPosition, Labels.Matrix.MirrorVerticalLabel))
            {
                value = value.MirrorVertical();
            }

            fieldPosition.x -= fieldPosition.width;

            if(GUI.Button(fieldPosition, Labels.Matrix.MirrorHorizontalLabel))
            {
                value = value.MirrorHorizontal();
            }

            //Swap
            EditorGUILayout.Space();

            fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.Set(fieldPosition.x + fieldPosition.width - 112f, fieldPosition.y, 112f, fieldPosition.height);

            swapMode |= GUI.Button(fieldPosition, Labels.Matrix.SwapMatrixLabel);

            return value;
        }

        /// <summary>
        /// Draws a <see cref="Vector4"/> serialized property as a raw matrix field.
        /// Values are x (X1), y (Y1), z (X2), and w (Y2).
        /// </summary>
        /// <param name="property">Serialized property for a <see cref="Vector4"/>.</param>
        /// <param name="label">Label for the raw matrix's foldout.</param>
        /// <param name="swapMode">Bool that is switched to true if the matrix field should swap between raw and transform matrix UI.</param>
        public static void DrawTransformMatrixRaw(SerializedProperty property, GUIContent label, ref bool swapMode)
        {
            bool showContents = true;

            if(label != GUIContent.none)
            {
                property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label, true);
                showContents = property.isExpanded;
            }

            if(showContents)
            {
                float cachedWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 24f;

                Rect fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.width = (fieldPosition.width * 0.5f) - 8f;

                EditorGUI.PropertyField(fieldPosition, property.FindPropertyRelative("x"), Labels.Matrix.RawMatrixX1Label);

                fieldPosition.x += fieldPosition.width + 16f;

                EditorGUI.PropertyField(fieldPosition, property.FindPropertyRelative("z"), Labels.Matrix.RawMatrixX2Label);

                fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.width = (fieldPosition.width * 0.5f) - 8f;

                EditorGUI.PropertyField(fieldPosition, property.FindPropertyRelative("y"), Labels.Matrix.RawMatrixY1Label);

                fieldPosition.x += fieldPosition.width + 16f;

                EditorGUI.PropertyField(fieldPosition, property.FindPropertyRelative("w"), Labels.Matrix.RawMatrixY2Label);

                EditorGUIUtility.labelWidth = cachedWidth;

                //Swap
                EditorGUILayout.Space();

                fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.Set(fieldPosition.x + fieldPosition.width - 48f, fieldPosition.y, 48f, fieldPosition.height);

                swapMode |= GUI.Button(fieldPosition, Labels.Matrix.SwapRawLabel);
            }
        }

        /// <summary>
        /// Draws a <see cref="Vector4"/> as a raw matrix field.
        /// Values are x (X1), y (Y1), z (X2), and w (Y2).
        /// </summary>
        /// <param name="value"><see cref="Vector4"/> to draw as a matrix field.</param>
        /// <param name="swapMode">Bool that is switched to true if the matrix field should swap between raw and transform matrix UI.</param>
        public static Vector4 DrawTransformMatrixRaw(Vector4 value, ref bool swapMode)
        {
            float cachedWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 24f;

            Rect fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.width = (fieldPosition.width * 0.5f) - 8f;

            value.x = EditorGUI.FloatField(fieldPosition, Labels.Matrix.RawMatrixX1Label, value.x);

            fieldPosition.x += fieldPosition.width + 16f;

            value.z = EditorGUI.FloatField(fieldPosition, Labels.Matrix.RawMatrixX2Label, value.z);

            fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.width = (fieldPosition.width * 0.5f) - 8f;

            value.y = EditorGUI.FloatField(fieldPosition, Labels.Matrix.RawMatrixY1Label, value.y);

            fieldPosition.x += fieldPosition.width + 16f;

            value.w = EditorGUI.FloatField(fieldPosition, Labels.Matrix.RawMatrixY2Label, value.w);

            EditorGUIUtility.labelWidth = cachedWidth;

            //Swap
            EditorGUILayout.Space();

            fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.Set(fieldPosition.x + fieldPosition.width - 48f, fieldPosition.y, 48f, fieldPosition.height);

            swapMode |= GUI.Button(fieldPosition, Labels.Matrix.SwapRawLabel);

            return value;
        }

        #endregion
    }
}