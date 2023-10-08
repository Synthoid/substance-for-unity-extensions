using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets float2 values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceFloat2InputValue : SubstanceInputValueT<Vector2>
    {
        [SerializeField, SubstanceInputTypeFilter(SbsInputTypeFilter.Float2)]
        protected SubstanceParameter parameter = new SubstanceParameter();

        public override SubstanceParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override void SetInputValue(SubstanceNativeGraph graph)
        {
            graph.SetInputFloat2(ParameterIndex, GetValue());
        }
    }
}