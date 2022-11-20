using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    public class FloatInputControlLogic : InputControlLogic<float>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputFloat valueInput = (SubstanceInputFloat)input;

            value = valueInput.Data;
        }


        public override float GetInputValue()
        {
            return CachedNativeGraph.GetInputFloat(InputIndex);
        }


        public override void SetInputValue()
        {
            CachedNativeGraph.SetInputFloat(InputIndex, Value);
        }


        public override void SetInputValueFromControl(RuntimeParameterControl inputControl)
        {
            switch(inputControl)
            {
                case ParameterSliderControl control:
                    Value = control.SliderValue;
                    break;
                case ParameterInputControl control:
                    if(!float.TryParse(control.Text, out float newValue))
                    {
                        newValue = 0f;
                    }

                    Value = newValue;
                    break;
                default:
                    Debug.LogWarning(string.Format(kControlTypeInvalidFormatString, inputControl.GetType().FullName, this.GetType().FullName));
                    break;
            }
        }


        public override void SetControlValues(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case ParameterSliderControl control:
                    control.SetSliderValueWithoutNotify(Value);
                    break;
                case ParameterInputControl control:
                    control.SetInputValueWithoutNotify(Value.ToString());
                    break;
            }
        }
    }
}