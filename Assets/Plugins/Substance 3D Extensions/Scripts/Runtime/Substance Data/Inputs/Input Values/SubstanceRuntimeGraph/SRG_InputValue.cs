using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random values on a SubstanceRuntimeGraph input.
    /// </summary>
    public abstract class SRG_InputValue : ISubstanceRuntimeGraphInputValue
    {
        [SerializeField, Tooltip("Name of the substance input parameter to set.")]
        protected string inputName = "";

        public string InputName
        {
            get { return inputName; }
            set { inputName = value; }
        }

        public abstract void SetInputValue(SubstanceRuntimeGraph graph);
    }
}