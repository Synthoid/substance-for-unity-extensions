using System.Collections;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;
using UnityEngine;

namespace SOS.SubstanceExtensions.Examples
{
    public class Integer4InputControlLogic : InputControlLogic<Vector4Int>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputInt4 valueInput = (SubstanceInputInt4)input;

            value = valueInput.DataVector4Int();
        }


        public override Vector4Int GetInputValue()
        {
            return new Vector4Int(CachedNativeGraph.GetInputInt4(InputIndex));
        }


        public override void SetInputValue()
        {
            CachedNativeGraph.SetInputInt4(InputIndex, Value);
        }


        public override void SetInputValueFromControl(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case ParameterInput4Control control:
                    if (!int.TryParse(control.Text, out int xValue))
                    {
                        xValue = 0;
                    }

                    if (!int.TryParse(control.Text, out int yValue))
                    {
                        yValue = 0;
                    }

                    if (!int.TryParse(control.Text, out int zValue))
                    {
                        zValue = 0;
                    }

                    if (!int.TryParse(control.Text, out int wValue))
                    {
                        wValue = 0;
                    }

                    Value = new Vector4Int(xValue, yValue, zValue, wValue);
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
                case ParameterInput4Control control:
                    control.SetInputValuesWithoutNotify(Value.x.ToString(), Value.y.ToString(), Value.z.ToString(), Value.w.ToString());
                    break;
            }
        }
    }
}