using UnityEngine;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Extension methods for substanve input classes.
    /// </summary>
    public static class SubstanceInputExtensions
    {
        /// <summary>
        /// Get the target Int4 input's value as a <see cref="Vector4Int"/>.
        /// </summary>
        /// <param name="input">Input get pull values from.</param>
        /// <returns><see cref="Vector4Int"/> representing the input's value.</returns>
        public static Vector4Int DataVector4Int(this SubstanceInputInt4 input)
        {
            return new Vector4Int(input.Data0, input.Data1, input.Data2, input.Data3);
        }
    }
}