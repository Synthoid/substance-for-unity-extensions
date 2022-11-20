using UnityEngine;
using UnityEngine.UI;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    public class InvalidParameterControl : RuntimeParameterControl
    {
        private const string kErrorFormatText = "No control found for [{0} - {1}] input.";

        [SerializeField]
        private Text errorLabel = null;

        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            errorLabel.text = string.Format(kErrorFormatText, input.ValueType, input.Description.WidgetType);
        }

        public override object GetInputValueRaw()
        {
            return "INVALID";
        }


        public override void SetInputValue()
        {
            //Do nothing
        }
    }
}