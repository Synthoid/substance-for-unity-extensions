using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random values on a SubstanceRuntimeGraph input.
    /// </summary>
    public abstract class SRG_RandomInputValueT<T> : SRG_InputValue, ISubstanceRuntimeGraphRandomInputValue<T>
    {
        public abstract T GetRandomValue();
    }
}