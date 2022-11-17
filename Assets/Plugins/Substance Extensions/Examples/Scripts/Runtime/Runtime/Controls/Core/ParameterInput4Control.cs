using UnityEngine;
using UnityEngine.UI;

namespace SOS.SubstanceExtensions.Examples
{
    public class ParameterInput4Control : ParameterInput3Control
    {
        [SerializeField, Tooltip("InputField accepting player input.")]
        protected InputField input4 = null;

        public virtual string Text4
        {
            get { return input4.text; }
            set { input4.text = value; }
        }


        public virtual void SetInputValuesWithoutNotify(string text1, string text2, string text3, string text4)
        {
            SetInputValueWithoutNotify(text1);
            SetInput2ValueWithoutNotify(text2);
            SetInput3ValueWithoutNotify(text3);
            SetInput4ValueWithoutNotify(text4);
        }


        public virtual void SetInput4ValueWithoutNotify(string value)
        {
            input4.SetTextWithoutNotify(value);
        }


        protected virtual void OnText4EndEdit(string value)
        {
            SetInputValue();
        }


        protected override void Start()
        {
            base.Start();

            input4.onEndEdit.AddListener(OnText4EndEdit);
        }
    }
}