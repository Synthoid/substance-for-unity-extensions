using System.Collections;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace SOS.SubstanceExtensions.Examples
{
    public class Float2InputControlLogic : InputControlLogic<Vector2>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputFloat2 valueInput = (SubstanceInputFloat2)input;

            value = valueInput.Data;
        }


        public override Vector2 GetInputValue()
        {
            return CachedNativeGraph.GetInputFloat2(InputIndex);
        }


        public override void SetInputValue()
        {
            CachedNativeGraph.SetInputFloat2(InputIndex, Value);
        }


        public override void SetInputValueFromControl(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case ParameterInput2Control control:
                    if (!float.TryParse(control.Text, out float xValue))
                    {
                        xValue = 0f;
                    }

                    if (!float.TryParse(control.Text, out float yValue))
                    {
                        yValue = 0f;
                    }

                    Value = new Vector2(xValue, yValue);
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