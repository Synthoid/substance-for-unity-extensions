using UnityEngine;
using UnityEngine.UI;
using Adobe.Substance;
using Adobe.Substance.Input;
using Adobe.Substance.Input.Description;

namespace SOS.SubstanceExtensions.Examples
{
    public class FloatSliderControl : FloatInputControl
    {
        [SerializeField, Tooltip("Slider for adjusting input value.")]
        private Slider slider = null;

        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            if(input.TryGetNumericalDescription(out ISubstanceInputDescNumerical description))
            {
                SubstanceInputDescNumericalFloat floatDesc = (SubstanceInputDescNumericalFloat)description;

                slider.minValue = floatDesc.MinValue;
                slider.maxValue = floatDesc.MaxValue;
            }
        }


        public override void SetValueWithoutNotify(float value)
        {
            base.SetValueWithoutNotify(value);

            slider.SetValueWithoutNotify(value);
        }


        protected override void OnTextEndEdit(string value)
        {
            base.OnTextEndEdit(value);

            slider.SetValueWithoutNotify(Value);
        }


        private void OnSliderChanged(float value)
        {
            input.SetTextWithoutNotify(value.ToString(kNumberFormat));

            Value = value;
        }


        protected override void Start()
        {
            base.Start();

            slider.onValueChanged.AddListener(OnSliderChanged);
        }
    }
}