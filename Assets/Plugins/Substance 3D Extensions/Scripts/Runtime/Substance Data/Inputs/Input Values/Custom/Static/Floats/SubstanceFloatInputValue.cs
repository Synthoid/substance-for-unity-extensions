using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets float values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceFloatInputValue : SubstanceInputValueT<float>
    {
        [SerializeField, SubstanceInputTypeFilter(SbsInputTypeFilter.Float)]
        protected SubstanceParameter parameter = new SubstanceParameter();

        public override SubstanceParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override void SetInputValue(SubstanceNativeGraph graph)
        {
            graph.SetInputFloat(ParameterIndex, GetValue());
        }
    }
}