using UnityEngine;
using UnityEngine.UI;

namespace SOS.SubstanceExtensions.Examples
{
    public class ParameterInput2Control : ParameterInputControl
    {
        [SerializeField, Tooltip("InputField accepting player input.")]
        protected InputField input2 = null;

        public virtual string Text2
        {
            get { return input2.text; }
            set { input2.text = value; }
        }


        public virtual void SetInputValuesWithoutNotify(string text1, string text2)
        {
            SetInputValueWithoutNotify(text1);
            SetInput2ValueWithoutNotify(text2);
        }


        public virtual void SetInput2ValueWithoutNotify(string value)
        {
            input2.SetTextWithoutNotify(value);
        }


        protected virtual void OnText2EndEdit(string value)
        {
            SetInputValue();
        }


        protected override void Start()
        {
            base.Start();

            input2.onEndEdit.AddListener(OnText2EndEdit);
        }
    }
}