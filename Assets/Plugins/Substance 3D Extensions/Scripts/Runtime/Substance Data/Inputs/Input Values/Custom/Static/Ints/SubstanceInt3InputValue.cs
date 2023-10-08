using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets int3 values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceInt3InputValue : SubstanceInputValueT<Vector3Int>
    {
        [SerializeField, SubstanceInputTypeFilter(SbsInputTypeFilter.Int3)]
        protected SubstanceParameter parameter = new SubstanceParameter();

        public override SubstanceParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override void SetInputValue(SubstanceNativeGraph graph)
        {
            graph.SetInputInt3(ParameterIndex, GetValue());
        }
    }
}