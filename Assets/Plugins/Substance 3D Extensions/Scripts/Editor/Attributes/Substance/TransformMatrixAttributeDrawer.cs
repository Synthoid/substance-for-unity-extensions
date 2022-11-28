using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(TransformMatrixAttribute))]
    public class TransformMatrixAttributeDrawer : PropertyDrawer
    {
        private TransformMatrixAttribute attr = null;
        private Dictionary<string, float> angles = new Dictionary<string, float>();
        private Dictionary<string, float> stretchWidths = new Dictionary<string, float>();
        private Dictionary<string, float> stretchHeights = new Dictionary<string, float>();
        private Dictionary<string, bool> rawEdits = new Dictionary<string, bool>();

        private TransformMatrixAttribute Attr
        {
            get { return attr != null ? attr : (attr = (TransformMatrixAttribute)attribute); }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);

            if(property.isExpanded)
            {
                if(!angles.ContainsKey(property.propertyPath))
                {
                    string path = property.propertyPath;
                    angles.Add(path, 0f);
                    stretchWidths.Add(path, 100f);
                    stretchHeights.Add(path, 100f);
                    rawEdits.Add(path, false);
                }

                height += EditorGUIUtility.standardVerticalSpacing;
                height += rawEdits[property.propertyPath] ? SubstanceExtensionsEditorUtility.GetMatrixRawHeight() : SubstanceExtensionsEditorUtility.GetMatrixHeight();
            }

            return height;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float height = position.height;

            position.height = EditorGUIUtility.singleLineHeight;

            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);

            if(property.isExpanded)
            {
                if(!angles.ContainsKey(property.propertyPath))
                {
                    string path = property.propertyPath;
                    angles.Add(path, 0f);
                    stretchWidths.Add(path, 100f);
                    stretchHeights.Add(path, 100f);
                    rawEdits.Add(path, false);
                }

                float angle = angles[property.propertyPath];
                float stretchWidth = stretchWidths[property.propertyPath];
                float stretchHeight = stretchHeights[property.propertyPath];

                position.Set(position.x, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width, height - (position.height + EditorGUIUtility.standardVerticalSpacing));

                bool swapMode = false;

                if(rawEdits[property.propertyPath])
                {
                    SubstanceExtensionsEditorUtility.DrawTransformMatrixRaw(position, property, ref swapMode);
                }
                else
                {
                    SubstanceExtensionsEditorUtility.DrawTransformMatrix(position, property, Attr.type, ref angle, ref stretchWidth, ref stretchHeight, ref swapMode);

                    angles[property.propertyPath] = angle;
                    stretchWidths[property.propertyPath] = stretchWidth;
                    stretchHeights[property.propertyPath] = stretchHeight;
                }

                if(swapMode)
                {
                    rawEdits[property.propertyPath] = !rawEdits[property.propertyPath];
                }
            }
        }
    }
}
