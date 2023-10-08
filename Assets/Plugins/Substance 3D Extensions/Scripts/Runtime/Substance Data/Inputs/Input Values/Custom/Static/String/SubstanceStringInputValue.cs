using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets string values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceStringInputValue : SubstanceInputValueT<string>
    {
        [SerializeField, SubstanceInputTypeFilter(SbsInputTypeFilter.String)]
        protected SubstanceParameter parameter = new SubstanceParameter();

        public override SubstanceParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override void SetInputValue(SubstanceNativeGraph graph)
        {
            graph.SetInputString(ParameterIndex, GetValue());
        }
    }
}