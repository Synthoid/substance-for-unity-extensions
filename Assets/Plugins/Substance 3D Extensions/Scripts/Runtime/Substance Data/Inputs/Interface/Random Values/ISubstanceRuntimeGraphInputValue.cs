using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Interface implemented by data values that set input values on a SubstanceRuntimeGraph component.
    /// </summary>
    public interface ISubstanceRuntimeGraphInputValue
    {
        /// <summary>
        /// Name of the substance input parameter to set.
        /// </summary>
        string InputName { get; set; }

        /// <summary>
        /// Set the target input value on the given graph.
        /// </summary>
        /// <param name="graph">Graph to set an input value on.</param>
        void SetInputValue(SubstanceRuntimeGraph graph);
    }
}