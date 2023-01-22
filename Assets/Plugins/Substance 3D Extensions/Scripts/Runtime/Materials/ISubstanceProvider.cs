using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Interface implemented by objects that reference substance graphs.
    /// </summary>
    public interface ISubstanceProvider
    {
        /// <summary>
        /// Get an array of substances referenced by this provider.
        /// </summary>
        /// <returns>Array of substances referenced by the provider.</returns>
        SubstanceGraphSO[] GetSubstances();
    }
}