using UnityEngine;
using UnityEditor;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(RoundAttribute))]
    public class RoundAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (EditorGUI.ChangeCheckScope scope = new EditorGUI.ChangeCheckScope())
            {
                switch (property.propertyType)
                {
                    case SerializedPropertyType.Float:
                        float value = property.floatValue;

                        value = EditorGUI.FloatField(position, label, value);

                        if(scope.changed)
                        {
                            property.floatValue = Mathf.Round(value);
                        }
                        break;
                    case SerializedPropertyType.Vector2:
                        Vector2 vector2Value = property.vector2Value;

                        vector2Value = EditorGUI.Vector2Field(position, label, vector2Value);

                        if(scope.changed)
                        {
                            property.vector2Value = new Vector2(Mathf.Round(vector2Value.x), Mathf.Round(vector2Value.y));
                        }
                        break;
                    case SerializedPropertyType.Vector3:
                        Vector3 vector3Value = property.vector3Value;

                        vector3Value = EditorGUI.Vector3Field(position, label, vector3Value);

                        if(scope.changed)
                        {
                            property.vector3Value = new Vector3(Mathf.Round(vector3Value.x), Mathf.Round(vector3Value.y), Mathf.Round(vector3Value.z));
                        }
                        break;
                    case SerializedPropertyType.Vector4:
                        Vector4 vector4Value = property.vector4Value;

                        vector4Value = EditorGUI.Vector4Field(position, label, vector4Value);

                        if(scope.changed)
                        {
                            property.vector4Value = new Vector4(Mathf.Round(vector4Value.x), Mathf.Round(vector4Value.y), Mathf.Round(vector4Value.z), Mathf.Round(vector4Value.w));
                        }
                        break;
                    default:
                        EditorGUI.HelpBox(position, string.Format("Unsupported type [{0}]. Cannot round value.", property.type), MessageType.Warning);
                        break;
                }
            }
        }
    }
}