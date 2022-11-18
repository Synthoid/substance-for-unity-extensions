using System.Collections;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;
using UnityEngine;

namespace SOS.SubstanceExtensions.Examples
{
    public class Integer2InputControlLogic : InputControlLogic<Vector2Int>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputInt2 valueInput = (SubstanceInputInt2)input;

            value = valueInput.Data;
        }


        public override Vector2Int GetInputValue()
        {
            return CachedNativeGraph.GetInputInt2(InputIndex);
        }


        public override void SetInputValue()
        {
            CachedNativeGraph.SetInputInt2(InputIndex, Value);
        }


        public override void SetInputValueFromControl(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case ParameterInput2Control control:
                    if (!int.TryParse(control.Text, out int xValue))
                    {
                        xValue = 0;
                    }

                    if (!int.TryParse(control.Text, out int yValue))
                    {
                        yValue = 0;
                    }

                    Value = new Vector2Int(xValue, yValue);
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
                case ParameterInput2Control control:
                    control.SetInputValuesWithoutNotify(Value.x.ToString(), Value.y.ToString());
                    break;
            }
        }
    }
}