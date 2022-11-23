using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Misc extension methods that don't really warrant having their own extension scripts.
    /// </summary>
    public static class MiscExtensions
    {
        /// <summary>
        /// Converts a <see cref="SubstanceValueType"/> to its corresponding <see cref="SbsInputTypeFilter"/> value.
        /// </summary>
        /// <param name="valueType">Value type to convert.</param>
        /// <returns>Filter value associated with the value type.</returns>
        public static SbsInputTypeFilter ToFilter(this SubstanceValueType valueType)
        {
            switch(valueType)
            {
                case SubstanceValueType.Float: return SbsInputTypeFilter.Float;
                case SubstanceValueType.Float2: return SbsInputTypeFilter.Float2;
                case SubstanceValueType.Float3: return SbsInputTypeFilter.Float3;
                case SubstanceValueType.Float4: return SbsInputTypeFilter.Float4;
                case SubstanceValueType.Int: return SbsInputTypeFilter.Int;
                case SubstanceValueType.Int2: return SbsInputTypeFilter.Int2;
                case SubstanceValueType.Int3: return SbsInputTypeFilter.Int3;
                case SubstanceValueType.Int4: return SbsInputTypeFilter.Int4;
                case SubstanceValueType.Image: return SbsInputTypeFilter.Image;
                case SubstanceValueType.String: return SbsInputTypeFilter.String;
                case SubstanceValueType.Font: return SbsInputTypeFilter.Font;
            }

            return SbsInputTypeFilter.None;
        }
    }
}