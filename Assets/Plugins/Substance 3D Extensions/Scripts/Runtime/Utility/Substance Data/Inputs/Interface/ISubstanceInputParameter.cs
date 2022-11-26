using UnityEngine;
using System;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Interface implemented by data classes representing a substance input.
    /// </summary>
    public interface ISubstanceInputParameter
    {
        /// <summary>
        /// Name for the input parameter. This is based on the input's identifier value.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Index for the input parameter on the target graph.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// Value type for the parameter.
        /// </summary>
        public SubstanceValueType ValueType { get; }
        /// <summary>
        /// Inspector widget used for the input parameter. This is primarily used for tooling purposes.
        /// </summary>
        public SubstanceWidgetType WidgetType { get; }
        /// <summary>
        /// Min slider values used for float parameters. X is used for Float parameters. X, Y for Float2, etc.
        /// </summary>
        public Vector4 RangeMin { get; }
        /// <summary>
        /// Max slider values used for float parameters. X is used for Float parameters. X, Y for Float2, etc.
        /// </summary>
        public Vector4 RangeMax { get; }
        /// <summary>
        /// Min slider values used for integer parameters. X is used for Int parameters. X, Y for Int2, etc.
        /// </summary>
        public Vector4Int RangeIntMin { get; }
        /// <summary>
        /// Max slider values used for integer parameters. X is used for Int parameters. X, Y for Int2, etc.
        /// </summary>
        public Vector4Int RangeIntMax { get; }
    }
}