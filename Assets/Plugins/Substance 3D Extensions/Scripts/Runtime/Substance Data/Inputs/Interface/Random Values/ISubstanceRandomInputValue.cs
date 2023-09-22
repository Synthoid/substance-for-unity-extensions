using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Interface implemented by data values that set a random input value on a substance graph.
    /// </summary>
    public interface ISubstanceRandomInputValue<T> : ISubstanceInputValue
    {
        /// <summary>
        /// Get a random value for an input.
        /// </summary>
        T GetRandomValue();
    }
}