using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random Color values on a SubstanceRuntimeGraph input.
    /// </summary>
    public abstract class SRG_RandomColor : SRG_RandomInputValueT<Color>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            graph.SetInputColor(InputName, GetRandomValue());
        }
    }
}