using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random float values on a SubstanceRuntimeGraph input.
    /// </summary>
    public abstract class SRG_RandomInt : SRG_RandomInputValueT<int>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            graph.SetInputInt(InputName, GetRandomValue());
        }
    }
}