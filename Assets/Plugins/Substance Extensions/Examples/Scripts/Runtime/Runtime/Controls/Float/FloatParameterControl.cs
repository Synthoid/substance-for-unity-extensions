using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    public abstract class FloatParameterControl : RuntimeParameterControl<float>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputFloat valueInput = (SubstanceInputFloat)input;

            SetValueWithoutNotify(valueInput.Data);
        }


        public override float GetInputValue()
        {
            return CachedNativeGraph.GetInputFloat(InputIndex);
        }


        public override void SetInputValue()
        {
            CachedNativeGraph.SetInputFloat(InputIndex, Value);
        }
    }
}