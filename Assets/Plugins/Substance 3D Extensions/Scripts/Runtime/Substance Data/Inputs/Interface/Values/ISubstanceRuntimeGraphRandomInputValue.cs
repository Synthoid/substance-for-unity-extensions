using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Interface implemented by data values that set random input values on a SubstanceRuntimeGraph component.
    /// </summary>
    public interface ISubstanceRuntimeGraphRandomInputValue<T> : ISubstanceRuntimeGraphInputValue
    {
        /// <summary>
        /// Get a random value for an input.
        /// </summary>
        T GetRandomValue();
    }
}