using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets color values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceColorInputValue : SubstanceInputValueT<Color>
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