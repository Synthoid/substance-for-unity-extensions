using UnityEngine;
using UnityEngine.UI;
using Adobe.Substance;
using Adobe.Substance.Input;
using Adobe.Substance.Input.Description;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

namespace SOS.SubstanceExtensions.Examples
{
    public class ParameterSliderControl : ParameterInputControl
    {
        [SerializeField, Tooltip("Slider for adjusting input value.")]
        private Slider slider = null;

        public float SliderValue
        {
            get { return slider.value; }
            set { slider.value = value; }
        }

        public override void Initialize(SubstanceNativeGraph nativeGraph, SubstanceGraphSO substance, ISubstanceInput input)
        {
            if (input.TryGetNumericalDescription(out ISubstanceInputDescNumerical description))
            {
                switch(description)
                {
                    case SubstanceInputDescNumericalFloat desc:
                        slider.minValue = desc.MinValue;
                        slider.maxValue = desc.MaxValue;
                        break;
                    case SubstanceInputDescNumericalInt desc:
                        slider.wholeNumbers = true;
                        slider.minValue = (float)desc.MinValue;
                        slider.maxValue = (float)desc.MaxValue;
                        break;
                }
            }

            base.Initialize(nativeGraph, substance, input);
        }


        public override void SetInputValueWithoutNotify(string value)
        {
            base.SetInputValueWithoutNotify(value);

            if(!float.TryParse(value, out float newValue))
            {
                newValue = 0f;
            }

            slider.SetValueWithoutNotify(newValue);
        }


        public virtual void SetSliderValueWithoutNotify(float value)
        {
            slider.SetValueWithoutNotify(value);
            input.SetTextWithoutNotify(value.ToString(kNumberFormat));
        }


        protected override void OnTextEndEdit(string value)
        {
            if (!float.TryParse(value, out float newValue))
            {
                newValue = 0f;
                value = newValue.ToString(kNumberFormat);
                input.SetTextWithoutNotify(value);
            }

            slider.SetValueWithoutNotify(newValue);

            base.OnTextEndEdit(value);
        }


        private void OnSliderChanged(float value)
        {
            input.SetTextWithoutNotify(value.ToString(kNumberFormat));

            SetInputValue();
        }


        protected override void Start()
        {
            base.Start();

            slider.onValueChanged.AddListener(OnSliderChanged);
        }
    }
}
