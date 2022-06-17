using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Contains extension methods for <see cref="SubstanceFileSO"/>.
    /// </summary>
    public static class SubstanceFileExtensions
    {
        public const string PARAM_OUTPUT_SIZE = "$outputsize";
        public const string PARAM_RANDOM_SEED = "$randomseed";

        public static List<ISubstanceInput> GetInputs(this SubstanceFileSO substance)
        {
            List<ISubstanceInput> inputs = new List<ISubstanceInput>();

            for(int i=0; i < substance.Instances.Count; i++)
            {
                inputs.AddRange(substance.Instances[i].Input);
            }

            return inputs;
        }


        public static ISubstanceInput GetInput(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetInput(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static ISubstanceInput GetInput(this SubstanceFileSO substance, int index, int graphId=0)
        {
            if(graphId < 0 || graphId >= substance.Instances.Count ||
                index < 0 || index >= substance.Instances[graphId].Input.Count)
            {
                return null;
            }

            return substance.Instances[graphId].Input[index];
        }


        public static T GetInput<T>(this SubstanceFileSO substance, string name, int graphId=0) where T : ISubstanceInput
        {
            return GetInput<T>(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static T GetInput<T>(this SubstanceFileSO substance, int index, int graphId=0) where T : ISubstanceInput
        {
            if(graphId < 0 || graphId >= substance.Instances.Count ||
                index < 0 || index >= substance.Instances[graphId].Input.Count)
            {
                return default(T);
            }

            return (T)substance.Instances[graphId].Input[index];
        }


        public static Tuple<int, int> GetGraphAndInputIndexes(this SubstanceFileSO substance, string name)
        {
            for(int i=0; i < substance.Instances.Count; i++)
            {
                for(int j=0; j < substance.Instances[i].Input.Count; j++)
                {
                    if(substance.Instances[i].Input[j].Description.Identifier == name)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }

            return Tuple.Create(-1, -1);
        }


        public static int GetInputIndex(this SubstanceFileSO substance, string name, int graphId=0)
        {
            if(graphId < 0 || graphId >= substance.Instances.Count) return -1;

            for(int i=0; i < substance.Instances[graphId].Input.Count; i++)
            {
                if(substance.Instances[graphId].Input[i].Description.Identifier == name)
                {
                    return i;
                }
            }

            return -1;
        }

        #region Get/Set Values

        /// <summary>
        /// Get the generic value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>object represnting the target input's value.</returns>
        public static object GetValue(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetValue(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the genric value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>object represnting the target input's value.</returns>
        public static object GetValue(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetValue(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the generic value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>object represnting the target input's value.</returns>
        public static object GetValue(this SubstanceFileSO substance, int index, int graphId=0)
        {
            ISubstanceInput input = GetInput(substance, index, graphId);

            switch(input)
            {
                case SubstanceInputFloat floatInput: return floatInput.Data;
                case SubstanceInputFloat2 float2Input: return float2Input.Data;
                case SubstanceInputFloat3 float3Input: return float3Input.Data;
                case SubstanceInputFloat4 float4Input: return float4Input.Data;
                case SubstanceInputInt intInput: return intInput.Data;
                case SubstanceInputInt2 int2Input: return int2Input.Data;
                case SubstanceInputInt3 int3Input: return int3Input.Data;
                case SubstanceInputInt4 int4Input: return new Vector4Int(int4Input.Data0, int4Input.Data1, int4Input.Data2, int4Input.Data3);
                case SubstanceInputString stringInput: return stringInput.Data;
                case SubstanceInputTexture textureInput: return GetTextureInternal(textureInput);
            }

            return null;
        }

        /// <summary>
        /// Attempt to get a generic value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetValue(this SubstanceFileSO substance, out object value, SubstanceParameter inputParameter)
        {
            return TryGetValue(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get a generic value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetValue(this SubstanceFileSO substance, out object value, string name, int graphId=0)
        {
            return TryGetValue(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get a generic value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetValue(this SubstanceFileSO substance, out object value, int index, int graphId=0)
        {
            ISubstanceInput input = GetInput(substance, index, graphId);

            if(input != null)
            {
                switch(input)
                {
                    case SubstanceInputFloat floatInput:
                        value = floatInput.Data;
                        break;
                    case SubstanceInputFloat2 float2Input:
                        value = float2Input.Data;
                        break;
                    case SubstanceInputFloat3 float3Input:
                        value = float3Input.Data;
                        break;
                    case SubstanceInputFloat4 float4Input:
                        value = float4Input.Data;
                        break;
                    case SubstanceInputInt intInput:
                        value = intInput.Data;
                        break;
                    case SubstanceInputInt2 int2Input:
                        value = int2Input.Data;
                        break;
                    case SubstanceInputInt3 int3Input:
                        value = int3Input.Data;
                        break;
                    case SubstanceInputInt4 int4Input:
                        value = new Vector4Int(int4Input.Data0, int4Input.Data1, int4Input.Data2, int4Input.Data3);
                        break;
                    case SubstanceInputString stringInput:
                        value = stringInput.Data;
                        break;
                    case SubstanceInputTexture textureInput: value = GetTextureInternal(textureInput);
                        break;
                    default:
                        value = null;
                        break;
                }

                return true;
            }

            value = null;

            return false;
        }

        /// <summary>
        /// Set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetValue(this SubstanceFileSO substance, object value, SubstanceParameter inputParameter)
        {
            SetValue(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetValue(this SubstanceFileSO substance, object value, string name, int graphId=0)
        {
            SetValue(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetValue(this SubstanceFileSO substance, object value, int index, int graphId=0)
        {
            ISubstanceInput input = GetInput(substance, index, graphId);

            switch(input)
            {
                case SubstanceInputFloat floatInput:
                    floatInput.Data = (float)value;
                    break;
                case SubstanceInputFloat2 float2Input:
                    float2Input.Data = (Vector2)value;
                    break;
                case SubstanceInputFloat3 float3Input:
                    float3Input.Data = (Vector3)value;
                    break;
                case SubstanceInputFloat4 float4Input:
                    float4Input.Data = (Vector4)value;
                    break;
                case SubstanceInputInt intInput:
                    intInput.Data = (int)value;
                    break;
                case SubstanceInputInt2 int2Input:
                    int2Input.Data = (Vector2Int)value;
                    break;
                case SubstanceInputInt3 int3Input:
                    int3Input.Data = (Vector3Int)value;
                    break;
                case SubstanceInputInt4 int4Input:
                    Vector4Int int4Value = (Vector4Int)value;
                    int4Input.Data0 = int4Value.x;
                    int4Input.Data1 = int4Value.y;
                    int4Input.Data2 = int4Value.z;
                    int4Input.Data3 = int4Value.w;
                    break;
                case SubstanceInputString stringInput:
                    stringInput.Data = (string)value;
                    break;
                case SubstanceInputTexture textureInput:
                    SetTextureInternal(textureInput, (Texture2D)value);
                    break;
            }
        }

        /// <summary>
        /// Attempt to set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetValue(this SubstanceFileSO substance, object value, SubstanceParameter inputParameter)
        {
            return TrySetValue(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetValue(this SubstanceFileSO substance, object value, string name, int graphId=0)
        {
            return TrySetValue(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetValue(this SubstanceFileSO substance, object value, int index, int graphId=0)
        {
            ISubstanceInput input = GetInput(substance, index, graphId);

            if(input == null) return false;

            switch(input)
            {
                case SubstanceInputFloat floatInput:
                    if(!(value is float)) return false;

                    floatInput.Data = (float)value;
                    break;
                case SubstanceInputFloat2 float2Input:
                    if(!(value is Vector2)) return false;

                    float2Input.Data = (Vector2)value;
                    break;
                case SubstanceInputFloat3 float3Input:
                    if(!(value is Vector3)) return false;

                    float3Input.Data = (Vector3)value;
                    break;
                case SubstanceInputFloat4 float4Input:
                    if(!(value is Vector4)) return false;

                    float4Input.Data = (Vector4)value;
                    break;
                case SubstanceInputInt intInput:
                    if(!(value is int)) return false;

                    intInput.Data = (int)value;
                    break;
                case SubstanceInputInt2 int2Input:
                    if(!(value is Vector2Int)) return false;

                    int2Input.Data = (Vector2Int)value;
                    break;
                case SubstanceInputInt3 int3Input:
                    if(!(value is Vector3Int)) return false;

                    int3Input.Data = (Vector3Int)value;
                    break;
                case SubstanceInputInt4 int4Input:
                    if(!(value is Vector4Int)) return false;

                    Vector4Int int4Value = (Vector4Int)value;
                    int4Input.Data0 = int4Value.x;
                    int4Input.Data1 = int4Value.y;
                    int4Input.Data2 = int4Value.z;
                    int4Input.Data3 = int4Value.w;
                    break;
                case SubstanceInputString stringInput:
                    if(!(value is string)) return false;

                    stringInput.Data = (string)value;
                    break;
                case SubstanceInputTexture textureInput:
                    if(!(value is Texture2D)) return false;

                    SetTextureInternal(textureInput, (Texture2D)value);
                    break;
                default:
                    Debug.LogWarning("Input type not recognized!");
                    return false;
            }

            return true;
        }

        #region Texture

        private static FieldInfo textureDataField = null;

        private static FieldInfo TextureDataField
        {
            get
            {
                if(textureDataField == null)
                {
                    Type inputType = typeof(SubstanceInputTexture);
                    textureDataField = inputType.GetField("Data", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                }

                return textureDataField;
            }
        }

        /// <summary>
        /// Get the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Texture2D"/> represnting the target input's value.</returns>
        public static Texture2D GetTexture(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetTexture(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Texture2D"/> represnting the target input's value.</returns>
        public static Texture2D GetTexture(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetTexture(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Texture2D"/> represnting the target input's value.</returns>
        public static Texture2D GetTexture(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputTexture input = GetInput<SubstanceInputTexture>(substance, index, graphId);

            return input == null ? null : GetTextureInternal(input);
        }

        /// <summary>
        /// Attempt to get a texture value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetTexture(this SubstanceFileSO substance, out Texture2D value, SubstanceParameter inputParameter)
        {
            return TryGetTexture(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get a texture value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetTexture(this SubstanceFileSO substance, out Texture2D value, string name, int graphId=0)
        {
            return TryGetTexture(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get a texture value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetTexture(this SubstanceFileSO substance, out Texture2D value, int index, int graphId=0)
        {
            SubstanceInputTexture input = GetInput<SubstanceInputTexture>(substance, index, graphId);

            if(input != null)
            {
                //value = input.Data;
                value = GetTextureInternal(input);

                return true;
            }

            value = null;

            return false;
        }

        /// <summary>
        /// Set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetTexture(this SubstanceFileSO substance, Texture2D value, SubstanceParameter inputParameter)
        {
            SetTexture(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetTexture(this SubstanceFileSO substance, Texture2D value, string name, int graphId=0)
        {
            SetTexture(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetTexture(this SubstanceFileSO substance, Texture2D value, int index, int graphId=0)
        {
            SubstanceInputTexture input = GetInput<SubstanceInputTexture>(substance, index, graphId);

            if(input != null) SetTextureInternal(input, value);
        }

        /// <summary>
        /// Attempt to set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetTexture(this SubstanceFileSO substance, Texture2D value, SubstanceParameter inputParameter)
        {
            return TrySetTexture(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetTexture(this SubstanceFileSO substance, Texture2D value, string name, int graphId=0)
        {
            return TrySetTexture(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetTexture(this SubstanceFileSO substance, Texture2D value, int index, int graphId=0)
        {
            SubstanceInputTexture input = GetInput<SubstanceInputTexture>(substance, index, graphId);

            if(input == null) return false;

            SetTextureInternal(input, value);

            return true;
        }


        private static Texture2D GetTextureInternal(SubstanceInputTexture input)
        {
            return (Texture2D)TextureDataField.GetValue(input);
        }


        private static void SetTextureInternal(SubstanceInputTexture input, Texture2D value)
        {
            TextureDataField.SetValue(input, value);
        }

        #endregion

        #region String

        /// <summary>
        /// Get the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>string represnting the target input's value.</returns>
        public static string GetString(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetString(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>string represnting the target input's value.</returns>
        public static string GetString(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetString(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>string represnting the target input's value.</returns>
        public static string GetString(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputString input = GetInput<SubstanceInputString>(substance, index, graphId);

            return input == null ? string.Empty : input.Data;
        }

        /// <summary>
        /// Attempt to get a string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns an empty string.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetString(this SubstanceFileSO substance, out string value, SubstanceParameter inputParameter)
        {
            return TryGetString(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get a string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns an empty string.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetString(this SubstanceFileSO substance, out string value, string name, int graphId=0)
        {
            return TryGetString(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get a string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns an empty string.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetString(this SubstanceFileSO substance, out string value, int index, int graphId=0)
        {
            SubstanceInputString input = GetInput<SubstanceInputString>(substance, index, graphId);

            if(input != null)
            {
                value = input.Data;

                return true;
            }

            value = string.Empty;

            return false;
        }

        /// <summary>
        /// Set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetString(this SubstanceFileSO substance, string value, SubstanceParameter inputParameter)
        {
            SetString(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetString(this SubstanceFileSO substance, string value, string name, int graphId=0)
        {
            SetString(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetString(this SubstanceFileSO substance, string value, int index, int graphId=0)
        {
            SubstanceInputString input = GetInput<SubstanceInputString>(substance, index, graphId);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetString(this SubstanceFileSO substance, string value, SubstanceParameter inputParameter)
        {
            return TrySetString(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetString(this SubstanceFileSO substance, string value, string name, int graphId = 0)
        {
            return TrySetString(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetString(this SubstanceFileSO substance, string value, int index, int graphId = 0)
        {
            SubstanceInputString input = GetInput<SubstanceInputString>(substance, index, graphId);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Float

        /// <summary>
        /// Get the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>float represnting the target input's value.</returns>
        public static float GetFloat(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetFloat(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>float represnting the target input's value.</returns>
        public static float GetFloat(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetFloat(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>float represnting the target input's value.</returns>
        public static float GetFloat(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(substance, index, graphId);

            return input == null ? 0f : input.Data;
        }

        /// <summary>
        /// Attempt to get a float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat(this SubstanceFileSO substance, out float value, SubstanceParameter inputParameter)
        {
            return TryGetFloat(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get a float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat(this SubstanceFileSO substance, out float value, string name, int graphId=0)
        {
            return TryGetFloat(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get a float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat(this SubstanceFileSO substance, out float value, int index, int graphId=0)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(substance, index, graphId);

            if(input != null)
            {
                value = input.Data;

                return true;
            }

            value = 0f;

            return false;
        }

        /// <summary>
        /// Set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetFloat(this SubstanceFileSO substance, float value, SubstanceParameter inputParameter)
        {
            SetFloat(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetFloat(this SubstanceFileSO substance, float value, string name, int graphId = 0)
        {
            SetFloat(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetFloat(this SubstanceFileSO substance, float value, int index, int graphId = 0)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(substance, index, graphId);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat(this SubstanceFileSO substance, float value, SubstanceParameter inputParameter)
        {
            return TrySetFloat(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat(this SubstanceFileSO substance, float value, string name, int graphId = 0)
        {
            return TrySetFloat(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat(this SubstanceFileSO substance, float value, int index, int graphId = 0)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(substance, index, graphId);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Float2

        /// <summary>
        /// Get the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector2"/> represnting the target input's value.</returns>
        public static Vector2 GetFloat2(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetFloat2(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector2"/> represnting the target input's value.</returns>
        public static Vector2 GetFloat2(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetFloat2(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector2"/> represnting the target input's value.</returns>
        public static Vector2 GetFloat2(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(substance, index, graphId);

            return input == null ? Vector2.zero : input.Data;
        }

        /// <summary>
        /// Attempt to get a float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat2(this SubstanceFileSO substance, out Vector2 value, SubstanceParameter inputParameter)
        {
            return TryGetFloat2(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get a float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat2(this SubstanceFileSO substance, out Vector2 value, string name, int graphId=0)
        {
            return TryGetFloat2(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get a float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat2(this SubstanceFileSO substance, out Vector2 value, int index, int graphId=0)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(substance, index, graphId);

            if(input != null)
            {
                value = input.Data;

                return true;
            }

            value = Vector2.zero;

            return false;
        }

        /// <summary>
        /// Set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetFloat2(this SubstanceFileSO substance, Vector2 value, SubstanceParameter inputParameter)
        {
            SetFloat2(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetFloat2(this SubstanceFileSO substance, Vector2 value, string name, int graphId=0)
        {
            SetFloat2(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetFloat2(this SubstanceFileSO substance, Vector2 value, int index, int graphId=0)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(substance, index, graphId);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat2(this SubstanceFileSO substance, Vector2 value, SubstanceParameter inputParameter)
        {
            return TrySetFloat2(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat2(this SubstanceFileSO substance, Vector2 value, string name, int graphId=0)
        {
            return TrySetFloat2(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat2(this SubstanceFileSO substance, Vector2 value, int index, int graphId=0)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(substance, index, graphId);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Float3

        /// <summary>
        /// Get the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector3"/> represnting the target input's value.</returns>
        public static Vector3 GetFloat3(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetFloat3(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector3"/> represnting the target input's value.</returns>
        public static Vector3 GetFloat3(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetFloat3(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector3"/> represnting the target input's value.</returns>
        public static Vector3 GetFloat3(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputFloat3 input = GetInput<SubstanceInputFloat3>(substance, index, graphId);

            return input == null ? Vector3.zero : input.Data;
        }

        /// <summary>
        /// Attempt to get a float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat3(this SubstanceFileSO substance, out Vector3 value, SubstanceParameter inputParameter)
        {
            return TryGetFloat3(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get a float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat3(this SubstanceFileSO substance, out Vector3 value, string name, int graphId=0)
        {
            return TryGetFloat3(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get a float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat3(this SubstanceFileSO substance, out Vector3 value, int index, int graphId=0)
        {
            SubstanceInputFloat3 input = GetInput<SubstanceInputFloat3>(substance, index, graphId);

            if(input != null)
            {
                value = input.Data;

                return true;
            }

            value = Vector3.zero;

            return false;
        }

        /// <summary>
        /// Set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetFloat3(this SubstanceFileSO substance, Vector3 value, SubstanceParameter inputParameter)
        {
            SetFloat3(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetFloat3(this SubstanceFileSO substance, Vector3 value, string name, int graphId=0)
        {
            SetFloat3(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetFloat3(this SubstanceFileSO substance, Vector3 value, int index, int graphId=0)
        {
            SubstanceInputFloat3 input = GetInput<SubstanceInputFloat3>(substance, index, graphId);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat3(this SubstanceFileSO substance, Vector3 value, SubstanceParameter inputParameter)
        {
            return TrySetFloat3(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat3(this SubstanceFileSO substance, Vector3 value, string name, int graphId=0)
        {
            return TrySetFloat3(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat3(this SubstanceFileSO substance, Vector3 value, int index, int graphId=0)
        {
            SubstanceInputFloat3 input = GetInput<SubstanceInputFloat3>(substance, index, graphId);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Float4

        /// <summary>
        /// Get the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector4"/> represnting the target input's value.</returns>
        public static Vector4 GetFloat4(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetFloat4(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector4"/> represnting the target input's value.</returns>
        public static Vector4 GetFloat4(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetFloat4(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector4"/> represnting the target input's value.</returns>
        public static Vector4 GetFloat4(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputFloat4 input = GetInput<SubstanceInputFloat4>(substance, index, graphId);

            return input == null ? Vector4.zero : input.Data;
        }

        /// <summary>
        /// Attempt to get a float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat4(this SubstanceFileSO substance, out Vector4 value, SubstanceParameter inputParameter)
        {
            return TryGetFloat4(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get a float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat4(this SubstanceFileSO substance, out Vector4 value, string name, int graphId=0)
        {
            return TryGetFloat4(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get a float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat4(this SubstanceFileSO substance, out Vector4 value, int index, int graphId=0)
        {
            SubstanceInputFloat4 input = GetInput<SubstanceInputFloat4>(substance, index, graphId);

            if(input != null)
            {
                value = input.Data;

                return true;
            }

            value = Vector2.zero;

            return false;
        }

        /// <summary>
        /// Set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetFloat4(this SubstanceFileSO substance, Vector4 value, SubstanceParameter inputParameter)
        {
            SetFloat4(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetFloat4(this SubstanceFileSO substance, Vector4 value, string name, int graphId=0)
        {
            SetFloat4(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetFloat4(this SubstanceFileSO substance, Vector4 value, int index, int graphId=0)
        {
            SubstanceInputFloat4 input = GetInput<SubstanceInputFloat4>(substance, index, graphId);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat4(this SubstanceFileSO substance, Vector4 value, SubstanceParameter inputParameter)
        {
            return TrySetFloat4(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat4(this SubstanceFileSO substance, Vector4 value, string name, int graphId=0)
        {
            return TrySetFloat4(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat4(this SubstanceFileSO substance, Vector4 value, int index, int graphId=0)
        {
            SubstanceInputFloat4 input = GetInput<SubstanceInputFloat4>(substance, index, graphId);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Int

        /// <summary>
        /// Get the $randomseed value for the substance, using the given parameter data.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the $randomseed input.</param>
        /// <returns>int represnting the substance's $randomseed value.</returns>
        public static int GetRandomSeed(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetInt(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the $randomseed value for the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the $randomseed value from.</param>
        /// <param name="graphId">Index for the graph containing the target $randomseed.</param>
        /// <returns>int represnting the substance's $randomseed value.</returns>
        public static int GetRandomSeed(this SubstanceFileSO substance, int graphId=0)
        {
            return GetInt(substance, PARAM_RANDOM_SEED, graphId);
        }

        /// <summary>
        /// Set the $randomseed value for the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the $randomseed value on.</param>
        /// <param name="value">New value for the $randomseed input.</param>
        /// <param name="inputParameter">Parameter data for the $randomseed input.</param>
        public static void SetRandomSeed(this SubstanceFileSO substance, int value, SubstanceParameter inputParameter)
        {
            SetInt(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the $randomseed value for the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the $randomseed value on.</param>
        /// <param name="value">New value for the $randomseed input.</param>
        /// <param name="graphId"></param>
        public static void SetRandomSeed(this SubstanceFileSO substance, int value, int graphId=0)
        {
            SetInt(substance, value, PARAM_RANDOM_SEED, graphId);
        }

        /// <summary>
        /// Get the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>bool represnting the target input's value. False if 0, true if anything else.</returns>
        public static bool GetBool(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetBool(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>bool represnting the target input's value. False if 0, true if anything else.</returns>
        public static bool GetBool(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetBool(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>bool represnting the target input's value. False if 0, true if anything else.</returns>
        public static bool GetBool(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index, graphId);

            return input == null ? false : input.Data != 0;
        }

        /// <summary>
        /// Attempt to get a bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns false.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetBool(this SubstanceFileSO substance, out bool value, SubstanceParameter inputParameter)
        {
            return TryGetBool(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get a bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns false.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetBool(this SubstanceFileSO substance, out bool value, string name, int graphId=0)
        {
            return TryGetBool(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get a bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns false.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetBool(this SubstanceFileSO substance, out bool value, int index, int graphId=0)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index, graphId);

            if(input != null)
            {
                value = input.Data != 0;

                return true;
            }

            value = false;

            return false;
        }

        /// <summary>
        /// Set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetBool(this SubstanceFileSO substance, bool value, SubstanceParameter inputParameter)
        {
            SetBool(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetBool(this SubstanceFileSO substance, bool value, string name, int graphId=0)
        {
            SetBool(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetBool(this SubstanceFileSO substance, bool value, int index, int graphId=0)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index, graphId);

            if(input != null) input.Data = value ? 1 : 0;
        }

        /// <summary>
        /// Attempt to set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetBool(this SubstanceFileSO substance, bool value, SubstanceParameter inputParameter)
        {
            return TrySetBool(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetBool(this SubstanceFileSO substance, bool value, string name, int graphId=0)
        {
            return TrySetBool(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetBool(this SubstanceFileSO substance, bool value, int index, int graphId=0)
        {
            return TrySetInt(substance, value ? 1 : 0, index, graphId);
        }

        /// <summary>
        /// Get the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>int represnting the target input's value.</returns>
        public static int GetInt(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetInt(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>int represnting the target input's value.</returns>
        public static int GetInt(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetInt(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>int represnting the target input's value.</returns>
        public static int GetInt(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index, graphId);

            return input == null ? 0 : input.Data;
        }

        /// <summary>
        /// Attempt to get an int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt(this SubstanceFileSO substance, out int value, SubstanceParameter inputParameter)
        {
            return TryGetInt(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get an int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt(this SubstanceFileSO substance, out int value, string name, int graphId=0)
        {
            return TryGetInt(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get an int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt(this SubstanceFileSO substance, out int value, int index, int graphId=0)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index, graphId);

            if(input != null)
            {
                value = input.Data;

                return true;
            }

            value = 0;

            return false;
        }

        /// <summary>
        /// Set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetInt(this SubstanceFileSO substance, int value, SubstanceParameter inputParameter)
        {
            SetInt(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetInt(this SubstanceFileSO substance, int value, string name, int graphId=0)
        {
            SetInt(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetInt(this SubstanceFileSO substance, int value, int index, int graphId=0)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index, graphId);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt(this SubstanceFileSO substance, int value, SubstanceParameter inputParameter)
        {
            return TrySetInt(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt(this SubstanceFileSO substance, int value, string name, int graphId=0)
        {
            return TrySetInt(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt(this SubstanceFileSO substance, int value, int index, int graphId=0)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index, graphId);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Int2

        /// <summary>
        /// Get the $outputsize value for the substance, using the given parameter data.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the $outputsize input.</param>
        /// <returns><see cref="Vector2Int"/> represnting the substance's $outputsize value.</returns>
        public static Vector2Int GetOutputSize(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetInt2(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the $outputsize value for the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the $outputsize value from.</param>
        /// <param name="graphId">Index for the graph containing the target $outputsize.</param>
        /// <returns><see cref="Vector2Int"/> represnting the substance's $outputsize value.</returns>
        public static Vector2Int GetOutputSize(this SubstanceFileSO substance, int graphId=0)
        {
            return GetInt2(substance, PARAM_OUTPUT_SIZE, graphId);
        }

        /// <summary>
        /// Get the $outputsize width and height values for the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the $outputsize value from.</param>
        /// <param name="width">Value to populate with the substance's $outputsize width.</param>
        /// <param name="height">Value to populate with the substance's $outputsize height.</param>
        /// <param name="graphId">Index for the graph containing the target $outputsize.</param>
        public static void GetOutputSize(this SubstanceFileSO substance, out SbsOutputSize width, out SbsOutputSize height, int graphId=0)
        {
            Vector2Int value = GetOutputSize(substance, graphId);

            width = (SbsOutputSize)value.x;
            height = (SbsOutputSize)value.y;
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="size">Width and height of the size.</param>
        /// <param name="graphId">Index for the specific graph being targeted. Usually this can be left as 0.</param>
        public static void SetOutputSize(this SubstanceFileSO substance, SbsOutputSize size, int graphId=0)
        {
            SetOutputSize(substance, new Vector2Int((int)size, (int)size), graphId);
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="width">Width of the size value.</param>
        /// <param name="height">Height of the size value.</param>
        /// <param name="graphId">Index for the specific graph being targeted. Usually this can be left as 0.</param>
        public static void SetOutputSize(this SubstanceFileSO substance, SbsOutputSize width, SbsOutputSize height, int graphId=0)
        {
            SetOutputSize(substance, new Vector2Int((int)width, (int)height), graphId);
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="width">Width of the size value. Note that this value isn't the resolution in pixels, but specific int values associated with sizes. See <see cref="SbsOutputSize"/> for coresponding resolutions and ints.</param>
        /// <param name="height">Height of the size value. Note that this value isn't the resolution in pixels, but specific int values associated with sizes. See <see cref="SbsOutputSize"/> for coresponding resolutions and ints.</param>
        /// <param name="graphId">Index for the specific graph being targeted. Usually this can be left as 0.</param>
        public static void SetOutputSize(this SubstanceFileSO substance, int width, int height, int graphId=0)
        {
            SetOutputSize(substance, new Vector2Int(width, height), graphId);
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="size">Width and height values of the size. Note that this value isn't the resolution in pixels, but specific int values associated with sizes. See <see cref="SbsOutputSize"/> for coresponding resolutions and ints.</param>
        /// <param name="graphId">Index for the specific graph being targeted. Usually this can be left as 0.</param>
        public static void SetOutputSize(this SubstanceFileSO substance, Vector2Int size, int graphId=0)
        {
            SetInt2(substance, size, PARAM_OUTPUT_SIZE, graphId);
        }

        /// <summary>
        /// Get the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector2Int"/> represnting the target input's value.</returns>
        public static Vector2Int GetInt2(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetInt2(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector2Int"/> represnting the target input's value.</returns>
        public static Vector2Int GetInt2(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetInt2(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector2Int"/> represnting the target input's value.</returns>
        public static Vector2Int GetInt2(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index, graphId);

            return input == null ? Vector2Int.zero : input.Data;
        }

        /// <summary>
        /// Attempt to get an int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2Int.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt2(this SubstanceFileSO substance, out Vector2Int value, SubstanceParameter inputParameter)
        {
            return TryGetInt2(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get an int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2Int.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt2(this SubstanceFileSO substance, out Vector2Int value, string name, int graphId=0)
        {
            return TryGetInt2(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get an int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2Int.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt2(this SubstanceFileSO substance, out Vector2Int value, int index, int graphId=0)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index, graphId);

            if(input != null)
            {
                value = input.Data;

                return true;
            }

            value = Vector2Int.zero;

            return false;
        }

        /// <summary>
        /// Set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetInt2(this SubstanceFileSO substance, Vector2Int value, SubstanceParameter inputParameter)
        {
            SetInt2(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetInt2(this SubstanceFileSO substance, Vector2Int value, string name, int graphId=0)
        {
            SetInt2(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetInt2(this SubstanceFileSO substance, Vector2Int value, int index, int graphId=0)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index, graphId);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt2(this SubstanceFileSO substance, Vector2Int value, SubstanceParameter inputParameter)
        {
            return TrySetInt2(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt2(this SubstanceFileSO substance, Vector2Int value, string name, int graphId=0)
        {
            return TrySetInt2(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt2(this SubstanceFileSO substance, Vector2Int value, int index, int graphId=0)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index, graphId);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Int3

        /// <summary>
        /// Get the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector3Int"/> represnting the target input's value.</returns>
        public static Vector3Int GetInt3(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetInt3(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector3Int"/> represnting the target input's value.</returns>
        public static Vector3Int GetInt3(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetInt3(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector3Int"/> represnting the target input's value.</returns>
        public static Vector3Int GetInt3(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputInt3 input = GetInput<SubstanceInputInt3>(substance, index, graphId);

            return input == null ? Vector3Int.zero : input.Data;
        }

        /// <summary>
        /// Attempt to get an int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3Int.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt3(this SubstanceFileSO substance, out Vector3Int value, SubstanceParameter inputParameter)
        {
            return TryGetInt3(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get an int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3Int.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt3(this SubstanceFileSO substance, out Vector3Int value, string name, int graphId=0)
        {
            return TryGetInt3(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get an int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3Int.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt3(this SubstanceFileSO substance, out Vector3Int value, int index, int graphId=0)
        {
            SubstanceInputInt3 input = GetInput<SubstanceInputInt3>(substance, index, graphId);

            if(input != null)
            {
                value = input.Data;

                return true;
            }

            value = Vector3Int.zero;

            return false;
        }

        /// <summary>
        /// Set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetInt3(this SubstanceFileSO substance, Vector3Int value, SubstanceParameter inputParameter)
        {
            SetInt3(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetInt3(this SubstanceFileSO substance, Vector3Int value, string name, int graphId=0)
        {
            SetInt3(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetInt3(this SubstanceFileSO substance, Vector3Int value, int index, int graphId=0)
        {
            SubstanceInputInt3 input = GetInput<SubstanceInputInt3>(substance, index, graphId);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt3(this SubstanceFileSO substance, Vector3Int value, SubstanceParameter inputParameter)
        {
            return TrySetInt3(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt3(this SubstanceFileSO substance, Vector3Int value, string name, int graphId=0)
        {
            return TrySetInt3(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt3(this SubstanceFileSO substance, Vector3Int value, int index, int graphId=0)
        {
            SubstanceInputInt3 input = GetInput<SubstanceInputInt3>(substance, index, graphId);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Int4

        /// <summary>
        /// Get the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector4Int"/> represnting the target input's value.</returns>
        public static Vector4Int GetInt4(this SubstanceFileSO substance, SubstanceParameter inputParameter)
        {
            return GetInt4(substance, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Get the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector4Int"/> represnting the target input's value.</returns>
        public static Vector4Int GetInt4(this SubstanceFileSO substance, string name, int graphId=0)
        {
            return GetInt4(substance, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Get the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns><see cref="Vector4Int"/> represnting the target input's value.</returns>
        public static Vector4Int GetInt4(this SubstanceFileSO substance, int index, int graphId=0)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index, graphId);

            return input == null ? Vector4Int.zero : new Vector4Int(input.Data0, input.Data1, input.Data2, input.Data3);
        }

        /// <summary>
        /// Attempt to get an int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4Int.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4(this SubstanceFileSO substance, out Vector4Int value, SubstanceParameter inputParameter)
        {
            return TryGetInt4(substance, out value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to get an int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4Int.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4(this SubstanceFileSO substance, out Vector4Int value, string name, int graphId=0)
        {
            return TryGetInt4(substance, out value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to get an int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4Int.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4(this SubstanceFileSO substance, out Vector4Int value, int index, int graphId=0)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index, graphId);

            if(input != null)
            {
                value = new Vector4Int(input.Data0, input.Data1, input.Data2, input.Data3);

                return true;
            }

            value = Vector4Int.zero;

            return false;
        }

        /// <summary>
        /// Set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetInt4(this SubstanceFileSO substance, Vector4Int value, SubstanceParameter inputParameter)
        {
            SetInt4(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetInt4(this SubstanceFileSO substance, Vector4Int value, string name, int graphId=0)
        {
            SetInt4(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        public static void SetInt4(this SubstanceFileSO substance, Vector4Int value, int index, int graphId=0)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index, graphId);

            if(input != null)
            {
                input.Data0 = value.x;
                input.Data1 = value.y;
                input.Data2 = value.z;
                input.Data3 = value.w;
            }
        }

        /// <summary>
        /// Attempt to set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt4(this SubstanceFileSO substance, Vector4Int value, SubstanceParameter inputParameter)
        {
            return TrySetInt4(substance, value, inputParameter.Index, inputParameter.GraphId);
        }

        /// <summary>
        /// Attempt to set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt4(this SubstanceFileSO substance, Vector4Int value, string name, int graphId=0)
        {
            return TrySetInt4(substance, value, GetInputIndex(substance, name, graphId), graphId);
        }

        /// <summary>
        /// Attempt to set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceFileSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="graphId">Index for the graph containing the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt4(this SubstanceFileSO substance, Vector4Int value, int index, int graphId=0)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index, graphId);

            if(input == null) return false;

            input.Data0 = value.x;
            input.Data1 = value.y;
            input.Data2 = value.z;
            input.Data3 = value.w;

            return true;
        }

        #endregion

        #endregion
    }
}