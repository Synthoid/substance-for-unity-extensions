using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    public abstract class RuntimeParameterControl : MonoBehaviour
    {
        [System.Serializable]
        public class ControlEvent : UnityEvent { }

        protected const string kNumberFormat = "0.0";

        [SerializeField, Tooltip("Label for the control. Show's the bound input's label value.")]
        protected Text label = null;
        [SerializeField, Tooltip("Invoked when the control's value is changed.")]
        protected ControlEvent m_OnValueChanged = new ControlEvent();

        public ControlEvent onValueChanged
        {
            get { return m_OnValueChanged; }
            set { m_OnValueChanged = value; }
        }

        public SubstanceNativeGraph CachedNativeGraph { get; protected set; }
        public int InputIndex { get; protected set; }

        /// <summary>
        /// Get the bound input value on the native graph.
        /// </summary>
        /// <returns>object representing the current value on the cached native graph's target input.</returns>
        public abstract object GetInputValueRaw();
        /// <summary>
        /// Set the bound input value on the native graph.
        /// </summary>
        public abstract void SetInputValue();

        public virtual void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            CachedNativeGraph = nativeGraph;
            InputIndex = input.Index;

            label.text = input.Description.Label;
        }


        public void Cleanup()
        {
            CachedNativeGraph = null;
        }
    }


    public abstract class RuntimeParameterControl<T> : RuntimeParameterControl
    {
        protected T value;

        public virtual T Value
        {
            get { return value; }
            protected set
            {
                this.value = value;

                SetInputValue();

                onValueChanged.Invoke();
            }
        }

        /// <summary>
        /// Get the bound input value on the native graph.
        /// </summary>
        /// <returns>Cast value representing the current value on the cached native graph's target input.</returns>
        public abstract T GetInputValue();

        /// <summary>
        /// Update the visuals for the control without firing any events.
        /// </summary>
        /// <param name="value">New value for the control.</param>
        public virtual void SetValueWithoutNotify(T value)
        {
            this.value = value;
        }


        public override object GetInputValueRaw()
        {
            return GetInputValue();
        }
    }
}
