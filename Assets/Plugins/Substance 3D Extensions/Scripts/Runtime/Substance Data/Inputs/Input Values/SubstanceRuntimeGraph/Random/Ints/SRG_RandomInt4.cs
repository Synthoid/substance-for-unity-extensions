using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random int4 values on a SubstanceRuntimeGraph input.
    /// </summary>
    public abstract class SRG_RandomInt4 : SRG_RandomInputValueT<Vector4Int>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            Vector4Int value = GetRandomValue();

            graph.SetInputVector4Int(InputName, value.x, value.y, value.z, value.w);
        }
    }
}