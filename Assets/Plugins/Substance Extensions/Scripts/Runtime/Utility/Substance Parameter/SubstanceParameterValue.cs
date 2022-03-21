using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    [System.Serializable]
    public struct SubstanceParameterValue
    {
        /// <summary>
        /// Parameter being referenced.
        /// </summary>
        [SerializeField, Tooltip("Parameter being referenced.")]
        private SubstanceParameter parameter;
        /// <summary>
        /// Vector used to store float values for the parameter.
        /// </summary>
        [SerializeField]
        private Vector4 vectorValue;
        /// <summary>
        /// Vector used to store int values for the parameter.
        /// </summary>
        [SerializeField]
        private Vector4Int vectorIntValue;
        /// <summary>
        /// String value for the parameter.
        /// </summary>
        [SerializeField, TextArea(3, 5)]
        private string stringValue;
        /// <summary>
        /// Texture value for the parameter.
        /// </summary>
        [SerializeField]
        private Texture2D textureValue;

        public string Name
        {
            get { return parameter.Name; }
        }

        public int GraphId
        {
            get { return parameter.GraphId; }
        }

        public int Index
        {
            get { return parameter.Index; }
        }

        public SubstanceValueType Type
        {
            get { return parameter.Type; }
        }

        public SubstanceWidgetType WidgetType
        {
            get { return parameter.WidgetType; }
        }
        
        public bool BoolValue
        {
            get { return vectorIntValue.x != 0; }
            set { vectorIntValue.x = value ? 1 : 0; }
        }

        public int IntValue
        {
            get { return vectorIntValue.x; }
            set { vectorIntValue.x = value; }
        }

        public Vector2Int Int2Value
        {
            get { return (Vector2Int)vectorIntValue; }
            set { vectorIntValue = new Vector4Int(value.x, value.y, 0, 0); }
        }

        public Vector3Int Int3Value
        {
            get { return (Vector3Int)vectorIntValue; }
            set { vectorIntValue = new Vector4Int(value.x, value.y, value.z, 0); }
        }

        public Vector4Int Int4Value
        {
            get { return vectorIntValue; }
            set { vectorIntValue = value; }
        }

        public float FloatValue
        {
            get { return vectorValue.x; }
            set { vectorValue.x = value; }
        }

        public Vector2 Float2Value
        {
            get { return vectorValue; }
            set { vectorValue = value; }
        }

        public Vector3 Float3Value
        {
            get { return vectorValue; }
            set { vectorValue = value; }
        }

        public Vector4 Float4Value
        {
            get { return vectorValue; }
            set { vectorValue = value; }
        }

        public Color ColorValue
        {
            get { return vectorValue; }
            set { vectorValue = value; }
        }

        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

        public Texture2D TextureValue
        {
            get { return textureValue; }
            set { textureValue = value; }
        }

        public bool SetValue(SubstanceNativeHandler graphHandler)
        {
            switch(Type)
            {
                case SubstanceValueType.Float:
                    graphHandler.SetInputFloat(FloatValue, Index, GraphId);
                    break;
                case SubstanceValueType.Float2:
                    graphHandler.SetInputFloat2(Float2Value, Index, GraphId);
                    break;
                case SubstanceValueType.Float3:
                    graphHandler.SetInputFloat3(Float3Value, Index, GraphId);
                    break;
                case SubstanceValueType.Float4:
                    graphHandler.SetInputFloat4(Float4Value, Index, GraphId);
                    break;
                case SubstanceValueType.Int:
                    graphHandler.SetInputInt(IntValue, Index, GraphId);
                    break;
                case SubstanceValueType.Int2:
                    graphHandler.SetInputInt2(Int2Value, Index, GraphId);
                    break;
                case SubstanceValueType.Int3:
                    graphHandler.SetInputInt3(Int3Value, Index, GraphId);
                    break;
                case SubstanceValueType.Int4:
                    graphHandler.SetInputInt4(Int4Value.x, Int4Value.y, Int4Value.z, Int4Value.w, Index, GraphId);
                    break;
                case SubstanceValueType.String:
                    graphHandler.SetInputString(StringValue, Index, GraphId);
                    break;
                case SubstanceValueType.Image:
                    graphHandler.SetInputTexture2D(TextureValue, Index, GraphId);
                    break;
            }

            return false;
        }
    }
}