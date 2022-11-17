using UnityEngine;
using UnityEditor;
using SOS.SubstanceExtensions;
using Adobe.Substance;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(RuntimeGraphOnlyAttribute))]
    public class RuntimeGraphOnlyAttributeDrawer : PropertyDrawer
    {
        private float cachedWidth = 16f;

        private RuntimeGraphOnlyAttribute Attr
        {
            get { return (RuntimeGraphOnlyAttribute)attribute; }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);

            if(property.objectReferenceInstanceIDValue != 0)
            {
                SubstanceGraphSO graph = AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GetAssetPath(property.objectReferenceInstanceIDValue));

                if(graph != null && !graph.IsRuntimeOnly)
                {
                    height += EditorGUIUtility.standardVerticalSpacing;
                    height += EditorStyles.helpBox.CalcHeight(new GUIContent(Attr.warning), cachedWidth - EditorGUIUtility.labelWidth);
                }
            }

            return height;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, property, label);

            if(property.objectReferenceInstanceIDValue != 0)
            {
                SubstanceGraphSO graph = AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GetAssetPath(property.objectReferenceInstanceIDValue));

                if(graph != null && !graph.IsRuntimeOnly)
                {
                    if(position.width > 0f) cachedWidth = position.width;

                    position.Set(position.x + EditorGUIUtility.labelWidth, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width - EditorGUIUtility.labelWidth, EditorStyles.helpBox.CalcHeight(new GUIContent(Attr.warning), position.width - EditorGUIUtility.labelWidth));

                    EditorGUI.HelpBox(position, Attr.warning, MessageType.Warning);
                }
            }
        }
    }
}