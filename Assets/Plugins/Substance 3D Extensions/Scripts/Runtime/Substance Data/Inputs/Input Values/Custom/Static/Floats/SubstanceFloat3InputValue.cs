using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets float3 values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceFloat3InputValue : SubstanceInputValueT<Vector3>
    {
        [SerializeField, SubstanceInputTypeFilter(SbsInputTypeFilter.Float3)]
        protected SubstanceParameter parameter = new SubstanceParameter();

        public override SubstanceParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override void SetInputValue(SubstanceNativeGraph graph)
        {
            graph.SetInputFloat3(ParameterIndex, GetValue());
        }
    }
}