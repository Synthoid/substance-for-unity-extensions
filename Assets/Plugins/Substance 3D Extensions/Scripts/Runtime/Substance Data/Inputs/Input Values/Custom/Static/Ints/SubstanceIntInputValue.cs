using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets int values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceIntInputValue : SubstanceInputValueT<int>
    {
        [SerializeField, SubstanceInputTypeFilter(SbsInputTypeFilter.Int)]
        protected SubstanceParameter parameter = new SubstanceParameter();

        public override SubstanceParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override void SetInputValue(SubstanceNativeGraph graph)
        {
            graph.SetInputInt(ParameterIndex, GetValue());
        }
    }
}