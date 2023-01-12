using UnityEngine;
using System;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Contains extension methods for <see cref="SubstanceGraphSO"/>.
    /// </summary>
    public static class SubstanceGraphExtensions
    {
        /// <summary>
        /// Identifier for output size inputs.
        /// </summary>
        public const string kOutputSize = "$outputsize";
        /// <summary>
        /// Identifier for random seed inputs.
        /// </summary>
        public const string kRandomSeed = "$randomseed";

        #region Inputs

        public static ISubstanceInput GetInput(this SubstanceGraphSO substance, string name)
        {
            return GetInput(substance, GetInputIndex(substance, name));
        }


        public static ISubstanceInput GetInput(this SubstanceGraphSO substance, int index)
        {
            if(index < 0 || index >= substance.Input.Count)
            {
                return null;
            }

            return substance.Input[index];
        }


        public static T GetInput<T>(this SubstanceGraphSO substance, string name) where T : ISubstanceInput
        {
            return GetInput<T>(substance, GetInputIndex(substance, name));
        }


        public static T GetInput<T>(this SubstanceGraphSO substance, int index) where T : ISubstanceInput
        {
            if(index < 0 || index >= substance.Input.Count)
            {
                return default(T);
            }

            return (T)substance.Input[index];
        }


        public static int GetInputIndex(this SubstanceGraphSO substance, string name)
        {
            for(int i = 0; i < substance.Input.Count; i++)
            {
                if(substance.Input[i].Description.Identifier == name)
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion
        
        #region Get/Set Values

        /// <summary>
        /// Get the generic value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>object representing the target input's value.</returns>
        public static object GetValue(this SubstanceGraphSO substance, int index)
        {
            ISubstanceInput input = GetInput(substance, index);

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
                case SubstanceInputTexture textureInput: return textureInput.GetTexture();
            }

            return null;
        }

        /// <summary>
        /// Attempt to get a generic value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetValue(this SubstanceGraphSO substance, out object value, ISubstanceInputParameter inputParameter)
        {
            return TryGetValue(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get a generic value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetValue(this SubstanceGraphSO substance, out object value, string name)
        {
            return TryGetValue(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get a generic value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetValue(this SubstanceGraphSO substance, out object value, int index)
        {
            ISubstanceInput input = GetInput(substance, index);

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
                    case SubstanceInputTexture textureInput:
                        value = textureInput.GetTexture();
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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetValue(this SubstanceGraphSO substance, object value, ISubstanceInputParameter inputParameter)
        {
            SetValue(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetValue(this SubstanceGraphSO substance, object value, string name)
        {
            SetValue(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetValue(this SubstanceGraphSO substance, object value, int index)
        {
            ISubstanceInput input = GetInput(substance, index);

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
                    textureInput.SetTexture((Texture2D)value);
                    break;
            }
        }

        /// <summary>
        /// Attempt to set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetValue(this SubstanceGraphSO substance, object value, ISubstanceInputParameter inputParameter)
        {
            return TrySetValue(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetValue(this SubstanceGraphSO substance, object value, string name)
        {
            return TrySetValue(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetValue(this SubstanceGraphSO substance, object value, int index)
        {
            ISubstanceInput input = GetInput(substance, index);

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

                    textureInput.SetTexture((Texture2D)value);
                    break;
                default:
                    Debug.LogWarning("Input type not recognized!");
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Set the input values on the graph asset to match the given list of inputs.
        /// </summary>
        /// <param name="substance">Graph asset to set input values on.</param>
        /// <param name="inputs">Inputs to copy data from.</param>
        public static void SetInputValues(this SubstanceGraphSO substance, IList<ISubstanceInput> inputs)
        {
            for(int i=0; i < inputs.Count; i++)
            {
                if(!inputs[i].CopyTo(substance.Input[inputs[i].Index]))
                {
                    Debug.LogWarning(string.Format("Could not update input [{0}]", inputs[i].Description.Identifier));
                }
            }
        }


        [System.Obsolete("Use SetValues instead.")]
        public static void SetInputs(this SubstanceGraphSO substance, IList<ISubstanceInputParameterValue> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                values[i].SetValue(substance);
            }
        }

        /// <summary>
        /// Set values for the given inputs.
        /// </summary>
        /// <param name="substance">Substance asset to set values on.</param>
        /// <param name="values">Values to set on the target substance.</param>
        public static void SetValues<T>(this SubstanceGraphSO substance, IList<T> values) where T : ISubstanceInputParameterValue
        {
            SetValues(substance, (IList<ISubstanceInputParameterValue>)values);
        }

        /// <summary>
        /// Set values for the given inputs.
        /// </summary>
        /// <param name="substance">Substance asset to set values on.</param>
        /// <param name="values">Values to set on the target substance.</param>
        public static void SetValues(this SubstanceGraphSO substance, IList<ISubstanceInputParameterValue> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                values[i].SetValue(substance);
            }
        }

        #region Texture

        /// <summary>
        /// Get the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Texture2D"/> representing the target input's value.</returns>
        public static Texture2D GetTexture(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetTexture(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns><see cref="Texture2D"/> representing the target input's value.</returns>
        public static Texture2D GetTexture(this SubstanceGraphSO substance, string name)
        {
            return GetTexture(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns><see cref="Texture2D"/> representing the target input's value.</returns>
        public static Texture2D GetTexture(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputTexture input = GetInput<SubstanceInputTexture>(substance, index);

            return input == null ? null : input.GetTexture();
        }

        /// <summary>
        /// Attempt to get a texture value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetTexture(this SubstanceGraphSO substance, out Texture2D value, ISubstanceInputParameter inputParameter)
        {
            return TryGetTexture(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get a texture value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetTexture(this SubstanceGraphSO substance, out Texture2D value, string name)
        {
            return TryGetTexture(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get a texture value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns null.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetTexture(this SubstanceGraphSO substance, out Texture2D value, int index)
        {
            SubstanceInputTexture input = GetInput<SubstanceInputTexture>(substance, index);

            if(input != null)
            {
                value = input.GetTexture();

                return true;
            }

            value = null;

            return false;
        }

        /// <summary>
        /// Set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetTexture(this SubstanceGraphSO substance, Texture2D value, ISubstanceInputParameter inputParameter)
        {
            SetTexture(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetTexture(this SubstanceGraphSO substance, Texture2D value, string name)
        {
            SetTexture(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetTexture(this SubstanceGraphSO substance, Texture2D value, int index)
        {
            SubstanceInputTexture input = GetInput<SubstanceInputTexture>(substance, index);

            if(input != null) input.SetTexture(value);
        }

        /// <summary>
        /// Attempt to set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetTexture(this SubstanceGraphSO substance, Texture2D value, ISubstanceInputParameter inputParameter)
        {
            return TrySetTexture(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetTexture(this SubstanceGraphSO substance, Texture2D value, string name)
        {
            return TrySetTexture(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the <see cref="Texture2D"/> value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetTexture(this SubstanceGraphSO substance, Texture2D value, int index)
        {
            SubstanceInputTexture input = GetInput<SubstanceInputTexture>(substance, index);

            if(input == null) return false;

            input.SetTexture(value);

            return true;
        }

        #endregion

        #region String

        /// <summary>
        /// Get the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>string representing the target input's value.</returns>
        public static string GetString(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetString(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>string representing the target input's value.</returns>
        public static string GetString(this SubstanceGraphSO substance, string name)
        {
            return GetString(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>string representing the target input's value.</returns>
        public static string GetString(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputString input = GetInput<SubstanceInputString>(substance, index);

            return input == null ? string.Empty : input.Data;
        }

        /// <summary>
        /// Attempt to get a string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns an empty string.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetString(this SubstanceGraphSO substance, out string value, ISubstanceInputParameter inputParameter)
        {
            return TryGetString(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get a string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns an empty string.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetString(this SubstanceGraphSO substance, out string value, string name)
        {
            return TryGetString(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get a string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns an empty string.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetString(this SubstanceGraphSO substance, out string value, int index)
        {
            SubstanceInputString input = GetInput<SubstanceInputString>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetString(this SubstanceGraphSO substance, string value, ISubstanceInputParameter inputParameter)
        {
            SetString(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetString(this SubstanceGraphSO substance, string value, string name)
        {
            SetString(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetString(this SubstanceGraphSO substance, string value, int index)
        {
            SubstanceInputString input = GetInput<SubstanceInputString>(substance, index);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetString(this SubstanceGraphSO substance, string value, ISubstanceInputParameter inputParameter)
        {
            return TrySetString(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetString(this SubstanceGraphSO substance, string value, string name)
        {
            return TrySetString(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the string value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetString(this SubstanceGraphSO substance, string value, int index)
        {
            SubstanceInputString input = GetInput<SubstanceInputString>(substance, index);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Float

        /// <summary>
        /// Get the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>float representing the target input's value.</returns>
        public static float GetFloat(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetFloat(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>float representing the target input's value.</returns>
        public static float GetFloat(this SubstanceGraphSO substance, string name)
        {
            return GetFloat(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>float representing the target input's value.</returns>
        public static float GetFloat(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(substance, index);

            return input == null ? 0f : input.Data;
        }

        /// <summary>
        /// Attempt to get a float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat(this SubstanceGraphSO substance, out float value, ISubstanceInputParameter inputParameter)
        {
            return TryGetFloat(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get a float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat(this SubstanceGraphSO substance, out float value, string name)
        {
            return TryGetFloat(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get a float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat(this SubstanceGraphSO substance, out float value, int index)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetFloat(this SubstanceGraphSO substance, float value, ISubstanceInputParameter inputParameter)
        {
            SetFloat(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetFloat(this SubstanceGraphSO substance, float value, string name)
        {
            SetFloat(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetFloat(this SubstanceGraphSO substance, float value, int index)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(substance, index);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat(this SubstanceGraphSO substance, float value, ISubstanceInputParameter inputParameter)
        {
            return TrySetFloat(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat(this SubstanceGraphSO substance, float value, string name)
        {
            return TrySetFloat(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the float value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat(this SubstanceGraphSO substance, float value, int index)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(substance, index);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Float2

        /// <summary>
        /// Get the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector2"/> representing the target input's value.</returns>
        public static Vector2 GetFloat2(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetFloat2(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns><see cref="Vector2"/> representing the target input's value.</returns>
        public static Vector2 GetFloat2(this SubstanceGraphSO substance, string name)
        {
            return GetFloat2(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns><see cref="Vector2"/> representing the target input's value.</returns>
        public static Vector2 GetFloat2(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(substance, index);

            return input == null ? Vector2.zero : input.Data;
        }

        /// <summary>
        /// Attempt to get a float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat2(this SubstanceGraphSO substance, out Vector2 value, ISubstanceInputParameter inputParameter)
        {
            return TryGetFloat2(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get a float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat2(this SubstanceGraphSO substance, out Vector2 value, string name)
        {
            return TryGetFloat2(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get a float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat2(this SubstanceGraphSO substance, out Vector2 value, int index)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetFloat2(this SubstanceGraphSO substance, Vector2 value, ISubstanceInputParameter inputParameter)
        {
            SetFloat2(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetFloat2(this SubstanceGraphSO substance, Vector2 value, string name)
        {
            SetFloat2(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetFloat2(this SubstanceGraphSO substance, Vector2 value, int index)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(substance, index);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat2(this SubstanceGraphSO substance, Vector2 value, ISubstanceInputParameter inputParameter)
        {
            return TrySetFloat2(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat2(this SubstanceGraphSO substance, Vector2 value, string name)
        {
            return TrySetFloat2(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the float2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat2(this SubstanceGraphSO substance, Vector2 value, int index)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(substance, index);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Float3

        /// <summary>
        /// Get the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector3"/> representing the target input's value.</returns>
        public static Vector3 GetFloat3(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetFloat3(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns><see cref="Vector3"/> representing the target input's value.</returns>
        public static Vector3 GetFloat3(this SubstanceGraphSO substance, string name)
        {
            return GetFloat3(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns><see cref="Vector3"/> representing the target input's value.</returns>
        public static Vector3 GetFloat3(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputFloat3 input = GetInput<SubstanceInputFloat3>(substance, index);

            return input == null ? Vector3.zero : input.Data;
        }

        /// <summary>
        /// Attempt to get a float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat3(this SubstanceGraphSO substance, out Vector3 value, ISubstanceInputParameter inputParameter)
        {
            return TryGetFloat3(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get a float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat3(this SubstanceGraphSO substance, out Vector3 value, string name)
        {
            return TryGetFloat3(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get a float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat3(this SubstanceGraphSO substance, out Vector3 value, int index)
        {
            SubstanceInputFloat3 input = GetInput<SubstanceInputFloat3>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetFloat3(this SubstanceGraphSO substance, Vector3 value, ISubstanceInputParameter inputParameter)
        {
            SetFloat3(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetFloat3(this SubstanceGraphSO substance, Vector3 value, string name)
        {
            SetFloat3(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetFloat3(this SubstanceGraphSO substance, Vector3 value, int index)
        {
            SubstanceInputFloat3 input = GetInput<SubstanceInputFloat3>(substance, index);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat3(this SubstanceGraphSO substance, Vector3 value, ISubstanceInputParameter inputParameter)
        {
            return TrySetFloat3(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat3(this SubstanceGraphSO substance, Vector3 value, string name)
        {
            return TrySetFloat3(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the float3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat3(this SubstanceGraphSO substance, Vector3 value, int index)
        {
            SubstanceInputFloat3 input = GetInput<SubstanceInputFloat3>(substance, index);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Float4

        /// <summary>
        /// Get the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector4"/> representing the target input's value.</returns>
        public static Vector4 GetFloat4(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetFloat4(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns><see cref="Vector4"/> representing the target input's value.</returns>
        public static Vector4 GetFloat4(this SubstanceGraphSO substance, string name)
        {
            return GetFloat4(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns><see cref="Vector4"/> representing the target input's value.</returns>
        public static Vector4 GetFloat4(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputFloat4 input = GetInput<SubstanceInputFloat4>(substance, index);

            return input == null ? Vector4.zero : input.Data;
        }

        /// <summary>
        /// Attempt to get a float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat4(this SubstanceGraphSO substance, out Vector4 value, ISubstanceInputParameter inputParameter)
        {
            return TryGetFloat4(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get a float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat4(this SubstanceGraphSO substance, out Vector4 value, string name)
        {
            return TryGetFloat4(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get a float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetFloat4(this SubstanceGraphSO substance, out Vector4 value, int index)
        {
            SubstanceInputFloat4 input = GetInput<SubstanceInputFloat4>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetFloat4(this SubstanceGraphSO substance, Vector4 value, ISubstanceInputParameter inputParameter)
        {
            SetFloat4(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetFloat4(this SubstanceGraphSO substance, Vector4 value, string name)
        {
            SetFloat4(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetFloat4(this SubstanceGraphSO substance, Vector4 value, int index)
        {
            SubstanceInputFloat4 input = GetInput<SubstanceInputFloat4>(substance, index);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat4(this SubstanceGraphSO substance, Vector4 value, ISubstanceInputParameter inputParameter)
        {
            return TrySetFloat4(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat4(this SubstanceGraphSO substance, Vector4 value, string name)
        {
            return TrySetFloat4(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the float4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetFloat4(this SubstanceGraphSO substance, Vector4 value, int index)
        {
            SubstanceInputFloat4 input = GetInput<SubstanceInputFloat4>(substance, index);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Int

        /// <summary>
        /// Get the $randomseed value for the substance, using the given parameter data.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the $randomseed input.</param>
        /// <returns>int representing the substance's $randomseed value.</returns>
        public static int GetRandomSeed(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetInt(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the $randomseed value for the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the $randomseed value from.</param>
        /// <returns>int representing the substance's $randomseed value.</returns>
        public static int GetRandomSeed(this SubstanceGraphSO substance)
        {
            return GetInt(substance, kRandomSeed);
        }

        /// <summary>
        /// Set the $randomseed value for the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the $randomseed value on.</param>
        /// <param name="value">New value for the $randomseed input.</param>
        /// <param name="inputParameter">Parameter data for the $randomseed input.</param>
        public static void SetRandomSeed(this SubstanceGraphSO substance, int value, ISubstanceInputParameter inputParameter)
        {
            SetInt(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the $randomseed value for the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the $randomseed value on.</param>
        /// <param name="value">New value for the $randomseed input.</param>
        public static void SetRandomSeed(this SubstanceGraphSO substance, int value)
        {
            SetInt(substance, value, kRandomSeed);
        }

        #region Bool

        /// <summary>
        /// Get the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>bool representing the target input's value. False if 0, true if anything else.</returns>
        public static bool GetBool(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetBool(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>bool representing the target input's value. False if 0, true if anything else.</returns>
        public static bool GetBool(this SubstanceGraphSO substance, string name)
        {
            return GetBool(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>bool representing the target input's value. False if 0, true if anything else.</returns>
        public static bool GetBool(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

            return input == null ? false : input.Data != 0;
        }

        /// <summary>
        /// Attempt to get a bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns false.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetBool(this SubstanceGraphSO substance, out bool value, ISubstanceInputParameter inputParameter)
        {
            return TryGetBool(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get a bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns false.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetBool(this SubstanceGraphSO substance, out bool value, string name)
        {
            return TryGetBool(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get a bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns false.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetBool(this SubstanceGraphSO substance, out bool value, int index)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetBool(this SubstanceGraphSO substance, bool value, ISubstanceInputParameter inputParameter)
        {
            SetBool(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetBool(this SubstanceGraphSO substance, bool value, string name)
        {
            SetBool(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetBool(this SubstanceGraphSO substance, bool value, int index)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

            if(input != null) input.Data = value ? 1 : 0;
        }

        /// <summary>
        /// Attempt to set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetBool(this SubstanceGraphSO substance, bool value, ISubstanceInputParameter inputParameter)
        {
            return TrySetBool(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetBool(this SubstanceGraphSO substance, bool value, string name)
        {
            return TrySetBool(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the bool value for the target input on the substance. Note: bool values are wrappers for int values.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetBool(this SubstanceGraphSO substance, bool value, int index)
        {
            return TrySetInt(substance, value ? 1 : 0, index);
        }

        #endregion

        #region Enum

        /// <summary>
        /// Get the enum value for the target int input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <param name="defaultValue">Default value returned if the target input is not found.</param>
        /// <returns>Enum value cast from the target input's int value.</returns>
        public static TEnum GetEnum<TEnum>(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter, TEnum defaultValue=default(TEnum)) where TEnum : Enum
        {
            return GetEnum<TEnum>(substance, inputParameter.Index, defaultValue);
        }

        /// <summary>
        /// Get the enum value for the target int input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="defaultValue">Default value returned if the target input is not found.</param>
        /// <returns>Enum value cast from the target input's int value.</returns>
        public static TEnum GetEnum<TEnum>(this SubstanceGraphSO substance, string name, TEnum defaultValue=default(TEnum)) where TEnum : Enum
        {
            return GetEnum<TEnum>(substance, GetInputIndex(substance, name), defaultValue);
        }

        /// <summary>
        /// Get the enum value for the target int input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="defaultValue">Default value returned if the target input is not found.</param>
        /// <returns>Enum value cast from the target input's int value.</returns>
        public static TEnum GetEnum<TEnum>(this SubstanceGraphSO substance, int index, TEnum defaultValue=default(TEnum)) where TEnum : Enum
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

            return input == null ? defaultValue : (TEnum)(object)input.Data;
        }

        /// <summary>
        /// Attempt to get an int value for the target input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <param name="defaultValue">Default value returned if the target input is not found.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetEnum<TEnum>(this SubstanceGraphSO substance, out TEnum value, ISubstanceInputParameter inputParameter, TEnum defaultValue=default(TEnum)) where TEnum : Enum
        {
            return TryGetEnum<TEnum>(substance, out value, inputParameter.Index, defaultValue);
        }

        /// <summary>
        /// Attempt to get an int value for the target input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="defaultValue">Default value returned if the target input is not found.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetEnum<TEnum>(this SubstanceGraphSO substance, out TEnum value, string name, TEnum defaultValue=default(TEnum)) where TEnum : Enum
        {
            return TryGetEnum<TEnum>(substance, out value, GetInputIndex(substance, name), defaultValue);
        }

        /// <summary>
        /// Attempt to get an int value for the target input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="defaultValue">Default value returned if the target input is not found.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetEnum<TEnum>(this SubstanceGraphSO substance, out TEnum value, int index, TEnum defaultValue=default(TEnum)) where TEnum : Enum
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

            if(input != null)
            {
                value = (TEnum)(object)input.Data;

                return true;
            }

            value = defaultValue;

            return false;
        }

        /// <summary>
        /// Set the int value for the target input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetEnum<TEnum>(this SubstanceGraphSO substance, TEnum value, ISubstanceInputParameter inputParameter) where TEnum : Enum
        {
            SetEnum<TEnum>(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the int value for the target input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetEnum<TEnum>(this SubstanceGraphSO substance, TEnum value, string name) where TEnum : Enum
        {
            SetEnum<TEnum>(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the int value for the target input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetEnum<TEnum>(this SubstanceGraphSO substance, TEnum value, int index) where TEnum : Enum
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

            if(input != null) input.Data = (int)(object)value;
        }

        /// <summary>
        /// Attempt to set the int value for the target input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetEnum<TEnum>(this SubstanceGraphSO substance, TEnum value, ISubstanceInputParameter inputParameter) where TEnum : Enum
        {
            return TrySetEnum<TEnum>(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the int value for the target input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetEnum<TEnum>(this SubstanceGraphSO substance, TEnum value, string name) where TEnum : Enum
        {
            return TrySetEnum<TEnum>(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the int value for the target input on the substance.
        /// </summary>
        /// <typeparam name="TEnum">Enum type to cast the input's int value to.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetEnum<TEnum>(this SubstanceGraphSO substance, TEnum value, int index) where TEnum : Enum
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

            if(input == null) return false;

            input.Data = (int)(object)value;

            return true;
        }

        #endregion

        /// <summary>
        /// Get the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>int representing the target input's value.</returns>
        public static int GetInt(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetInt(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>int representing the target input's value.</returns>
        public static int GetInt(this SubstanceGraphSO substance, string name)
        {
            return GetInt(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>int representing the target input's value.</returns>
        public static int GetInt(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

            return input == null ? 0 : input.Data;
        }

        /// <summary>
        /// Attempt to get an int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt(this SubstanceGraphSO substance, out int value, ISubstanceInputParameter inputParameter)
        {
            return TryGetInt(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get an int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt(this SubstanceGraphSO substance, out int value, string name)
        {
            return TryGetInt(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get an int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns 0.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt(this SubstanceGraphSO substance, out int value, int index)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetInt(this SubstanceGraphSO substance, int value, ISubstanceInputParameter inputParameter)
        {
            SetInt(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetInt(this SubstanceGraphSO substance, int value, string name)
        {
            SetInt(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetInt(this SubstanceGraphSO substance, int value, int index)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt(this SubstanceGraphSO substance, int value, ISubstanceInputParameter inputParameter)
        {
            return TrySetInt(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt(this SubstanceGraphSO substance, int value, string name)
        {
            return TrySetInt(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the int value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt(this SubstanceGraphSO substance, int value, int index)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Int2

        /// <summary>
        /// Get the $outputsize value for the substance, using the given parameter data.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the $outputsize input.</param>
        /// <returns><see cref="Vector2Int"/> representing the substance's $outputsize value.</returns>
        public static Vector2Int GetOutputSize(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetInt2(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the $outputsize value for the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the $outputsize value from.</param>
        /// <returns><see cref="Vector2Int"/> representing the substance's $outputsize value.</returns>
        public static Vector2Int GetOutputSize(this SubstanceGraphSO substance)
        {
            return GetInt2(substance, kOutputSize);
        }

        /// <summary>
        /// Get the $outputsize width and height values for the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the $outputsize value from.</param>
        /// <param name="width">Value to populate with the substance's $outputsize width.</param>
        /// <param name="height">Value to populate with the substance's $outputsize height.</param>
        public static void GetOutputSize(this SubstanceGraphSO substance, out SbsOutputSize width, out SbsOutputSize height)
        {
            Vector2Int value = GetOutputSize(substance);

            width = (SbsOutputSize)value.x;
            height = (SbsOutputSize)value.y;
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="size">Width and height of the size.</param>
        public static void SetOutputSize(this SubstanceGraphSO substance, SbsOutputSize size)
        {
            SetOutputSize(substance, new Vector2Int((int)size, (int)size));
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="width">Width of the size value.</param>
        /// <param name="height">Height of the size value.</param>
        public static void SetOutputSize(this SubstanceGraphSO substance, SbsOutputSize width, SbsOutputSize height)
        {
            SetOutputSize(substance, new Vector2Int((int)width, (int)height));
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="width">Width of the size value. Note that this value isn't the resolution in pixels, but specific int values associated with sizes. See <see cref="SbsOutputSize"/> for coresponding resolutions and ints.</param>
        /// <param name="height">Height of the size value. Note that this value isn't the resolution in pixels, but specific int values associated with sizes. See <see cref="SbsOutputSize"/> for coresponding resolutions and ints.</param>
        public static void SetOutputSize(this SubstanceGraphSO substance, int width, int height)
        {
            SetOutputSize(substance, new Vector2Int(width, height));
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="size">Width and height values of the size. Note that this value isn't the resolution in pixels, but specific int values associated with sizes. See <see cref="SbsOutputSize"/> for coresponding resolutions and ints.</param>
        public static void SetOutputSize(this SubstanceGraphSO substance, Vector2Int size)
        {
            SetInt2(substance, size, kOutputSize);
        }

        /// <summary>
        /// Get the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector2Int"/> representing the target input's value.</returns>
        public static Vector2Int GetInt2(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetInt2(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns><see cref="Vector2Int"/> representing the target input's value.</returns>
        public static Vector2Int GetInt2(this SubstanceGraphSO substance, string name)
        {
            return GetInt2(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns><see cref="Vector2Int"/> representing the target input's value.</returns>
        public static Vector2Int GetInt2(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index);

            return input == null ? Vector2Int.zero : input.Data;
        }

        /// <summary>
        /// Attempt to get an int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2Int.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt2(this SubstanceGraphSO substance, out Vector2Int value, ISubstanceInputParameter inputParameter)
        {
            return TryGetInt2(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get an int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2Int.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt2(this SubstanceGraphSO substance, out Vector2Int value, string name)
        {
            return TryGetInt2(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get an int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector2Int.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt2(this SubstanceGraphSO substance, out Vector2Int value, int index)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetInt2(this SubstanceGraphSO substance, Vector2Int value, ISubstanceInputParameter inputParameter)
        {
            SetInt2(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetInt2(this SubstanceGraphSO substance, Vector2Int value, string name)
        {
            SetInt2(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetInt2(this SubstanceGraphSO substance, Vector2Int value, int index)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt2(this SubstanceGraphSO substance, Vector2Int value, ISubstanceInputParameter inputParameter)
        {
            return TrySetInt2(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt2(this SubstanceGraphSO substance, Vector2Int value, string name)
        {
            return TrySetInt2(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the int2 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt2(this SubstanceGraphSO substance, Vector2Int value, int index)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Int3

        /// <summary>
        /// Get the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector3Int"/> representing the target input's value.</returns>
        public static Vector3Int GetInt3(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetInt3(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns><see cref="Vector3Int"/> representing the target input's value.</returns>
        public static Vector3Int GetInt3(this SubstanceGraphSO substance, string name)
        {
            return GetInt3(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns><see cref="Vector3Int"/> representing the target input's value.</returns>
        public static Vector3Int GetInt3(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputInt3 input = GetInput<SubstanceInputInt3>(substance, index);

            return input == null ? Vector3Int.zero : input.Data;
        }

        /// <summary>
        /// Attempt to get an int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3Int.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt3(this SubstanceGraphSO substance, out Vector3Int value, ISubstanceInputParameter inputParameter)
        {
            return TryGetInt3(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get an int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3Int.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt3(this SubstanceGraphSO substance, out Vector3Int value, string name)
        {
            return TryGetInt3(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get an int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector3Int.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt3(this SubstanceGraphSO substance, out Vector3Int value, int index)
        {
            SubstanceInputInt3 input = GetInput<SubstanceInputInt3>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetInt3(this SubstanceGraphSO substance, Vector3Int value, ISubstanceInputParameter inputParameter)
        {
            SetInt3(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetInt3(this SubstanceGraphSO substance, Vector3Int value, string name)
        {
            SetInt3(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetInt3(this SubstanceGraphSO substance, Vector3Int value, int index)
        {
            SubstanceInputInt3 input = GetInput<SubstanceInputInt3>(substance, index);

            if(input != null) input.Data = value;
        }

        /// <summary>
        /// Attempt to set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt3(this SubstanceGraphSO substance, Vector3Int value, ISubstanceInputParameter inputParameter)
        {
            return TrySetInt3(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt3(this SubstanceGraphSO substance, Vector3Int value, string name)
        {
            return TrySetInt3(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the int3 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt3(this SubstanceGraphSO substance, Vector3Int value, int index)
        {
            SubstanceInputInt3 input = GetInput<SubstanceInputInt3>(substance, index);

            if(input == null) return false;

            input.Data = value;

            return true;
        }

        #endregion

        #region Int4

        /// <summary>
        /// Get the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="int[]"/> representing the target input's value.</returns>
        public static int[] GetInt4(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetInt4(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns><see cref="int[]"/> representing the target input's value.</returns>
        public static int[] GetInt4(this SubstanceGraphSO substance, string name)
        {
            return GetInt4(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns><see cref="int[]"/> representing the target input's value.</returns>
        public static int[] GetInt4(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index);

            return input == null ? new int[4] : new int[4] { input.Data0, input.Data1, input.Data2, input.Data3 };
        }

        /// <summary>
        /// Attempt to get an int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns an array with four 0 values.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4(this SubstanceGraphSO substance, out int[] value, ISubstanceInputParameter inputParameter)
        {
            return TryGetInt4(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get an int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns an array with four 0 values.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4(this SubstanceGraphSO substance, out int[] value, string name)
        {
            return TryGetInt4(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get an int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns an array with four 0 values.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4(this SubstanceGraphSO substance, out int[] value, int index)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index);

            if(input != null)
            {
                value = new int[4] { input.Data0, input.Data1, input.Data2, input.Data3 };

                return true;
            }

            value = new int[4];

            return false;
        }

        /// <summary>
        /// Populate an existing int array with the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <param name="value">Array of int values to populate. This must have at least 4 elements.</param>
        public static void GetInt4NonAlloc(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter, int[] value)
        {
            GetInt4NonAlloc(substance, inputParameter.Index, value);
        }

        /// <summary>
        /// Populate an existing int array with the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <param name="value">Array of int values to populate. This must have at least 4 elements.</param>
        public static void GetInt4NonAlloc(this SubstanceGraphSO substance, string name, int[] value)
        {
            GetInt4NonAlloc(substance, GetInputIndex(substance, name), value);
        }

        /// <summary>
        /// Populate an existing int array with the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <param name="value">Array of int values to populate. This must have at least 4 elements.</param>
        public static void GetInt4NonAlloc(this SubstanceGraphSO substance, int index, int[] value)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index);

            if(input == null)
            {
                value[0] = 0;
                value[1] = 0;
                value[2] = 0;
                value[3] = 0;
                return;
            }

            value[0] = input.Data0;
            value[1] = input.Data1;
            value[2] = input.Data2;
            value[3] = input.Data3;
        }

        /// <summary>
        /// Attempt to populate an existing int array with the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Array of int values to populate. This must have at least 4 elements.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4NonAlloc(this SubstanceGraphSO substance, int[] value, ISubstanceInputParameter inputParameter)
        {
            return TryGetInt4NonAlloc(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to populate an existing int array with the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Array of int values to populate. This must have at least 4 elements.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4NonAlloc(this SubstanceGraphSO substance, int[] value, string name)
        {
            return TryGetInt4NonAlloc(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to populate an existing int array with the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Array of int values to populate. This must have at least 4 elements.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4NonAlloc(this SubstanceGraphSO substance, int[] value, int index)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index);

            if(input != null)
            {
                value[0] = input.Data0;
                value[1] = input.Data1;
                value[2] = input.Data2;
                value[3] = input.Data3;

                return true;
            }

            value[0] = 0;
            value[1] = 0;
            value[2] = 0;
            value[3] = 0;

            return false;
        }

        /// <summary>
        /// Get the int4 value for the target input on the substance as a <see cref="Vector4Int"/>.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns><see cref="Vector4Int"/> representing the target input's value.</returns>
        public static Vector4Int GetInt4Vector(this SubstanceGraphSO substance, ISubstanceInputParameter inputParameter)
        {
            return GetInt4Vector(substance, inputParameter.Index);
        }

        /// <summary>
        /// Get the int4 value for the target input on the substance as a <see cref="Vector4Int"/>.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns><see cref="Vector4Int"/> representing the target input's value.</returns>
        public static Vector4Int GetInt4Vector(this SubstanceGraphSO substance, string name)
        {
            return GetInt4Vector(substance, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Get the int4 value for the target input on the substance as a <see cref="Vector4Int"/>.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns><see cref="Vector4Int"/> representing the target input's value.</returns>
        public static Vector4Int GetInt4Vector(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index);

            return input == null ? Vector4Int.zero : new Vector4Int(input.Data0, input.Data1, input.Data2, input.Data3);
        }

        /// <summary>
        /// Attempt to get an int4 value for the target input on the substance as a <see cref="Vector4Int"/>.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4Int.zero"/>.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4Vector(this SubstanceGraphSO substance, out Vector4Int value, ISubstanceInputParameter inputParameter)
        {
            return TryGetInt4Vector(substance, out value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to get an int4 value for the target input on the substance as a <see cref="Vector4Int"/>.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4Int.zero"/>.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4Vector(this SubstanceGraphSO substance, out Vector4Int value, string name)
        {
            return TryGetInt4Vector(substance, out value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to get an int4 value for the target input on the substance as a <see cref="Vector4Int"/>.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the input value from.</param>
        /// <param name="value">Value for the target input if found. Otherwise returns <see cref="Vector4Int.zero"/>.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the value was found or false if the input was not found.</returns>
        public static bool TryGetInt4Vector(this SubstanceGraphSO substance, out Vector4Int value, int index)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetInt4(this SubstanceGraphSO substance, int[] value, ISubstanceInputParameter inputParameter)
        {
            SetInt4(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetInt4(this SubstanceGraphSO substance, int[] value, string name)
        {
            SetInt4(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetInt4(this SubstanceGraphSO substance, int[] value, int index)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index);

            if(input != null)
            {
                input.Data0 = value[0];
                input.Data1 = value[1];
                input.Data2 = value[2];
                input.Data3 = value[3];
            }
        }

        /// <summary>
        /// Attempt to set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt4(this SubstanceGraphSO substance, int[] value, ISubstanceInputParameter inputParameter)
        {
            return TrySetInt4(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt4(this SubstanceGraphSO substance, int[] value, string name)
        {
            return TrySetInt4(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt4(this SubstanceGraphSO substance, int[] value, int index)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index);

            if(input == null) return false;

            input.Data0 = value[0];
            input.Data1 = value[1];
            input.Data2 = value[2];
            input.Data3 = value[3];

            return true;
        }

        /// <summary>
        /// Set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        public static void SetInt4(this SubstanceGraphSO substance, Vector4Int value, ISubstanceInputParameter inputParameter)
        {
            SetInt4(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        public static void SetInt4(this SubstanceGraphSO substance, Vector4Int value, string name)
        {
            SetInt4(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        public static void SetInt4(this SubstanceGraphSO substance, Vector4Int value, int index)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index);

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
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="inputParameter">Parameter data for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt4(this SubstanceGraphSO substance, Vector4Int value, ISubstanceInputParameter inputParameter)
        {
            return TrySetInt4(substance, value, inputParameter.Index);
        }

        /// <summary>
        /// Attempt to set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="name">Name for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt4(this SubstanceGraphSO substance, Vector4Int value, string name)
        {
            return TrySetInt4(substance, value, GetInputIndex(substance, name));
        }

        /// <summary>
        /// Attempt to set the int4 value for the target input on the substance.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to set the input value on.</param>
        /// <param name="value">New value for the input.</param>
        /// <param name="index">Index for the target input.</param>
        /// <returns>True if the input value was set or false otherwise.</returns>
        public static bool TrySetInt4(this SubstanceGraphSO substance, Vector4Int value, int index)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index);

            if(input == null) return false;

            input.Data0 = value.x;
            input.Data1 = value.y;
            input.Data2 = value.z;
            input.Data3 = value.w;

            return true;
        }

        #endregion

        #endregion

        #region Outputs

        /// <summary>
        /// Get the output texture associated with the given output identifier.
        /// </summary>
        /// <param name="substance">Substance to pull output textures from.</param>
        /// <param name="identifier">Identifier for the target output texture. This is the output identifier specified in Substance Designer, ie "basecolor"</param>
        /// <returns>Texture on the graph associated with the given identifier, or null if one isn't found.</returns>
        public static Texture2D GetOutputTexture(this SubstanceGraphSO substance, string identifier)
        {
            List<SubstanceOutputTexture> outputs = substance.Output;

            for(int i=0; i < outputs.Count; i++)
            {
                if(outputs[i].Description.Identifier == identifier)
                {
                    return outputs[i].OutputTexture;
                }
            }

            return null;
        }

        #endregion
    }
}