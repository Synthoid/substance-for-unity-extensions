using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    public abstract class SRG_RandomFloat4 : SRG_RandomInputValueT<Vector4>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            graph.SetInputVector4(InputName, GetRandomValue());
        }
    }
}