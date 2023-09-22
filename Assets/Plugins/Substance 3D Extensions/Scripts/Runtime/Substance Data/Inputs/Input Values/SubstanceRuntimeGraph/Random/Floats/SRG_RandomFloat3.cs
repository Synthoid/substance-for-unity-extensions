using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    public abstract class SRG_RandomFloat3 : SRG_RandomInputValueT<Vector3>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            graph.SetInputVector3(InputName, GetRandomValue());
        }
    }
}