using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions.Examples
{
    public class OutputSizeControl : RuntimeParameterControl
    {
        [System.Serializable]
        public class OutputSizeOption
        {
            public string label;
            public SbsOutputSize value;
        }

        [SerializeField, Tooltip("Dropdown displaying output size.")]
        private Dropdown dropdown = null;
        [SerializeField]
        private OutputSizeOption[] options = new OutputSizeOption[0];

        public SbsOutputSize Value
        {
            get { return options[dropdown.value].value; }
        }


        public void SetValueWithoutNotify(SbsOutputSize value)
        {
            RefreshDropdownOptions();

            for(int i=0; i < options.Length; i++)
            {
                if (options[i].value == value)
                {
                    dropdown.SetValueWithoutNotify(i);
                    return;
                }
            }

            dropdown.SetValueWithoutNotify(0);
        }


        public void SetValueWithoutNotify(int value)
        {
            dropdown.SetValueWithoutNotify(value);
        }


        private void RefreshDropdownOptions()
        {
            if (dropdown.options.Count > 0) return;

            List<string> newOptions = new List<string>(options.Length);

            for (int i = 0; i < options.Length; i++)
            {
                newOptions.Add(options[i].label);
            }

            dropdown.AddOptions(newOptions);
        }


        private void OnDropdownValueChanged(int value)
        {
            SetInputValue();
        }


        private void Start()
        {
            RefreshDropdownOptions();

            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
    }
}