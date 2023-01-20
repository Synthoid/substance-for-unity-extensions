using UnityEngine;
using UnityEditor;
using Adobe.Substance;
using Adobe.Substance.Input;
using Adobe.SubstanceEditor;

namespace SOS.SubstanceExtensionsEditor
{
    public static class SubstanceGraphEditorExtensions
    {
        /// <summary>
        /// Returns true if SubstanceEditorEngine has a cached <see cref="SubstanceNativeGraph"/> for the substance.
        /// </summary>
        /// <param name="substance">Substance graph to check.</param>
        /// <returns>True if the substance has a native graph value cached by the editor engine. False otherwise.</returns>
        public static bool IsCachedByEditorEngine(this SubstanceGraphSO substance)
        {
            return SubstanceReflectionEditorUtility.TryGetHandlerFromInstance(substance, out SubstanceNativeGraph nativeGraph);
        }
    }
}