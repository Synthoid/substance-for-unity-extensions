using System.Collections;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace SOS.SubstanceExtensions.Examples
{
    public class Integer3InputControlLogic : InputControlLogic<Vector3Int>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputInt3 valueInput = (SubstanceInputInt3)input;

            value = valueInput.Data;
        }


        public override Vector3Int GetInputValue()
        {
            return CachedNativeGraph.GetInputInt3(InputIndex);
        }


        public override void SetInputValue()
        {
            CachedNativeGraph.SetInputInt3(InputIndex, Value);
        }


        public override void SetInputValueFromControl(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case ParameterInput3Control control:
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

                    Value = new Vector3Int(xValue, yValue, zValue);
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