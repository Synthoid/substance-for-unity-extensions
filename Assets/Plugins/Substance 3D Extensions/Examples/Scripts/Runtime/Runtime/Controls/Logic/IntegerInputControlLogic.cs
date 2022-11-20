using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    public class IntegerInputControlLogic : InputControlLogic<int>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputInt valueInput = (SubstanceInputInt)input;

            value = valueInput.Data;
        }


        public override int GetInputValue()
        {
            return CachedNativeGraph.GetInputInt(InputIndex);
        }


        public override void SetInputValue()
        {
            CachedNativeGraph.SetInputInt(InputIndex, Value);
        }


        public override void SetInputValueFromControl(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case ParameterSliderControl control:
                    Value = Mathf.RoundToInt(control.SliderValue);
                    break;
                case ParameterInputControl control:
                    if (!int.TryParse(control.Text, out int newValue))
                    {
                        newValue = 0;
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
                    control.SetSliderValueWithoutNotify((float)Value);
                    break;
                case ParameterInputControl control:
                    control.SetInputValueWithoutNotify(Value.ToString());
                    break;
            }
        }
    }
}