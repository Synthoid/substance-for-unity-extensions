using UnityEngine;

namespace SOS.SubstanceExtensions.Examples
{
    public class InvalidControlLogic : InputControlLogic
    {
        public override void SetControlValues(RuntimeParameterControl control)
        {

        }


        public override void SetInputValueFromControl(RuntimeParameterControl control)
        {

        }


        public override object GetInputValueRaw()
        {
            return "<Invalid>";
        }


        public override void SetInputValueRaw(object newValue)
        {

        }


        public override void SetInputValue()
        {

        }
    }
}