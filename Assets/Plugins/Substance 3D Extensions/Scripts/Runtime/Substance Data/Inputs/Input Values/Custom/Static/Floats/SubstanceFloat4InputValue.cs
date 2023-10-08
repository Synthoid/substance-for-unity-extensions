using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets float4 values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceFloat4InputValue : SubstanceInputValueT<Vector4>
    {
        [SerializeField, SubstanceInputTypeFilter(SbsInputTypeFilter.Float4)]
        protected SubstanceParameter parameter = new SubstanceParameter();

        public override SubstanceParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override void SetInputValue(SubstanceNativeGraph graph)
        {
            graph.SetInputFloat4(ParameterIndex, GetValue());
        }
    }
}