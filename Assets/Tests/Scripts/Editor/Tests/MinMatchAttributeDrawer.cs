using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MinMatchAttribute))]
public class MinMatchAttributeDrawer : PropertyDrawer
{
    private MinMatchAttribute Attr
    {
        get { return (MinMatchAttribute)attribute; }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.Slider(position, property, Attr.min, Attr.max);

        float minValue = property.serializedObject.FindProperty(Attr.target).floatValue;

        if(property.floatValue < minValue)
        {
            property.floatValue = minValue;
        }
    }
}