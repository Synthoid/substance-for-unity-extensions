using UnityEngine;
using UnityEngine.UI;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    public class ParameterInputControl : RuntimeParameterControl
    {
        [SerializeField, Tooltip("InputField accepting player input.")]
        protected InputField input = null;

        public virtual string Text
        {
            get { return input.text; }
            set { input.text = value; }
        }


        public virtual void SetInputValueWithoutNotify(string value)
        {
            input.SetTextWithoutNotify(value);
        }


        protected virtual void OnTextEndEdit(string value)
        {
            SetInputValue();
        }


        protected virtual void Start()
        {
            input.onEndEdit.AddListener(OnTextEndEdit);
        }
    }
}