using System.Collections;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;
using UnityEngine;

namespace SOS.SubstanceExtensions.Examples
{
    public abstract class InputControlLogic : ScriptableObject
    {
        protected const string kControlTypeInvalidFormatString = "RuntimeParameterControl [{0}] not supported for [{1}].";

        public enum InputLogicType
        {
            None    = 0,
            Float   = 1,
            Float2  = 2,
            Float3  = 3,
            Float4  = 4,
            Int     = 5,
            Int2    = 6,
            Int3    = 7,
            Int4    = 8,
            String  = 9,
            Texture = 10,
            Custom  = 20
        }

        public SubstanceNativeGraph CachedNativeGraph { get; protected set; }
        public SubstanceGraphSO CachedSubstance { get; protected set; }
        public string InputIdentifier { get; protected set; }
        public int InputIndex { get; protected set; }

        /// <summary>
        /// Get the bound input value on the native graph.
        /// </summary>
        /// <returns>object representing the current value on the cached native graph's target input.</returns>
        public abstract object GetInputValueRaw();
        /// <summary>
        /// Set the bound input value on the native graph.
        /// </summary>
        /// <param name="newValue">object value representing the new value for the input.</param>
        public abstract void SetInputValueRaw(object newValue);
        /// <summary>
        /// Set the bound input value on the native graph.
        /// </summary>
        public abstract void SetInputValue();

        public virtual void Initialize(SubstanceNativeGraph nativeGraph, SubstanceGraphSO substance, ISubstanceInput input)
        {
            CachedNativeGraph = nativeGraph;
            CachedSubstance = substance;
            InputIdentifier = input.Description.Identifier;
            InputIndex = input.Index;
        }


        public abstract void SetInputValueFromControl(RuntimeParameterControl control);
        public abstract void SetControlValues(RuntimeParameterControl control);


        public void Release()
        {
            CachedNativeGraph = null;
            CachedSubstance = null;
        }
    }


    public abstract class InputControlLogic<T> : InputControlLogic
    {
        protected T value;

        public virtual T Value
        {
            get { return value; }
            protected set
            {
                this.value = value;

                SetInputValue();
            }
        }

        /// <summary>
        /// Get the bound input value on the native graph.
        /// </summary>
        /// <returns>Cast value representing the current value on the cached native graph's target input.</returns>
        public abstract T GetInputValue();


        public override object GetInputValueRaw()
        {
            return GetInputValue();
        }


        public override void SetInputValueRaw(object newValue)
        {
            Value = (T)newValue;
        }


        public override void SetControlValues(RuntimeParameterControl inputControl)
        {
            switch(inputControl)
            {
                /*case ParameterSliderControl control:
                    control.SetInputValueWithoutNotify(Value.ToString());
                    break;*/
                case ParameterInputControl control:
                    control.SetInputValueWithoutNotify(Value.ToString());
                    break;
            }
        }
    }
}