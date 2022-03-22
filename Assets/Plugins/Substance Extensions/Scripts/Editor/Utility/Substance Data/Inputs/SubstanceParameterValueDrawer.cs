using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Text;
using SOS.SubstanceExtensions;
using Adobe.Substance;
using Adobe.Substance.Input;
using Adobe.Substance.Input.Description;

using Random = UnityEngine.Random;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomPropertyDrawer(typeof(SubstanceParameterValue))]
    public class SubstanceParameterValueDrawer : PropertyDrawer
    {
        private class Labels
        {
            //Value
            public static readonly GUIContent FloatLabel = new GUIContent("Value", "Float value for the parameter.");
            public static readonly GUIContent Float2Label = new GUIContent("Value", "Float2 value for the parameter.");
            public static readonly GUIContent Float3Label = new GUIContent("Value", "Float3 value for the parameter.");
            public static readonly GUIContent Float4Label = new GUIContent("Value", "Float4 value for the parameter.");
            public static readonly GUIContent IntLabel = new GUIContent("Value", "Int value for the parameter.");
            public static readonly GUIContent EnumLabel = new GUIContent("Value", "Enum value for the parameter.");
            public static readonly GUIContent Int2Label = new GUIContent("Value", "Int2 value for the parameter.");
            public static readonly GUIContent Int3Label = new GUIContent("Value", "Int3 value for the parameter.");
            public static readonly GUIContent Int4Label = new GUIContent("Value", "Int4 value for the parameter.");
            public static readonly GUIContent BoolLabel = new GUIContent("Value", "Bool value for the parameter. (This is a convenience wrapper for an Int parameter, with FALSE being 0 and TRUE being anything else, usually 1)");
            public static readonly GUIContent Float3ColorLabel = new GUIContent("Value", "Color value for the parameter. (This is a convenience wrapper for a Float3 parameter)");
            public static readonly GUIContent Float4ColorLabel = new GUIContent("Value", "Color value for the parameter. (This is a convenience wrapper for a Float4 parameter)");
            public static readonly GUIContent StringLabel = new GUIContent("Value", "String value for the parameter.");
            public static readonly GUIContent TextureLabel = new GUIContent("Value", "Texture value for the parameter.");
            public static readonly GUIContent DefaultEnumLabel = new GUIContent("Value", "<No Enum>\nThis parameter is an enum with no values...");
            //Vector
            public static readonly GUIContent XLabel = new GUIContent("X");
            public static readonly GUIContent YLabel = new GUIContent("Y");
            public static readonly GUIContent ZLabel = new GUIContent("Z");
            public static readonly GUIContent WLabel = new GUIContent("W");
            //Random Seed
            public static readonly GUIContent ReseedButtonLabel = new GUIContent("Reseed");
        }

        private class Defaults
        {
            public static readonly GUIContent[] DefaultEnumLabels = new GUIContent[1] { new GUIContent("<No Enum>") };
            public static readonly int[] DefaultEnumLabelValues = new int[1] { 0 };
        }

        private const string NAME_RANDOM_SEED = "$randomseed";

        private Dictionary<string, Dictionary<int, Dictionary<int, GUIContent[]>>> EnumLabels = new Dictionary<string, Dictionary<int, Dictionary<int, GUIContent[]>>>();
        private Dictionary<string, Dictionary<int, Dictionary<int, int[]>>> EnumValues = new Dictionary<string, Dictionary<int, Dictionary<int, int[]>>>();
        private Dictionary<string, Dictionary<int, Dictionary<int, GUIContent>>> EnumFieldLabels = new Dictionary<string, Dictionary<int, Dictionary<int, GUIContent>>>();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);

            if(property.isExpanded)
            {
                height *= 2f;
                height += EditorGUIUtility.standardVerticalSpacing * 2f;

                if(string.IsNullOrEmpty(property.FindPropertyRelative("parameter.name").stringValue)) return height;
                if(string.IsNullOrEmpty(property.FindPropertyRelative("parameter.guid").stringValue)) return height;

                SubstanceValueType valueType = (SubstanceValueType)property.FindPropertyRelative("parameter.type").intValue;

                switch(valueType)
                {
                    case SubstanceValueType.Float:
                    case SubstanceValueType.Image:
                    case SubstanceValueType.Int:
                        height += EditorGUIUtility.singleLineHeight;
                        break;
                    case SubstanceValueType.Float2:
                        if(property.FindPropertyRelative("vectorValue").isExpanded)
                        {
                            height += EditorGUIUtility.singleLineHeight * 3f;
                            height += EditorGUIUtility.standardVerticalSpacing * 3f;
                        }
                        else
                        {
                            height += EditorGUIUtility.singleLineHeight;
                        }
                        break;
                    case SubstanceValueType.Int2:
                        if(property.FindPropertyRelative("vectorIntValue").isExpanded)
                        {
                            height += EditorGUIUtility.singleLineHeight * 3f;
                            height += EditorGUIUtility.standardVerticalSpacing * 3f;
                        }
                        else
                        {
                            height += EditorGUIUtility.singleLineHeight;
                        }
                        break;
                    case SubstanceValueType.Float3:
                        if(property.FindPropertyRelative("vectorValue").isExpanded)
                        {
                            height += EditorGUIUtility.singleLineHeight * 4f;
                            height += EditorGUIUtility.standardVerticalSpacing * 4f;
                        }
                        else
                        {
                            height += EditorGUIUtility.singleLineHeight;
                        }
                        break;
                    case SubstanceValueType.Int3:
                        if(property.FindPropertyRelative("vectorIntValue").isExpanded)
                        {
                            height += EditorGUIUtility.singleLineHeight * 4f;
                            height += EditorGUIUtility.standardVerticalSpacing * 4f;
                        }
                        else
                        {
                            height += EditorGUIUtility.singleLineHeight;
                        }
                        break;
                    case SubstanceValueType.Float4:
                        SubstanceWidgetType widgetType = (SubstanceWidgetType)property.FindPropertyRelative("parameter.widgetType").intValue;

                        if(widgetType == SubstanceWidgetType.Color)
                        {
                            height += EditorGUIUtility.singleLineHeight;
                        }
                        else
                        {
                            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("vectorValue"));
                        }
                        break;
                    case SubstanceValueType.Int4:
                        height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("vectorIntValue"));
                        break;
                    case SubstanceValueType.String:
                        height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("stringValue"));
                        break;
                }
            }

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;

            //TODO: Menu option to reset to default value?
            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, property.isExpanded, label);
            EditorGUI.EndFoldoutHeaderGroup();

            if(property.isExpanded)
            {
                SubstanceWidgetType widgetType = (SubstanceWidgetType)property.FindPropertyRelative("parameter.widgetType").intValue;

                position.Set(position.x, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width, position.height);
                EditorGUI.BeginChangeCheck();
                EditorGUI.PropertyField(position, property.FindPropertyRelative("parameter"));
                if(EditorGUI.EndChangeCheck())
                {
                    
                }

                if(string.IsNullOrEmpty(property.FindPropertyRelative("parameter.name").stringValue)) return;

                SubstanceValueType valueType = (SubstanceValueType)property.FindPropertyRelative("parameter.type").intValue;

                switch(valueType)
                {
                    case SubstanceValueType.Float:
                        position.Set(position.x, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width, position.height);
                        float floatVal = property.FindPropertyRelative("vectorValue.x").floatValue;

                        EditorGUI.BeginChangeCheck();

                        if(widgetType == SubstanceWidgetType.Slider) //TODO: Slider only if parameter is clamped?
                        {
                            float floatMin = property.FindPropertyRelative("parameter.rangeMin.x").floatValue;
                            float floatMax = property.FindPropertyRelative("parameter.rangeMax.x").floatValue;

                            floatVal = EditorGUI.Slider(position, Labels.FloatLabel, floatVal, floatMin, floatMax);
                        }
                        else
                        {
                            floatVal = EditorGUI.FloatField(position, Labels.FloatLabel, floatVal);
                        }

                        if(EditorGUI.EndChangeCheck())
                        {
                            property.FindPropertyRelative("vectorValue.x").floatValue = floatVal;
                        }
                        break;
                    case SubstanceValueType.Float2:
                        position.Set(position.x + EditorGUIUtility.singleLineHeight, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width - EditorGUIUtility.singleLineHeight, position.height);

                        SerializedProperty float2Property = property.FindPropertyRelative("vectorValue");

                        float2Property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, float2Property.isExpanded, Labels.Float2Label);
                        EditorGUI.EndFoldoutHeaderGroup();

                        if(float2Property.isExpanded)
                        {
                            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                            Vector2 float2Val = float2Property.vector4Value;

                            EditorGUI.BeginChangeCheck();

                            if(widgetType == SubstanceWidgetType.Slider)
                            {
                                Vector2 float2Min = property.FindPropertyRelative("parameter.rangeMin").vector4Value;
                                Vector2 float2Max = property.FindPropertyRelative("parameter.rangeMax").vector4Value;

                                float2Val.x = EditorGUI.Slider(position, Labels.XLabel, float2Val.x, float2Min.x, float2Max.x);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                float2Val.y = EditorGUI.Slider(position, Labels.YLabel, float2Val.y, float2Min.y, float2Max.y);
                            }
                            else
                            {
                                float2Val.x = EditorGUI.FloatField(position, Labels.XLabel, float2Val.x);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                float2Val.y = EditorGUI.FloatField(position, Labels.YLabel, float2Val.y);
                            }

                            if(EditorGUI.EndChangeCheck())
                            {
                                property.FindPropertyRelative("vectorValue").vector4Value = float2Val;
                            }
                        }
                        break;
                    case SubstanceValueType.Float3:
                        position.Set(position.x + EditorGUIUtility.singleLineHeight, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width - EditorGUIUtility.singleLineHeight, position.height);

                        Vector3 float3Val = property.FindPropertyRelative("vectorValue").vector4Value;

                        if(widgetType == SubstanceWidgetType.Color)
                        {
                            position.Set(position.x - EditorGUIUtility.singleLineHeight, position.y, position.width + EditorGUIUtility.singleLineHeight, position.height);

                            EditorGUI.BeginChangeCheck();
                            float3Val = (Vector3)(Vector4)EditorGUI.ColorField(position, Labels.Float3ColorLabel, (Vector4)float3Val, true, false, false);
                            if(EditorGUI.EndChangeCheck())
                            {
                                property.FindPropertyRelative("vectorValue").vector4Value = float3Val;
                            }
                            break;
                        }

                        SerializedProperty float3Property = property.FindPropertyRelative("vectorValue");

                        float3Property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, float3Property.isExpanded, Labels.Float3Label);
                        EditorGUI.EndFoldoutHeaderGroup();

                        if(float3Property.isExpanded)
                        {
                            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;

                            EditorGUI.BeginChangeCheck();

                            if(widgetType == SubstanceWidgetType.Slider)
                            {
                                Vector3 float3Min = property.FindPropertyRelative("parameter.rangeMin").vector4Value;
                                Vector3 float3Max = property.FindPropertyRelative("parameter.rangeMax").vector4Value;

                                float3Val.x = EditorGUI.Slider(position, Labels.XLabel, float3Val.x, float3Min.x, float3Max.x);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                float3Val.y = EditorGUI.Slider(position, Labels.YLabel, float3Val.y, float3Min.y, float3Max.y);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                float3Val.z = EditorGUI.Slider(position, Labels.ZLabel, float3Val.z, float3Min.z, float3Max.z);
                            }
                            else
                            {
                                float3Val.x = EditorGUI.FloatField(position, Labels.XLabel, float3Val.x);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                float3Val.y = EditorGUI.FloatField(position, Labels.YLabel, float3Val.y);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                float3Val.z = EditorGUI.FloatField(position, Labels.ZLabel, float3Val.z);
                            }

                            if(EditorGUI.EndChangeCheck())
                            {
                                property.FindPropertyRelative("vectorValue").vector4Value = float3Val;
                            }
                        }
                        break;
                    case SubstanceValueType.Float4:
                        position.Set(position.x + EditorGUIUtility.singleLineHeight, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width - EditorGUIUtility.singleLineHeight, position.height);

                        Vector4 float4Val = property.FindPropertyRelative("vectorValue").vector4Value;

                        if(widgetType == SubstanceWidgetType.Color)
                        {
                            position.Set(position.x - EditorGUIUtility.singleLineHeight, position.y, position.width + EditorGUIUtility.singleLineHeight, position.height);

                            EditorGUI.BeginChangeCheck();
                            float4Val = EditorGUI.ColorField(position, Labels.Float4ColorLabel, float4Val, true, true, false);
                            if(EditorGUI.EndChangeCheck())
                            {
                                property.FindPropertyRelative("vectorValue").vector4Value = float4Val;
                            }
                            break;
                        }

                        SerializedProperty float4Property = property.FindPropertyRelative("vectorValue");

                        float4Property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, float4Property.isExpanded, Labels.Float4Label);
                        EditorGUI.EndFoldoutHeaderGroup();

                        if(float4Property.isExpanded)
                        {
                            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;

                            EditorGUI.BeginChangeCheck();

                            switch(widgetType)
                            {
                                case SubstanceWidgetType.Slider:
                                    Vector4 float4Min = property.FindPropertyRelative("parameter.rangeMin").vector4Value;
                                    Vector4 float4Max = property.FindPropertyRelative("parameter.rangeMax").vector4Value;

                                    float4Val.x = EditorGUI.Slider(position, Labels.XLabel, float4Val.x, float4Min.x, float4Max.x);
                                    position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                    float4Val.y = EditorGUI.Slider(position, Labels.YLabel, float4Val.y, float4Min.y, float4Max.y);
                                    position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                    float4Val.z = EditorGUI.Slider(position, Labels.ZLabel, float4Val.z, float4Min.z, float4Max.z);
                                    position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                    float4Val.w = EditorGUI.Slider(position, Labels.WLabel, float4Val.w, float4Min.w, float4Max.w);
                                    break;
                                default:
                                    float4Val.x = EditorGUI.FloatField(position, Labels.XLabel, float4Val.x);
                                    position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                    float4Val.y = EditorGUI.FloatField(position, Labels.YLabel, float4Val.y);
                                    position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                    float4Val.z = EditorGUI.FloatField(position, Labels.ZLabel, float4Val.z);
                                    position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                    float4Val.w = EditorGUI.FloatField(position, Labels.WLabel, float4Val.w);
                                    break;
                            }

                            if(EditorGUI.EndChangeCheck())
                            {
                                property.FindPropertyRelative("vectorValue").vector4Value = float4Val;
                            }
                        }
                        break;
                    case SubstanceValueType.Int:
                        position.Set(position.x, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width, position.height);
                        int intVal = property.FindPropertyRelative("vectorIntValue.x").intValue;

                        EditorGUI.BeginChangeCheck();

                        switch(widgetType)
                        {
                            case SubstanceWidgetType.ComboBox:
                                Tuple<GUIContent, GUIContent[], int[]> enumLabels = GetEnumLabels(property.FindPropertyRelative("parameter"));

                                EditorGUI.BeginChangeCheck();
                                intVal = EditorGUI.IntPopup(position, enumLabels.Item1, intVal, enumLabels.Item2, enumLabels.Item3);
                                if(EditorGUI.EndChangeCheck())
                                {
                                    property.FindPropertyRelative("vectorIntValue.x").intValue = intVal;
                                }

                                break;
                            case SubstanceWidgetType.ToggleButton:
                                intVal = EditorGUI.Toggle(position, Labels.BoolLabel, intVal != 0) ? 1 : 0;

                                break;
                            case SubstanceWidgetType.Slider:
                                int intMin = property.FindPropertyRelative("parameter.rangeIntMin.x").intValue;
                                int intMax = property.FindPropertyRelative("parameter.rangeIntMax.x").intValue;

                                intVal = EditorGUI.IntSlider(position, Labels.IntLabel, intVal, intMin, intMax);
                                break;
                            default:
                                if(property.FindPropertyRelative("parameter.name").stringValue == NAME_RANDOM_SEED)
                                {
                                    position.width -= 60f;
                                    intVal = EditorGUI.IntField(position, Labels.IntLabel, intVal);

                                    position.Set(position.x + position.width, position.y, 60f, position.height);

                                    if(GUI.Button(position, Labels.ReseedButtonLabel))
                                    {
                                        intVal = Random.Range(int.MinValue, int.MaxValue);
                                        GUI.changed = true;
                                    }
                                    break;
                                }
                                
                                intVal = EditorGUI.IntField(position, Labels.IntLabel, intVal);
                                break;
                        }

                        if(EditorGUI.EndChangeCheck())
                        {
                            property.FindPropertyRelative("vectorIntValue.x").intValue = intVal;
                        }
                        break;
                    case SubstanceValueType.Int2:
                        position.Set(position.x + EditorGUIUtility.singleLineHeight, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width - EditorGUIUtility.singleLineHeight, position.height);

                        SerializedProperty int2Property = property.FindPropertyRelative("vectorIntValue");

                        int2Property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, int2Property.isExpanded, Labels.Int2Label);
                        EditorGUI.EndFoldoutHeaderGroup();

                        if(int2Property.isExpanded)
                        {
                            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                            Vector2Int int2Val = (Vector2Int)int2Property.GetVector4IntValue();

                            EditorGUI.BeginChangeCheck();

                            if(widgetType == SubstanceWidgetType.Slider)
                            {
                                Vector2Int int2Min = (Vector2Int)property.FindPropertyRelative("parameter.rangeIntMin").GetVector4IntValue();
                                Vector2Int int2Max = (Vector2Int)property.FindPropertyRelative("parameter.rangeIntMax").GetVector4IntValue();

                                int2Val.x = EditorGUI.IntSlider(position, Labels.XLabel, int2Val.x, int2Min.x, int2Max.x);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int2Val.y = EditorGUI.IntSlider(position, Labels.YLabel, int2Val.y, int2Min.y, int2Max.y);
                            }
                            else
                            {
                                int2Val.x = EditorGUI.IntField(position, Labels.XLabel, int2Val.x);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int2Val.y = EditorGUI.IntField(position, Labels.YLabel, int2Val.y);
                            }

                            if(EditorGUI.EndChangeCheck())
                            {
                                property.FindPropertyRelative("vectorIntValue").SetVector4IntValue(new Vector4Int(int2Val.x, int2Val.y, 0, 0));
                            }
                        }
                        break;
                    case SubstanceValueType.Int3:
                        position.Set(position.x + EditorGUIUtility.singleLineHeight, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width - EditorGUIUtility.singleLineHeight, position.height);

                        SerializedProperty int3Property = property.FindPropertyRelative("vectorIntValue");

                        int3Property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, int3Property.isExpanded, Labels.Int3Label);
                        EditorGUI.EndFoldoutHeaderGroup();

                        if(int3Property.isExpanded)
                        {
                            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                            Vector3Int int3Val = (Vector3Int)int3Property.GetVector4IntValue();

                            EditorGUI.BeginChangeCheck();

                            if(widgetType == SubstanceWidgetType.Slider)
                            {
                                Vector3Int int3Min = (Vector3Int)property.FindPropertyRelative("parameter.rangeIntMin").GetVector4IntValue();
                                Vector3Int int3Max = (Vector3Int)property.FindPropertyRelative("parameter.rangeIntMax").GetVector4IntValue();

                                int3Val.x = EditorGUI.IntSlider(position, Labels.XLabel, int3Val.x, int3Min.x, int3Max.x);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int3Val.y = EditorGUI.IntSlider(position, Labels.YLabel, int3Val.y, int3Min.y, int3Max.y);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int3Val.z = EditorGUI.IntSlider(position, Labels.WLabel, int3Val.z, int3Min.z, int3Max.z);
                            }
                            else
                            {
                                int3Val.x = EditorGUI.IntField(position, Labels.XLabel, int3Val.x);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int3Val.y = EditorGUI.IntField(position, Labels.YLabel, int3Val.y);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int3Val.z = EditorGUI.IntField(position, Labels.ZLabel, int3Val.z);
                            }

                            if(EditorGUI.EndChangeCheck())
                            {
                                property.FindPropertyRelative("vectorIntValue").SetVector4IntValue(new Vector4Int(int3Val.x, int3Val.y, int3Val.z, 0));
                            }
                        }
                        break;
                    case SubstanceValueType.Int4:
                        position.Set(position.x + EditorGUIUtility.singleLineHeight, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width - EditorGUIUtility.singleLineHeight, position.height);

                        SerializedProperty int4Property = property.FindPropertyRelative("vectorIntValue");

                        int4Property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, int4Property.isExpanded, Labels.Int4Label);
                        EditorGUI.EndFoldoutHeaderGroup();

                        if(int4Property.isExpanded)
                        {
                            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                            Vector4Int int4Val = int4Property.GetVector4IntValue();
                            Vector4Int int4Min = property.FindPropertyRelative("parameter.rangeIntMin").GetVector4IntValue();
                            Vector4Int int4Max = property.FindPropertyRelative("parameter.rangeIntMax").GetVector4IntValue();

                            EditorGUI.BeginChangeCheck();

                            if(widgetType == SubstanceWidgetType.Slider && int4Min != int4Max)
                            {
                                int4Val.x = EditorGUI.IntSlider(position, Labels.XLabel, int4Val.x, int4Min.x, int4Max.x);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int4Val.y = EditorGUI.IntSlider(position, Labels.YLabel, int4Val.y, int4Min.y, int4Max.y);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int4Val.z = EditorGUI.IntSlider(position, Labels.WLabel, int4Val.z, int4Min.z, int4Max.z);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int4Val.w = EditorGUI.IntSlider(position, Labels.WLabel, int4Val.w, int4Min.w, int4Max.w);
                            }
                            else
                            {
                                int4Val.x = EditorGUI.IntField(position, Labels.XLabel, int4Val.x);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int4Val.y = EditorGUI.IntField(position, Labels.YLabel, int4Val.y);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int4Val.z = EditorGUI.IntField(position, Labels.ZLabel, int4Val.z);
                                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                                int4Val.w = EditorGUI.IntField(position, Labels.WLabel, int4Val.w);
                            }

                            if(EditorGUI.EndChangeCheck())
                            {
                                property.FindPropertyRelative("vectorIntValue").SetVector4IntValue(int4Val);
                            }
                        }
                        break;
                    case SubstanceValueType.Image:
                        position.Set(position.x, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width, position.height);
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("textureValue"), Labels.TextureLabel);
                        break;
                    case SubstanceValueType.String:
                        position.Set(position.x, position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUI.GetPropertyHeight(property.FindPropertyRelative("stringValue")));
                        EditorGUI.PropertyField(position, property.FindPropertyRelative("stringValue"), Labels.TextureLabel);
                        break;
                }
            }
        }

        private Tuple<GUIContent, GUIContent[], int[]> GetEnumLabels(SerializedProperty parameterProperty)
        {
            return GetEnumLabels(parameterProperty.FindPropertyRelative("guid").stringValue, parameterProperty.FindPropertyRelative("graphId").intValue, parameterProperty.FindPropertyRelative("index").intValue);
        }

        private Tuple<GUIContent, GUIContent[], int[]> GetEnumLabels(string guid, int graphIndex, int parameterIndex)
        {
            bool containsGraph = EnumLabels.TryGetValue(guid, out Dictionary<int, Dictionary<int, GUIContent[]>> graphs);

            if(!containsGraph)
            {
                graphs = new Dictionary<int, Dictionary<int, GUIContent[]>>();
            }

            bool containsIndex = graphs.TryGetValue(graphIndex, out Dictionary<int, GUIContent[]> indexes);

            if(!containsIndex)
            {
                indexes = new Dictionary<int, GUIContent[]>();
            }

            bool containsEnum = indexes.TryGetValue(parameterIndex, out GUIContent[] labels);
            int[] enumLabelValues;
            GUIContent label;

            if(!containsEnum)
            {
                SubstanceMaterialInstanceSO targetSubstance = AssetDatabase.LoadAssetAtPath<SubstanceMaterialInstanceSO>(AssetDatabase.GUIDToAssetPath(guid));
                ISubstanceInput input = targetSubstance.Graphs[graphIndex].Input[parameterIndex];

                if(!input.IsNumeric ||
                    !(input.NumericalDescription is SubstanceInputDescNumericalInt) ||
                    ((SubstanceInputDescNumericalInt)input.NumericalDescription).EnumValueCount == 0)
                {
                    labels = Defaults.DefaultEnumLabels;
                    enumLabelValues = Defaults.DefaultEnumLabelValues;
                    label = Labels.DefaultEnumLabel;
                }
                else
                {
                    SubstanceInputDescNumericalInt desc = (SubstanceInputDescNumericalInt)input.NumericalDescription;
                    labels = new GUIContent[desc.EnumValueCount];
                    enumLabelValues = new int[labels.Length];
                    StringBuilder tooltip = new StringBuilder("Enum value for the parameter. (This is a convenience wrapper for an Int property)\n");

                    for(int i = 0; i < desc.EnumValueCount; i++)
                    {
                        labels[i] = new GUIContent(desc.EnumValues[i].Label);
                        enumLabelValues[i] = desc.EnumValues[i].Value;
                        tooltip.Append(string.Format("\n{0} : {1}", labels[i], enumLabelValues[i]));
                    }

                    label = new GUIContent("Value", tooltip.ToString());
                }

                indexes.Add(parameterIndex, labels);

                if(!containsGraph)
                {
                    EnumValues.Add(guid, new Dictionary<int, Dictionary<int, int[]>>());
                    EnumFieldLabels.Add(guid, new Dictionary<int, Dictionary<int, GUIContent>>());
                }

                if(!containsIndex)
                {
                    EnumValues[guid].Add(graphIndex, new Dictionary<int, int[]>());
                    EnumFieldLabels[guid].Add(graphIndex, new Dictionary<int, GUIContent>());
                }

                EnumValues[guid][graphIndex].Add(parameterIndex, enumLabelValues);
                EnumFieldLabels[guid][graphIndex].Add(parameterIndex, label);
            }
            else
            {
                enumLabelValues = EnumValues[guid][graphIndex][parameterIndex];
                label = EnumFieldLabels[guid][graphIndex][parameterIndex];
            }

            if(!containsIndex)
            {
                graphs.Add(graphIndex, indexes);
            }

            if(!containsGraph)
            {
                EnumLabels.Add(guid, graphs);
            }

            return Tuple.Create(label, labels, enumLabelValues);
        }
    }
}