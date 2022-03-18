using UnityEngine;
using UnityEditor;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Base class for property drawers of classes that display values based on asset references.
    /// </summary>
    public abstract class GUIDReferenceDrawer : PropertyDrawer
    {
        protected const float FIELD_WIDTH = 36f;

        /// <summary>
        /// Name for the asset guid property.
        /// </summary>
        public abstract string AssetField { get; }
        /// <summary>
        /// Name for the value property.
        /// </summary>
        public abstract string ValueField { get; }
        /// <summary>
        /// Type for the target asset to pull values from.
        /// </summary>
        public abstract System.Type AssetType { get; }

        /// <summary>
        /// Override to draw the actual value field once the target asset has been loaded. By default, this only draws a help box.
        /// </summary>
        /// <param name="position">Position for the value field.</param>
        /// <param name="property">Base <see cref="SerializedProperty"/>.</param>
        /// <param name="assetProperty"><see cref="SerializedProperty"/> for the target asset's guid string value.</param>
        /// <param name="valueProperty"><see cref="SerializedProperty"/> for the value field.</param>
        protected virtual void DrawValueField(Rect position, SerializedProperty property, SerializedProperty assetProperty, SerializedProperty valueProperty)
        {
            EditorGUI.HelpBox(position, "Override DrawValueField() to draw a field here.", MessageType.Info);
        }

        protected SerializedProperty GetAssetProperty(SerializedProperty property)
        {
            return property.FindPropertyRelative(AssetField);
        }


        protected SerializedProperty GetValueProperty(SerializedProperty property)
        {
            return property.FindPropertyRelative(ValueField);
        }
    }


    public abstract class GUIDReferenceDrawer<T> : GUIDReferenceDrawer where T : Object
    {
        public override System.Type AssetType
        {
            get { return typeof(T); }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;

            SerializedProperty assetProperty = GetAssetProperty(property);
            string assetGuid = assetProperty.stringValue;
            T asset = null;

            if(string.IsNullOrEmpty(assetGuid))
            {
                EditorGUI.BeginChangeCheck();
                asset = (T)EditorGUI.ObjectField(position, label, asset, AssetType, false);
                if(EditorGUI.EndChangeCheck())
                {
                    assetProperty.stringValue = asset != null ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset)) : "";
                }
            }
            else
            {
                asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assetGuid));
                float width = position.width;

                EditorGUI.PrefixLabel(position, label);

                position.Set(position.x + position.width - FIELD_WIDTH, position.y, FIELD_WIDTH, position.height);

                EditorGUI.BeginChangeCheck();
                asset = (T)EditorGUI.ObjectField(position, GUIContent.none, asset, AssetType, false);
                if(EditorGUI.EndChangeCheck())
                {
                    assetProperty.stringValue = asset != null ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset)) : "";
                }

                position.Set(position.x - (width - EditorGUIUtility.labelWidth - FIELD_WIDTH), position.y, (width - EditorGUIUtility.labelWidth) - FIELD_WIDTH, position.height);

                DrawValueField(position, property, assetProperty, GetValueProperty(property));
            }
        }
    }
}