using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Convenience struct allowing substance input parameter values to be set up in the inspector.
    /// </summary>
    [System.Serializable]
    public struct SubstanceParameterValue
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

        /// <summary>
        /// Parameter being referenced.
        /// </summary>
        public SubstanceParameter Parameter
        {
            get { return parameter; }
        }

        /// <summary>
        /// Name for the input parameter associated with this value.
        /// </summary>
        public string Name
        {
            get { return parameter.Name; }
        }


        /// <summary>
        /// GUID for the <see cref="SubstanceGraphSO"/> containing the target parameter.
        /// </summary>
        /*public string GraphGuid
        {
            get { return parameter.GraphGuid; }
        }*/

        /// <summary>
        /// Index for the input parameter associated with this value.
        /// </summary>
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

        /// <summary>
        /// Value type for the input parameter associated with this value.
        /// </summary>
        public SubstanceValueType Type
        {
            get { return parameter.Type; }
        }

        /// <summary>
        /// Inspector widget used for the input parameter associated with this value.
        /// </summary>
        public SubstanceWidgetType WidgetType
        {
            get { return parameter.WidgetType; }
        }
        
        /// <summary>
        /// Bool value for the target input parameter.
        /// </summary>
        public bool BoolValue
        {
            get { return vectorIntValue.x != 0; }
            set { vectorIntValue.x = value ? 1 : 0; }
        }

        /// <summary>
        /// Int value for the target input parameter.
        /// </summary>
        public int IntValue
        {
            get { return vectorIntValue.x; }
            set { vectorIntValue.x = value; }
        }

        /// <summary>
        /// Int2 value for the target input parameter.
        /// </summary>
        public Vector2Int Int2Value
        {
            get { return (Vector2Int)vectorIntValue; }
            set { vectorIntValue = new Vector4Int(value.x, value.y, 0, 0); }
        }

        /// <summary>
        /// Int3 value for the target input parameter.
        /// </summary>
        public Vector3Int Int3Value
        {
            get { return (Vector3Int)vectorIntValue; }
            set { vectorIntValue = new Vector4Int(value.x, value.y, value.z, 0); }
        }

        /// <summary>
        /// Int4 value for the target input parameter.
        /// </summary>
        public Vector4Int Int4Value
        {
            get { return vectorIntValue; }
            set { vectorIntValue = value; }
        }

        /// <summary>
        /// Float value for the target input parameter.
        /// </summary>
        public float FloatValue
        {
            get { return vectorValue.x; }
            set { vectorValue.x = value; }
        }

        /// <summary>
        /// Float2 value for the target input parameter.
        /// </summary>
        public Vector2 Float2Value
        {
            get { return vectorValue; }
            set { vectorValue = value; }
        }

        /// <summary>
        /// Float3 value for the target input parameter.
        /// </summary>
        public Vector3 Float3Value
        {
            get { return vectorValue; }
            set { vectorValue = value; }
        }

        /// <summary>
        /// Float4 value for the target input parameter.
        /// </summary>
        public Vector4 Float4Value
        {
            get { return vectorValue; }
            set { vectorValue = value; }
        }

        /// <summary>
        /// Color value for the target input parameter.
        /// </summary>
        public Color ColorValue
        {
            get { return vectorValue; }
            set { vectorValue = value; }
        }

        /// <summary>
        /// String value for the target input parameter.
        /// </summary>
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

        /// <summary>
        /// Texture value for the target input parameter.
        /// </summary>
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

        /// <summary>
        /// Returns the input on the given <see cref="SubstanceGraphSO"/> targeted by this parameter.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the target input from.</param>
        public ISubstanceInput GetInput(SubstanceGraphSO substance)
        {
            return substance.Input[Index];
        }

        /// <summary>
        /// Returns the input on the given <see cref="SubstanceGraphSO"/> targeted by this parameter.
        /// </summary>
        /// <typeparam name="T">Expected type for the input data.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the target input from.</param>
        public T GetInput<T>(SubstanceGraphSO substance) where T : ISubstanceInput
        {
            return (T)GetInput(substance);
        }

        /// <summary>
        /// Update the given handler with this parameter's values.
        /// </summary>
        /// <param name="handler">Handler to update values on.</param>
        /// <returns>True if the handler has the target parameter set.</returns>
        public bool SetValue(SubstanceNativeGraph handler)
        {
            switch(Type)
            {
                case SubstanceValueType.Float:
                    handler.SetInputFloat(Index, FloatValue);
                    return true;
                case SubstanceValueType.Float2:
                    handler.SetInputFloat2(Index, Float2Value);
                    return true;
                case SubstanceValueType.Float3:
                    handler.SetInputFloat3(Index, Float3Value);
                    return true;
                case SubstanceValueType.Float4:
                    handler.SetInputFloat4(Index, Float4Value);
                    return true;
                case SubstanceValueType.Int:
                    handler.SetInputInt(Index, IntValue);
                    return true;
                case SubstanceValueType.Int2:
                    handler.SetInputInt2(Index, Int2Value);
                    return true;
                case SubstanceValueType.Int3:
                    handler.SetInputInt3(Index, Int3Value);
                    return true;
                case SubstanceValueType.Int4:
                    handler.SetInputInt4(Index, Int4Value.x, Int4Value.y, Int4Value.z, Int4Value.w);
                    return true;
                case SubstanceValueType.String:
                    handler.SetInputString(Index, StringValue);
                    return true;
                case SubstanceValueType.Image:
                    Debug.LogWarning("Broken in 0.0.100...");
                    //handler.SetInputTexture2D(TextureValue, Index, GraphId);
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Update the given substance with this parameter's values.
        /// </summary>
        /// <param name="substance">Substance to update values on.</param>
        /// <returns>True if the substance has the target parameter set.</returns>
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
                    Debug.LogWarning("Broken in 0.0.100...");
                    //textureInput.Data = TextureValue;
                    return true;
            }

            return false;
        }
    }
}