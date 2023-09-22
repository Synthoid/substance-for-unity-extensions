using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceInputValue : ISubstanceInputValue
    {
        public abstract SubstanceParameter Parameter { get; set; }

        public string ParameterName { get { return Parameter.Name; } }
        public int ParameterIndex { get { return Parameter.Index; } }

        public abstract void SetInputValue(SubstanceNativeGraph graph);
    }

    /// <summary>
    /// Base class for data that sets values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceInputValueT<T> : SubstanceInputValue, ISubstanceInputValueT<T>
    {
        public abstract T GetValue();
    }
}