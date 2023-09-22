using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random float values on a SubstanceRuntimeGraph input.
    /// </summary>
    public abstract class SRG_RandomFloat : SRG_RandomInputValueT<float>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            graph.SetInputFloat(InputName, GetRandomValue());
        }
    }
}