using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Base class for data that sets random string values on a SubstanceRuntimeGraph input.
    /// </summary>
    public abstract class SRG_RandomString : SRG_RandomInputValueT<string>
    {
        public override void SetInputValue(SubstanceRuntimeGraph graph)
        {
            graph.SetInputString(InputName, GetRandomValue());
        }
    }
}