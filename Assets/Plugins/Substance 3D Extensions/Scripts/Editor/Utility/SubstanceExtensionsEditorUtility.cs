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

        public const float kSpaceHeight = 8f;

        public class Labels
        {
            public class Controls
            {
                public static readonly GUIContent LinkedLabel = new GUIContent(EditorGUIUtility.IconContent(EditorGUIUtility.isProSkin ? "d_Linked" : "Linked").image);
                public static readonly GUIContent UnlinkedLabel = new GUIContent(EditorGUIUtility.IconContent(EditorGUIUtility.isProSkin ? "d_Unlinked" : "Linked").image);
            }

            public class Matrix
            {
                public static readonly GUIContent RotationLabel = new GUIContent("Rotation");
                public static readonly GUIContent RotationAngleLabel = new GUIContent("Angle", "Degrees (counterclockwise) to rotate the current matrix value by.");
                public static readonly GUIContent RotationApplyLabel = new GUIContent("Apply Angle", "Apply the given angle (counterclockwise, in degrees) to the current matrix value.");
                public static readonly GUIContent Rotation180Label = new GUIContent("180", "Rotate the current matrix 180 degrees.");
                public static readonly GUIContent Rotaiton90Label_CW = new GUIContent("90 cw", "Rotate the current matrix 90 degrees clockwise.");
                public static readonly GUIContent Rotation90Label_CCW = new GUIContent("90 ccw", "Rotate the current matrix 90 degrees counterclockwise.");
                public static readonly GUIContent StretchLabel = new GUIContent("Stretch");
                public static readonly GUIContent StretchWidthLabel = new GUIContent("Width");
                public static readonly GUIContent StretchHeightLabel = new GUIContent("Height");
                public static readonly GUIContent StretchApplyWidthLabel = new GUIContent("Apply Width", "Apply the given stretch width percentage to the current matrix value.");
                public static readonly GUIContent StretchApplyHeightLabel = new GUIContent("Apply Height", "Apply the given stretch height percentage to the current matrix value.");
                public static readonly GUIContent StretchPercentLabel = new GUIContent("%");
                //Misc
                public static readonly GUIContent MirrorHorizontalLabel = new GUIContent("Hori Mirror", "Mirror the current matrix value horizontally.");
                public static readonly GUIContent MirrorVerticalLabel = new GUIContent("Vert Mirror", "Mirror the current matrix value vertically.");
                public static readonly GUIContent Mul2Label = new GUIContent("x2", "Multiply the current matrix value by 2.");
                public static readonly GUIContent Div2Label = new GUIContent("/2", "Divide the current matrix value by 2.");
                //Swap
                public static readonly GUIContent SwapMatrixLabel = new GUIContent("Edit Matrix Values", "Edit the raw Float4 value of the matrix.");
                public static readonly GUIContent SwapRawLabel = new GUIContent("Back");
                //Raw Matrix
                public static GUIContent RawMatrixX1Label = new GUIContent("X1");
                public static GUIContent RawMatrixX2Label = new GUIContent("X2");
                public static GUIContent RawMatrixY1Label = new GUIContent("Y1");
                public static GUIContent RawMatrixY2Label = new GUIContent("Y2");
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
        /// Get the <see cref="SubstanceGraphSO"/> referenced by a <see cref="SubstanceParameter"/> field.
        /// </summary>
        /// <param name="property"><see cref="SubstanceParameter"/> property to get the target reference for.</param>
        public static SubstanceGraphSO GetGUIDReferenceSubstance(this SerializedProperty property)
        {
            string guid = property.FindPropertyRelative("guid").stringValue;

            if(string.IsNullOrEmpty(guid)) return null;

            return AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GUIDToAssetPath(guid));
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


        #region TransformMatrix

        /// <summary>
        /// Returns the height of a transform matrix GUI.
        /// </summary>
        public static float GetMatrixHeight()
        {
            return (EditorGUIUtility.singleLineHeight * 8f) + (EditorGUIUtility.standardVerticalSpacing * 7f) + (kSpaceHeight * 2f);
        }

        /// <summary>
        /// Returns the height of a raw transform matrix GUI.
        /// </summary>
        public static float GetMatrixRawHeight()
        {
            return (EditorGUIUtility.singleLineHeight * 3f) + (EditorGUIUtility.standardVerticalSpacing * 2f) + kSpaceHeight;
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

            if (GUI.Button(position, Labels.Matrix.RotationApplyLabel))
            {
                property.vector4Value = type == TransformMatrixType.Forward ? property.vector4Value.RotateCW(angle) : property.vector4Value.RotateCCW(angle);
            }

            //Buttons
            position.Set(position.x + position.width - 56f, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, 56f, position.height);

            if (GUI.Button(position, Labels.Matrix.Rotation90Label_CCW))
            {
                property.vector4Value = property.vector4Value.RotateCCW(90f);
            }

            position.x -= position.width;

            if (GUI.Button(position, Labels.Matrix.Rotation180Label))
            {
                property.vector4Value = property.vector4Value.RotateCW(180f);
            }

            position.x -= position.width;

            if (GUI.Button(position, Labels.Matrix.Rotaiton90Label_CW))
            {
                property.vector4Value = property.vector4Value.RotateCW(90f);
            }

            //Stretch
            position.Set(cachedX, position.y + position.height + EditorGUIUtility.standardVerticalSpacing + kSpaceHeight, cachedWidth, position.height);

            EditorGUI.LabelField(position, Labels.Matrix.StretchLabel, EditorStyles.boldLabel);

            //Width
            position.Set(position.x, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width - 100f, position.height);
            //position.width -= 100f;

            stretchWidth = EditorGUI.FloatField(position, Labels.Matrix.StretchWidthLabel, stretchWidth);

            position.Set(position.x + position.width, position.y, 16f, position.height);

            EditorGUI.LabelField(position, Labels.Matrix.StretchPercentLabel);

            position.Set(position.x + position.width, position.y, 84f, position.height);

            if (GUI.Button(position, Labels.Matrix.StretchApplyWidthLabel))
            {
                property.vector4Value = property.vector4Value.StretchWidth(stretchWidth);
            }

            //Height
            position.Set(cachedX, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, cachedWidth - 100f, position.height);

            stretchHeight = EditorGUI.FloatField(position, Labels.Matrix.StretchHeightLabel, stretchHeight);

            position.Set(position.x + position.width, position.y, 16f, position.height);

            EditorGUI.LabelField(position, Labels.Matrix.StretchPercentLabel);

            position.Set(position.x + position.width, position.y, 84f, position.height);

            if (GUI.Button(position, Labels.Matrix.StretchApplyHeightLabel))
            {
                property.vector4Value = property.vector4Value.StretchHeight(stretchHeight);
            }

            //Buttons
            position.Set(cachedX + cachedWidth - 32f, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, 32f, position.height);

            if (GUI.Button(position, Labels.Matrix.Div2Label))
            {
                property.vector4Value = property.vector4Value.Multiply(2f);
            }

            position.x -= position.width;

            if (GUI.Button(position, Labels.Matrix.Mul2Label))
            {
                property.vector4Value = property.vector4Value.Divide(2f);
            }

            position.Set(position.x - (48f + position.width), position.y, (48f + position.width), position.height);

            if (GUI.Button(position, Labels.Matrix.MirrorVerticalLabel))
            {
                property.vector4Value = property.vector4Value.MirrorVertical();
            }

            position.x -= position.width;

            if (GUI.Button(position, Labels.Matrix.MirrorHorizontalLabel))
            {
                property.vector4Value = property.vector4Value.MirrorHorizontal();
            }

            //Swap
            position.Set(cachedX + cachedWidth - 112f, position.y + position.height + EditorGUIUtility.standardVerticalSpacing + kSpaceHeight, 112f, position.height);

            //fieldPosition.Set(fieldPosition.x + fieldPosition.width - 112f, fieldPosition.y, 112f, fieldPosition.height);

            swapMode |= GUI.Button(position, Labels.Matrix.SwapMatrixLabel);
        }

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

            if (label != GUIContent.none)
            {
                property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label, true);
                showContents = property.isExpanded;
            }

            if (showContents)
            {
                //Rotation
                EditorGUILayout.LabelField(Labels.Matrix.RotationLabel, EditorStyles.boldLabel);

                Rect fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.width -= 86f;

                angle = EditorGUI.FloatField(fieldPosition, Labels.Matrix.RotationAngleLabel, angle);

                fieldPosition.Set(fieldPosition.x + fieldPosition.width + 2f, fieldPosition.y, 84f, fieldPosition.height);

                if (GUI.Button(fieldPosition, Labels.Matrix.RotationApplyLabel))
                {
                    property.vector4Value = type == TransformMatrixType.Forward ? property.vector4Value.RotateCW(angle) : property.vector4Value.RotateCCW(angle);
                }

                //Buttons
                fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.Set(fieldPosition.x + fieldPosition.width - 56f, fieldPosition.y, 56f, fieldPosition.height);

                if (GUI.Button(fieldPosition, Labels.Matrix.Rotation90Label_CCW))
                {
                    property.vector4Value = property.vector4Value.RotateCCW(90f);
                }

                fieldPosition.x -= fieldPosition.width;

                if (GUI.Button(fieldPosition, Labels.Matrix.Rotation180Label))
                {
                    property.vector4Value = property.vector4Value.RotateCW(180f);
                }

                fieldPosition.x -= fieldPosition.width;

                if (GUI.Button(fieldPosition, Labels.Matrix.Rotaiton90Label_CW))
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

                if (GUI.Button(fieldPosition, Labels.Matrix.StretchApplyWidthLabel))
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

                if (GUI.Button(fieldPosition, Labels.Matrix.StretchApplyHeightLabel))
                {
                    property.vector4Value = property.vector4Value.StretchHeight(stretchHeight);
                }

                //Buttons
                fieldPosition = EditorGUILayout.GetControlRect();

                fieldPosition.Set(fieldPosition.x + fieldPosition.width - 32f, fieldPosition.y, 32f, fieldPosition.height);

                if (GUI.Button(fieldPosition, Labels.Matrix.Div2Label))
                {
                    property.vector4Value = property.vector4Value.Multiply(2f);
                }

                fieldPosition.x -= fieldPosition.width;

                if (GUI.Button(fieldPosition, Labels.Matrix.Mul2Label))
                {
                    property.vector4Value = property.vector4Value.Divide(2f);
                }

                fieldPosition.Set(fieldPosition.x - (48f + fieldPosition.width), fieldPosition.y, (48f + fieldPosition.width), fieldPosition.height);

                if (GUI.Button(fieldPosition, Labels.Matrix.MirrorVerticalLabel))
                {
                    property.vector4Value = property.vector4Value.MirrorVertical();
                }

                fieldPosition.x -= fieldPosition.width;

                if (GUI.Button(fieldPosition, Labels.Matrix.MirrorHorizontalLabel))
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

            if (GUI.Button(fieldPosition, Labels.Matrix.RotationApplyLabel))
            {
                value = type == TransformMatrixType.Forward ? value.RotateCW(angle) : value.RotateCCW(angle);
            }

            //Buttons
            fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.Set(fieldPosition.x + fieldPosition.width - 56f, fieldPosition.y, 56f, fieldPosition.height);

            if (GUI.Button(fieldPosition, Labels.Matrix.Rotation90Label_CCW))
            {
                value = value.RotateCCW(90f);
            }

            fieldPosition.x -= fieldPosition.width;

            if (GUI.Button(fieldPosition, Labels.Matrix.Rotation180Label))
            {
                value = value.RotateCW(180f);
            }

            fieldPosition.x -= fieldPosition.width;

            if (GUI.Button(fieldPosition, Labels.Matrix.Rotaiton90Label_CW))
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

            if (GUI.Button(fieldPosition, Labels.Matrix.StretchApplyWidthLabel))
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

            if (GUI.Button(fieldPosition, Labels.Matrix.StretchApplyHeightLabel))
            {
                value = value.StretchHeight(stretchHeight);
            }

            //Buttons
            fieldPosition = EditorGUILayout.GetControlRect();

            fieldPosition.Set(fieldPosition.x + fieldPosition.width - 32f, fieldPosition.y, 32f, fieldPosition.height);

            if (GUI.Button(fieldPosition, Labels.Matrix.Div2Label))
            {
                value = value.Multiply(2f);
            }

            fieldPosition.x -= fieldPosition.width;

            if (GUI.Button(fieldPosition, Labels.Matrix.Mul2Label))
            {
                value = value.Divide(2f);
            }

            fieldPosition.Set(fieldPosition.x - (48f + fieldPosition.width), fieldPosition.y, (48f + fieldPosition.width), fieldPosition.height);

            if (GUI.Button(fieldPosition, Labels.Matrix.MirrorVerticalLabel))
            {
                value = value.MirrorVertical();
            }

            fieldPosition.x -= fieldPosition.width;

            if (GUI.Button(fieldPosition, Labels.Matrix.MirrorHorizontalLabel))
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
            position.Set(position.x + position.width - 48f, position.y + position.height + EditorGUIUtility.standardVerticalSpacing + kSpaceHeight, 48, position.height);

            swapMode |= GUI.Button(position, Labels.Matrix.SwapRawLabel);
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

            if (label != GUIContent.none)
            {
                property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label, true);
                showContents = property.isExpanded;
            }

            if (showContents)
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