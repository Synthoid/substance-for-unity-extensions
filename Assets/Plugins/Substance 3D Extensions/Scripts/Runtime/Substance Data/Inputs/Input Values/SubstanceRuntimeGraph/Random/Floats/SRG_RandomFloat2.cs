using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    public abstract class SRG_RandomFloat2 : SRG_RandomInputValueT<Vector2>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            graph.SetInputVector2(InputName, GetRandomValue());
        }
    }
}