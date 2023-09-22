using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random int2 values on a SubstanceRuntimeGraph input.
    /// </summary>
    public abstract class SRG_RandomInt2 : SRG_RandomInputValueT<Vector2Int>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            graph.SetInputVector2Int(InputName, GetRandomValue());
        }
    }
}