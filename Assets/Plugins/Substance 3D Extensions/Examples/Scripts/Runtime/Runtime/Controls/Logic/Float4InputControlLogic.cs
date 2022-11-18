using System.Collections;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;
using UnityEngine;

namespace SOS.SubstanceExtensions.Examples
{
    public class Float4InputControlLogic : InputControlLogic<Vector4>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputFloat4 valueInput = (SubstanceInputFloat4)input;

            value = valueInput.Data;
        }


        public override Vector4 GetInputValue()
        {
            return CachedNativeGraph.GetInputFloat4(InputIndex);
        }


        public override void SetInputValue()
        {
            CachedNativeGraph.SetInputFloat4(InputIndex, Value);
        }


        public override void SetInputValueFromControl(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case ParameterInput4Control control:
                    if(!float.TryParse(control.Text, out float xValue))
                    {
                        xValue = 0f;
                    }

                    if(!float.TryParse(control.Text, out float yValue))
                    {
                        yValue = 0f;
                    }

                    if(!float.TryParse(control.Text, out float zValue))
                    {
                        zValue = 0f;
                    }

                    if(!float.TryParse(control.Text, out float wValue))
                    {
                        wValue = 0f;
                    }

                    Value = new Vector4(xValue, yValue, zValue, wValue);
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