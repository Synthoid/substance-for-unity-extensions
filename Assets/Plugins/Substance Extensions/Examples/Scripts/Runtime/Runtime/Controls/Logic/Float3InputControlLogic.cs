using System.Collections;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;
using UnityEngine;

namespace SOS.SubstanceExtensions.Examples
{
    public class Float3InputControlLogic : InputControlLogic<Vector3>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputFloat3 valueInput = (SubstanceInputFloat3)input;

            value = valueInput.Data;
        }


        public override Vector3 GetInputValue()
        {
            return CachedNativeGraph.GetInputFloat3(InputIndex);
        }


        public override void SetInputValue()
        {
            CachedNativeGraph.SetInputFloat3(InputIndex, Value);
        }


        public override void SetInputValueFromControl(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case ParameterInput3Control control:
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

                    Value = new Vector3(xValue, yValue, zValue);
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
                case ParameterInput3Control control:
                    control.SetInputValuesWithoutNotify(Value.x.ToString(), Value.y.ToString(), Value.z.ToString());
                    break;
            }
        }
    }
}