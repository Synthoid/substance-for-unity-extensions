using UnityEngine;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Base class for logic overriding how control selection for a given value type occurs.
    /// </summary>
    public abstract class InputControlSelectLogic : ScriptableObject
    {
        /// <summary>
        /// Try to get a control prefab.
        /// </summary>
        /// <param name="input">Input to get a control prefab for.</param>
        /// <param name="controls">Array of existing controls.</param>
        /// <param name="control">Variable populated by the found control.</param>
        /// <returns>True if a prefab was found, false otherwise.</returns>
        public abstract bool TryGetControl(ISubstanceInput input, RuntimeParameterControlData[] controls, out RuntimeParameterControl control);
    }
}