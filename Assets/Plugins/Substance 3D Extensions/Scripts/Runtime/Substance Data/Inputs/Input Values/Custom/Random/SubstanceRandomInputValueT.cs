using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceRandomInputValueT<T> : SubstanceInputValue, ISubstanceRandomInputValue<T>
    {
        public abstract T GetRandomValue();
    }
}