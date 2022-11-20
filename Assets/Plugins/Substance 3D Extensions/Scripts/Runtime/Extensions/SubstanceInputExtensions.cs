using UnityEngine;
using System;
using System.Reflection;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Extension methods for substance input classes.
    /// </summary>
    public static class SubstanceInputExtensions
    {
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
    }
}