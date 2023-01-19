using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Extension methods for substance input classes.
    /// </summary>
    public static class SubstanceInputExtensions
    {
        /// <summary>
        /// Get the object value for a substance input.
        /// </summary>
        /// <param name="input">Input to get the value of.</param>
        /// <returns>object value for the target input.</returns>
        public static object GetValue(this ISubstanceInput input)
        {
            switch(input)
            {
                case SubstanceInputFloat castInput: return castInput.Data;
                case SubstanceInputFloat2 castInput: return castInput.Data;
                case SubstanceInputFloat3 castInput: return castInput.Data;
                case SubstanceInputFloat4 castInput: return castInput.Data;
                case SubstanceInputInt castInput: return castInput.Data;
                case SubstanceInputInt2 castInput: return castInput.Data;
                case SubstanceInputInt3 castInput: return castInput.Data;
                case SubstanceInputInt4 castInput: return castInput.DataVector4Int();
                case SubstanceInputString castInput: return castInput.Data;
                case SubstanceInputTexture castInput: return castInput.GetTexture();
                case SubstanceInputFont castInput: return castInput.Data;
            }

            return null;
        }

        /// <summary>
        /// Set the value for a substance input.
        /// </summary>
        /// <param name="input">Input to set the value of.</param>
        /// <param name="value">New value for the input.</param>
        /// <returns>True if the value was set, or false otherwise.</returns>
        public static bool SetValue(this ISubstanceInput input, object value)
        {
            switch(input)
            {
                case SubstanceInputFloat castInput:
                    castInput.Data = (float)value;
                    return true;
                case SubstanceInputFloat2 castInput:
                    castInput.Data = (Vector2)value;
                    return true;
                case SubstanceInputFloat3 castInput:
                    castInput.Data = (Vector3)value;
                    return true;
                case SubstanceInputFloat4 castInput:
                    castInput.Data = (Vector4)value;
                    return true;
                case SubstanceInputInt castInput:
                    castInput.Data = (int)value;
                    return true;
                case SubstanceInputInt2 castInput:
                    castInput.Data = (Vector2Int)value;
                    return true;
                case SubstanceInputInt3 castInput:
                    castInput.Data = (Vector3Int)value;
                    return true;
                case SubstanceInputInt4 castInput:
                    if(value is Vector4Int)
                    {
                        Vector4Int vectorValue = (Vector4Int)value;

                        castInput.Data0 = vectorValue.x;
                        castInput.Data1 = vectorValue.y;
                        castInput.Data2 = vectorValue.z;
                        castInput.Data3 = vectorValue.w;
                    }

                    if(value is IList<int>)
                    {
                        IList<int> arrayValue = (IList<int>)value;

                        castInput.Data0 = arrayValue[0];
                        castInput.Data1 = arrayValue[1];
                        castInput.Data2 = arrayValue[2];
                        castInput.Data3 = arrayValue[3];
                    }

                    return true;
                case SubstanceInputString castInput:
                    castInput.Data = (string)value;
                    return true;
                case SubstanceInputTexture castInput:
                    castInput.SetTexture((Texture2D)value);
                    return true;
                case SubstanceInputFont castInput:
                    Debug.LogWarning("[SOS - Substance Extensions] Font inputs are not currently supported...");
                    return false;
                default:
                    Debug.LogError(string.Format("[SOS - Supstance Extensions] Unrecognized input type: {0}", input.GetType().FullName));
                    break;
            }

            return false;
        }

        /// <summary>
        /// Copy the data value from input A to input B.
        /// </summary>
        /// <param name="inputA">Input to copy values from.</param>
        /// <param name="inputB">Input to paste values to.</param>
        /// <returns>True if the copy operation was a success. False otherwise.</returns>
        public static bool CopyTo(this ISubstanceInput inputA, ISubstanceInput inputB)
        {
            if(inputA.GetType() != inputB.GetType()) return false;

            switch(inputA)
            {
                case SubstanceInputFloat castInput:
                    SubstanceInputFloat floatInputB = (SubstanceInputFloat)inputB;
                    floatInputB.Data = castInput.Data;
                    return true;
                case SubstanceInputFloat2 castInput:
                    SubstanceInputFloat2 float2InputB = (SubstanceInputFloat2)inputB;
                    float2InputB.Data = castInput.Data;
                    return true;
                case SubstanceInputFloat3 castInput:
                    SubstanceInputFloat3 float3InputB = (SubstanceInputFloat3)inputB;
                    float3InputB.Data = castInput.Data;
                    return true;
                case SubstanceInputFloat4 castInput:
                    SubstanceInputFloat4 float4InputB = (SubstanceInputFloat4)inputB;
                    float4InputB.Data = castInput.Data;
                    return true;
                case SubstanceInputInt castInput:
                    SubstanceInputInt intInputB = (SubstanceInputInt)inputB;
                    intInputB.Data = castInput.Data;
                    return true;
                case SubstanceInputInt2 castInput:
                    SubstanceInputInt2 int2InputB = (SubstanceInputInt2)inputB;
                    int2InputB.Data = castInput.Data;
                    return true;
                case SubstanceInputInt3 castInput:
                    SubstanceInputInt3 int3InputB = (SubstanceInputInt3)inputB;
                    int3InputB.Data = castInput.Data;
                    return true;
                case SubstanceInputInt4 castInput:
                    SubstanceInputInt4 int4InputB = (SubstanceInputInt4)inputB;
                    int4InputB.Data0 = castInput.Data0;
                    int4InputB.Data1 = castInput.Data1;
                    int4InputB.Data2 = castInput.Data2;
                    int4InputB.Data3 = castInput.Data3;
                    return true;
                case SubstanceInputString castInput:
                    SubstanceInputString stringInputB = (SubstanceInputString)inputB;
                    stringInputB.Data = castInput.Data;
                    return true;
                case SubstanceInputTexture castInput:
                    SubstanceInputTexture imageInputB = (SubstanceInputTexture)inputB;

                    imageInputB.SetTexture(castInput.GetTexture());
                    return true;
                case SubstanceInputFont castInput:
                    Debug.LogWarning("[SOS - Substance Extensions] Font inputs are not currently supported...");
                    return false;
            }

            return false;
        }

        #region Int4

        /// <summary>
        /// Get the target Int4 input's value as a <see cref="Vector4Int"/>.
        /// </summary>
        /// <param name="input">Input get pull values from.</param>
        /// <returns><see cref="Vector4Int"/> representing the input's value.</returns>
        public static Vector4Int DataVector4Int(this SubstanceInputInt4 input)
        {
            return new Vector4Int(input.Data0, input.Data1, input.Data2, input.Data3);
        }

        #endregion

        #region Texture

        private static FieldInfo textureDataField = null;

        private static FieldInfo TextureDataField
        {
            get
            {
                if (textureDataField == null)
                {
                    Type inputType = typeof(SubstanceInputTexture);
                    textureDataField = inputType.GetField("Data", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                }

                return textureDataField;
            }
        }

        /// <summary>
        /// Get the texture assigned to the target <see cref="SubstanceInputTexture"/>.
        /// </summary>
        /// <param name="input">Input to obtain the texture reference from.</param>
        /// <returns><see cref="Texture2D"/> assigned to the target input.</returns>
        public static Texture2D GetTexture(this SubstanceInputTexture input)
        {
            return (Texture2D)TextureDataField.GetValue(input);
        }

        /// <summary>
        /// Set the texture reference for the target subtance input.
        /// </summary>
        /// <param name="input">Input to set the texture reference on.</param>
        /// <param name="value">New value for the input.</param>
        public static void SetTexture(this SubstanceInputTexture input, Texture2D value)
        {
            TextureDataField.SetValue(input, value);
        }

        #endregion

        #region List

        //TODO: Return render result?
        /// <summary>
        /// Updates the destination list of substance inputs with valid values from the source list.
        /// </summary>
        /// <typeparam name="T">Expected type for the lists.</typeparam>
        /// <param name="source">List containing inputs to copy values from.</param>
        /// <param name="destination">List containing inputs to paste values into.</param>
        /// <returns>Number of inputs from the source list that were updated on the destination list.</returns>
        public static int UpdateInputList<T>(IList<T> source, IList<T> destination) where T : ISubstanceInput
        {
            int count = 0;

            for(int i=0; i < source.Count; i++)
            {
                int index = i;
                int targetIndex = destination.IndexOf((input) => { return input.Description.Identifier == source[index].Description.Identifier; });

                //Skip if no input with matching identifier exists
                if(targetIndex < 0)
                {
                    Debug.LogWarning(string.Format("[SOS - Substance Extensions] No input with identifier [{0}] in destination inputs.", source[i].Description.Identifier));
                    continue;
                }

                //Skip if input failed to update for some reason.
                if(!source[index].CopyTo(destination[targetIndex]))
                {
                    Debug.LogWarning(string.Format("[SOS - Substance Extensions] Could not update input with identifier [{0}]", source[index].Description.Identifier));
                    continue;
                }

                count++;
            }

            return count;
        }

        #endregion
    }
}