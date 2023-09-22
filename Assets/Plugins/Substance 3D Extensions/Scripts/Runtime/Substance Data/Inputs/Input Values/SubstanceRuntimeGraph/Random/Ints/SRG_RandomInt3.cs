using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random int3 values on a SubstanceRuntimeGraph input.
    /// </summary>
    public abstract class SRG_RandomInt3 : SRG_RandomInputValueT<Vector3Int>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            graph.SetInputVector3Int(InputName, GetRandomValue());
        }
    }
}