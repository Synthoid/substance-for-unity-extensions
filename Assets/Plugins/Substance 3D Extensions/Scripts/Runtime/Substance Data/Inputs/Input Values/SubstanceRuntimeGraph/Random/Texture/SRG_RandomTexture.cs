using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random texture values on a SubstanceRuntimeGraph input.
    /// </summary>
    public abstract class SRG_RandomTexture : SRG_RandomInputValueT<Texture2D>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            graph.SetInputTexture(InputName, GetRandomValue());
        }
    }
}