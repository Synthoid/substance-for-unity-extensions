using UnityEngine;
using UnityEditor;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(NoFoldoutAttribute))]
    public class NoFoldoutAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.hasChildren) return base.GetPropertyHeight(property, label);

            property.isExpanded = true;

            return EditorGUI.GetPropertyHeight(property, label, true) - EditorGUI.GetPropertyHeight(property, label, false);
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(!property.hasChildren)
            {
                base.OnGUI(position, property, label);
                return;
            }

            SerializedProperty iterator = property.Copy();

            property.Next(true);

            do
            {
                float height = EditorGUI.GetPropertyHeight(property, property.hasVisibleChildren);
                position.height = height;
                EditorGUI.PropertyField(position, property, property.hasVisibleChildren);
                position.y += height + EditorGUIUtility.standardVerticalSpacing;
            }
            while(property.NextVisible(false));
        }
    }
}