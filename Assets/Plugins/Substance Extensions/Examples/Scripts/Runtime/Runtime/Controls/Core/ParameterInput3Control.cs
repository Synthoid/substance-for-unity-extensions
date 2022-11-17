using UnityEngine;
using UnityEngine.UI;

namespace SOS.SubstanceExtensions.Examples
{
    public class ParameterInput3Control : ParameterInput2Control
    {
        [SerializeField, Tooltip("InputField accepting player input.")]
        protected InputField input3 = null;

        public virtual string Text3
        {
            get { return input3.text; }
            set { input3.text = value; }
        }


        public virtual void SetInputValuesWithoutNotify(string text1, string text2, string text3)
        {
            SetInputValueWithoutNotify(text1);
            SetInput2ValueWithoutNotify(text2);
            SetInput3ValueWithoutNotify(text3);
        }


        public virtual void SetInput3ValueWithoutNotify(string value)
        {
            input3.SetTextWithoutNotify(value);
        }


        protected virtual void OnText3EndEdit(string value)
        {
            SetInputValue();
        }


        protected override void Start()
        {
            base.Start();

            input3.onEndEdit.AddListener(OnText3EndEdit);
        }
    }
}