using System.Collections;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace SOS.SubstanceExtensions.Examples
{
    public class StringInputControlLogic : InputControlLogic<string>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputString valueInput = (SubstanceInputString)input;

            value = valueInput.Data;
        }


        public override string GetInputValue()
        {
            return CachedNativeGraph.GetInputString(InputIndex);
        }


        public override void SetInputValue()
        {
            CachedNativeGraph.SetInputString(InputIndex, Value);
        }


        public override void SetInputValueFromControl(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case ParameterInputControl control:
                    Value = control.Text;
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
                case ParameterInputControl control:
                    control.SetInputValueWithoutNotify(Value);
                    break;
            }
        }
    }
}