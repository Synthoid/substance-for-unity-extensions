using UnityEngine;
using UnityEngine.UI;

namespace SOS.SubstanceExtensions.Examples
{
    public class FloatInputControl : FloatParameterControl
    {

        [SerializeField, Tooltip("Input field accepting numbers.")]
        protected InputField input = null;


        public override void SetValueWithoutNotify(float value)
        {
            base.SetValueWithoutNotify(value);

            input.SetTextWithoutNotify(value.ToString(kNumberFormat));
        }


        protected virtual void OnTextEndEdit(string value)
        {
            if(!float.TryParse(value, out float newValue))
            {
                newValue = 0f;
            }

            Value = newValue;
        }


        protected virtual void Start()
        {
            input.onEndEdit.AddListener(OnTextEndEdit);
        }
    }
}
