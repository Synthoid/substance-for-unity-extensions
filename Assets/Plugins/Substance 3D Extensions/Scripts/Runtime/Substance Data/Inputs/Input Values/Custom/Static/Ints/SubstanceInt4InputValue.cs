using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets int4 values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceInt4InputValue : SubstanceInputValueT<Vector4Int>
    {
        [SerializeField, SubstanceInputTypeFilter(SbsInputTypeFilter.Int4)]
        protected SubstanceParameter parameter = new SubstanceParameter();

        public override SubstanceParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override void SetInputValue(SubstanceNativeGraph graph)
        {
            graph.SetInputInt4(ParameterIndex, GetValue());
        }
    }
}