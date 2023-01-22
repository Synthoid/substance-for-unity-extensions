using UnityEngine;
using UnityEditor;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(IndentAttribute))]
    public class IndentAttributeDrawer : PropertyDrawer
    {
        private IndentAttribute Attr
        {
            get { return attribute as IndentAttribute; }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.indentLevel += Attr.level;
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.indentLevel -= Attr.level;
        }
    }
}