using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Interface implemented by data values that set an input value on a substance graph.
    /// </summary>
    public interface ISubstanceInputValue
    {
        /// <summary>
        /// Data for the target parameter.
        /// </summary>
        SubstanceParameter Parameter { get; set; }
        /// <summary>
        /// Name of the target parameter.
        /// </summary>
        string ParameterName { get; }
        /// <summary>
        /// Index (ID) for the target parameter.
        /// </summary>
        int ParameterIndex { get; }

        /// <summary>
        /// Set the target input value on the given native graph.
        /// </summary>
        /// <param name="graph">Graph to set an input value on.</param>
        void SetInputValue(SubstanceNativeGraph graph);
        /// <summary>
        /// Asynchronously set the target input value on the given native graph.
        /// </summary>
        /// <param name="graph">Graph to set an input value on.</param>
        /// <param name="cancelToken">[Optional] CancellationToken for the task.</param>
        /// <returns>Task for the value set operation.</returns>
        Task SetInputValueAsync(SubstanceNativeGraph graph, CancellationToken cancelToken=default);
    }

    /// <summary>
    /// Interface implemented by data values that set an input value on a substance graph.
    /// </summary>
    public interface ISubstanceInputValueT<T> : ISubstanceInputValue
    {
        /// <summary>
        /// Get a value for a substance input.
        /// </summary>
        T GetValue();
    }
}