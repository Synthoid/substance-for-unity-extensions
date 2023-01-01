using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Convenience struct allowing substance input parameter values to be set up in the inspector.
    /// </summary>
    [System.Serializable]
    public struct SubstanceParameterValue : ISubstanceInputParameterValue
    {
        #region Fields

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

        #endregion

        #region Properties

        public ISubstanceInputParameter Parameter
        {
            get { return parameter; }
        }

        public string Name
        {
            get { return parameter.Name; }
        }

        public int Index
        {
            get { return parameter.Index; }
        }

#if UNITY_EDITOR
        /// <summary>
        /// [Editor Only] <see cref="SubstanceGraphSO"/> asset referenced for input values.
        /// </summary>
        public SubstanceGraphSO EditorAsset
        {
            get { return parameter.EditorAsset; }
        }
#endif

#if UNITY_EDITOR
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        [System.Obsolete("Use ValueType instead.")]
        public SubstanceValueType Type
        {
            get { return parameter.ValueType; }
        }

        public SubstanceValueType ValueType
        {
            get { return parameter.ValueType; }
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

        #endregion

        #region Constructors

        public SubstanceParameterValue(SubstanceParameter parameter, bool value)
        {
            this.parameter = parameter;
            this.vectorValue = Vector4.zero;
            this.vectorIntValue = new Vector4Int(value ? 1 : 0);
            this.stringValue = "";
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, int value)
        {
            this.parameter = parameter;
            this.vectorValue = Vector4.zero;
            this.vectorIntValue = new Vector4Int(value);
            this.stringValue = "";
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, Vector2Int value)
        {
            this.parameter = parameter;
            this.vectorValue = Vector4.zero;
            this.vectorIntValue = new Vector4Int(value.x, value.y);
            this.stringValue = "";
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, Vector3Int value)
        {
            this.parameter = parameter;
            this.vectorValue = Vector4.zero;
            this.vectorIntValue = new Vector4Int(value.x, value.y, value.z);
            this.stringValue = "";
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, Vector4Int value)
        {
            this.parameter = parameter;
            this.vectorValue = Vector4.zero;
            this.vectorIntValue = value;
            this.stringValue = "";
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, int[] value)
        {
            this.parameter = parameter;
            this.vectorValue = Vector4.zero;
            this.stringValue = "";
            this.textureValue = null;

            if(value == null || value.Length == 0)
            {
                this.vectorIntValue = Vector4Int.zero;
            }
            else if (value.Length >= 4)
            {
                this.vectorIntValue = new Vector4Int(value[0], value[1], value[2], value[3]);
            }
            else if(value.Length == 3)
            {
                this.vectorIntValue = new Vector4Int(value[0], value[1], value[2], 0);
            }
            else if (value.Length == 2)
            {
                this.vectorIntValue = new Vector4Int(value[0], value[1], 0, 0);
            }
            else
            {
                this.vectorIntValue = new Vector4Int(value[0], 0, 0, 0);
            }
        }

        public SubstanceParameterValue(SubstanceParameter parameter, float value)
        {
            this.parameter = parameter;
            this.vectorValue = new Vector4(value, 0f, 0f, 0f);
            this.vectorIntValue = Vector4Int.zero;
            this.stringValue = "";
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, Vector2 value)
        {
            this.parameter = parameter;
            this.vectorValue = value;
            this.vectorIntValue = Vector4Int.zero;
            this.stringValue = "";
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, Vector3 value)
        {
            this.parameter = parameter;
            this.vectorValue = value;
            this.vectorIntValue = Vector4Int.zero;
            this.stringValue = "";
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, Vector4 value)
        {
            this.parameter = parameter;
            this.vectorValue = value;
            this.vectorIntValue = Vector4Int.zero;
            this.stringValue = "";
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, Color value)
        {
            this.parameter = parameter;
            this.vectorValue = value;
            this.vectorIntValue = Vector4Int.zero;
            this.stringValue = "";
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, string value)
        {
            this.parameter = parameter;
            this.vectorValue = Vector4.zero;
            this.vectorIntValue = Vector4Int.zero;
            this.stringValue = value;
            this.textureValue = null;
        }

        public SubstanceParameterValue(SubstanceParameter parameter, Texture2D value)
        {
            this.parameter = parameter;
            this.vectorValue = Vector4.zero;
            this.vectorIntValue = Vector4Int.zero;
            this.stringValue = "";
            this.textureValue = value;
        }

        #endregion


        public ISubstanceInput GetInput(SubstanceGraphSO substance)
        {
            return substance.Input[Index];
        }


        public T GetInput<T>(SubstanceGraphSO substance) where T : ISubstanceInput
        {
            return (T)GetInput(substance);
        }


        public void SetValue(SubstanceNativeGraph nativeGraph)
        {
            switch(ValueType)
            {
                case SubstanceValueType.Float:
                    nativeGraph.SetInputFloat(Index, FloatValue);
                    break;
                case SubstanceValueType.Float2:
                    nativeGraph.SetInputFloat2(Index, Float2Value);
                    break;
                case SubstanceValueType.Float3:
                    nativeGraph.SetInputFloat3(Index, Float3Value);
                    break;
                case SubstanceValueType.Float4:
                    nativeGraph.SetInputFloat4(Index, Float4Value);
                    break;
                case SubstanceValueType.Int:
                    nativeGraph.SetInputInt(Index, IntValue);
                    break;
                case SubstanceValueType.Int2:
                    nativeGraph.SetInputInt2(Index, Int2Value);
                    break;
                case SubstanceValueType.Int3:
                    nativeGraph.SetInputInt3(Index, Int3Value);
                    break;
                case SubstanceValueType.Int4:
                    nativeGraph.SetInputInt4(Index, Int4Value);
                    break;
                case SubstanceValueType.String:
                    nativeGraph.SetInputString(Index, StringValue);
                    break;
                case SubstanceValueType.Image:
                    if(TextureValue == null) nativeGraph.SetInputTexture2DNull(Index);
                    else nativeGraph.SetInputTextureCPU(Index, TextureValue);
                    break;
            }
        }


        public bool SetValue(SubstanceGraphSO substance)
        {
            ISubstanceInput input = substance.GetInput(Index);

            switch(input)
            {
                case SubstanceInputFloat floatInput:
                    floatInput.Data = FloatValue;
                    return true;
                case SubstanceInputFloat2 float2Input:
                    float2Input.Data = Float2Value;
                    return true;
                case SubstanceInputFloat3 float3Input:
                    float3Input.Data = Float3Value;
                    return true;
                case SubstanceInputFloat4 float4Input:
                    float4Input.Data = Float4Value;
                    return true;
                case SubstanceInputInt intInput:
                    intInput.Data = IntValue;
                    return true;
                case SubstanceInputInt2 int2Input:
                    int2Input.Data = Int2Value;
                    return true;
                case SubstanceInputInt3 int3Input:
                    int3Input.Data = Int3Value;
                    return true;
                case SubstanceInputInt4 int4Input:
                    int4Input.Data0 = Int4Value.x;
                    int4Input.Data1 = Int4Value.y;
                    int4Input.Data2 = Int4Value.z;
                    int4Input.Data3 = Int4Value.w;
                    return true;
                case SubstanceInputString stringInput:
                    stringInput.Data = StringValue;
                    return true;
                case SubstanceInputTexture textureInput:
                    //Since the texture variable is private, need to use reflection to set the graph input's texture value.
                    textureInput.SetTexture(TextureValue);
                    return false;
            }

            return false;
        }


        public async Task SetValueAsync(SubstanceNativeGraph nativeGraph)
        {
            switch(ValueType)
            {
                case SubstanceValueType.Float:
                    nativeGraph.SetInputFloat(Index, FloatValue);
                    break;
                case SubstanceValueType.Float2:
                    nativeGraph.SetInputFloat2(Index, Float2Value);
                    break;
                case SubstanceValueType.Float3:
                    nativeGraph.SetInputFloat3(Index, Float3Value);
                    break;
                case SubstanceValueType.Float4:
                    nativeGraph.SetInputFloat4(Index, Float4Value);
                    break;
                case SubstanceValueType.Int:
                    nativeGraph.SetInputInt(Index, IntValue);
                    break;
                case SubstanceValueType.Int2:
                    if (Name == SubstanceNativeGraphExtensions.kOutputSizeIdentifier)
                    {
                        nativeGraph.SetOutputSize(Index, Int2Value);
                    }
                    else
                    {
                        nativeGraph.SetInputInt2(Index, Int2Value);
                    }
                    break;
                case SubstanceValueType.Int3:
                    nativeGraph.SetInputInt3(Index, Int3Value);
                    break;
                case SubstanceValueType.Int4:
                    nativeGraph.SetInputInt4(Index, Int4Value.x, Int4Value.y, Int4Value.z, Int4Value.w);
                    break;
                case SubstanceValueType.String:
                    nativeGraph.SetInputString(Index, StringValue);
                    break;
                case SubstanceValueType.Image:
                    if (TextureValue == null)
                    {
                        nativeGraph.SetInputTexture2DNull(Index);
                    }
                    else
                    {
                        await nativeGraph.SetInputTextureGPUAsync(Index, TextureValue);
                    }
                    break;
            }
        }
    }
}