using UnityEngine;
using Adobe.Substance;
using System.Threading;
using System.Threading.Tasks;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets texture values on a SubstanceNativeGraph input.
    /// </summary>
    public abstract class SubstanceTextureInputValue : SubstanceInputValueT<Texture>
    {
        [SerializeField, SubstanceInputTypeFilter(SbsInputTypeFilter.Image)]
        protected SubstanceParameter parameter = new SubstanceParameter();

        public override SubstanceParameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public override void SetInputValue(SubstanceNativeGraph graph)
        {
            graph.SetInputTextureGPU(ParameterIndex, GetValue());
        }


        public override Task SetInputValueAsync(SubstanceNativeGraph graph, CancellationToken cancelToken=default)
        {
            return graph.SetInputTextureGPUAsync(ParameterIndex, GetValue());
        }
    }
}