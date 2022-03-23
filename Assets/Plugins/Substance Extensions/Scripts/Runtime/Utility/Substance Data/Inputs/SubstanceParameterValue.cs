using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

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

        /// <summary>
        /// Returns the input on the given <see cref="SubstanceMaterialInstanceSO"/> targeted by this parameter.
        /// </summary>
        /// <param name="graph"><see cref="SubstanceMaterialInstanceSO"/> to obtain the target input from.</param>
        public ISubstanceInput GetInput(SubstanceMaterialInstanceSO graph)
        {
            return graph.Graphs[GraphId].Input[Index];
        }

        /// <summary>
        /// Returns the input on the given <see cref="SubstanceMaterialInstanceSO"/> targeted by this parameter.
        /// </summary>
        /// <typeparam name="T">Expected type for the input data.</typeparam>
        /// <param name="graph"><see cref="SubstanceMaterialInstanceSO"/> to obtain the target input from.</param>
        public T GetInput<T>(SubstanceMaterialInstanceSO graph) where T : ISubstanceInput
        {
            return (T)GetInput(graph);
        }


        public bool SetValue(SubstanceNativeHandler handler)
        {
            switch(Type)
            {
                case SubstanceValueType.Float:
                    handler.SetInputFloat(FloatValue, Index, GraphId);
                    break;
                case SubstanceValueType.Float2:
                    handler.SetInputFloat2(Float2Value, Index, GraphId);
                    break;
                case SubstanceValueType.Float3:
                    handler.SetInputFloat3(Float3Value, Index, GraphId);
                    break;
                case SubstanceValueType.Float4:
                    handler.SetInputFloat4(Float4Value, Index, GraphId);
                    break;
                case SubstanceValueType.Int:
                    handler.SetInputInt(IntValue, Index, GraphId);
                    break;
                case SubstanceValueType.Int2:
                    handler.SetInputInt2(Int2Value, Index, GraphId);
                    break;
                case SubstanceValueType.Int3:
                    handler.SetInputInt3(Int3Value, Index, GraphId);
                    break;
                case SubstanceValueType.Int4:
                    handler.SetInputInt4(Int4Value.x, Int4Value.y, Int4Value.z, Int4Value.w, Index, GraphId);
                    break;
                case SubstanceValueType.String:
                    handler.SetInputString(StringValue, Index, GraphId);
                    break;
                case SubstanceValueType.Image:
                    handler.SetInputTexture2D(TextureValue, Index, GraphId);
                    break;
            }

            return false;
        }


        public bool SetValue(SubstanceMaterialInstanceSO substance)
        {
            ISubstanceInput input = substance.GetInput(Index, GraphId);

            switch(input)
            {
                case SubstanceInputFloat floatInput:
                    floatInput.Data = FloatValue;
                    break;
                case SubstanceInputFloat2 float2Input:
                    float2Input.Data = Float2Value;
                    break;
                case SubstanceInputFloat3 float3Input:
                    float3Input.Data = Float3Value;
                    break;
                case SubstanceInputFloat4 float4Input:
                    float4Input.Data = Float4Value;
                    break;
                case SubstanceInputInt intInput:
                    intInput.Data = IntValue;
                    break;
                case SubstanceInputInt2 int2Input:
                    int2Input.Data = Int2Value;
                    break;
                case SubstanceInputInt3 int3Input:
                    int3Input.Data = Int3Value;
                    break;
                case SubstanceInputInt4 int4Input:
                    int4Input._Data0 = Int4Value.x;
                    int4Input._Data1 = Int4Value.y;
                    int4Input._Data2 = Int4Value.z;
                    int4Input._Data3 = Int4Value.w;
                    break;
                case SubstanceInputString stringInput:
                    stringInput.Data = StringValue;
                    break;
                case SubstanceInputTexture textureInput:
                    textureInput.Data = TextureValue;
                    break;
            }

            return false;
        }
    }
}