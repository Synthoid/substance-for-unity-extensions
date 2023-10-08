using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets int2 values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceInt2InputValue : SubstanceInputValueT<Vector2Int>
    {
        [SerializeField, SubstanceInputTypeFilter(SbsInputTypeFilter.Int2)]
        protected SubstanceParameter parameter = new SubstanceParameter();

        public override SubstanceParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override void SetInputValue(SubstanceNativeGraph graph)
        {
            graph.SetInputInt2(ParameterIndex, GetValue());
        }
    }
}