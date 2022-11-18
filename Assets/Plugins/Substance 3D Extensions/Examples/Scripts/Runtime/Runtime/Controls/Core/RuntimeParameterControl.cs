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

        protected const string kNumberFormat = "G2";

        [SerializeField]
        protected InputControlLogic.InputLogicType type = InputControlLogic.InputLogicType.None;
        [SerializeField]
        protected InputControlLogic logic = null;
        [SerializeField, Tooltip("Label for the control. Show's the bound input's label value.")]
        protected Text label = null;
        [SerializeField, Tooltip("Label for the input's identifier.")]
        protected Text identifierLabel = null;
        [SerializeField, Tooltip("Invoked when the control's value is changed.")]
        protected ControlEvent m_OnValueChanged = new ControlEvent();

        public InputControlLogic Logic
        {
            get
            {
                if(logic == null)
                {
                    switch(type)
                    {
                        case InputControlLogic.InputLogicType.Float:
                            logic = ScriptableObject.CreateInstance<FloatInputControlLogic>();
                            break;
                        case InputControlLogic.InputLogicType.Float2:
                            logic = ScriptableObject.CreateInstance<Float2InputControlLogic>();
                            break;
                        case InputControlLogic.InputLogicType.Float3:
                            logic = ScriptableObject.CreateInstance<Float3InputControlLogic>();
                            break;
                        case InputControlLogic.InputLogicType.Float4:
                            logic = ScriptableObject.CreateInstance<Float4InputControlLogic>();
                            break;
                        case InputControlLogic.InputLogicType.Int:
                            logic = ScriptableObject.CreateInstance<IntegerInputControlLogic>();
                            break;
                        case InputControlLogic.InputLogicType.Int2:
                            logic = ScriptableObject.CreateInstance<Integer2InputControlLogic>();
                            break;
                        case InputControlLogic.InputLogicType.Int3:
                            logic = ScriptableObject.CreateInstance<Integer3InputControlLogic>();
                            break;
                        case InputControlLogic.InputLogicType.Int4:
                            logic = ScriptableObject.CreateInstance<Integer4InputControlLogic>();
                            break;
                        case InputControlLogic.InputLogicType.String:
                            logic = ScriptableObject.CreateInstance<StringInputControlLogic>();
                            break;
                        case InputControlLogic.InputLogicType.Texture:
                            break;
                    }

                    if(logic == null) logic = ScriptableObject.CreateInstance<InvalidControlLogic>();
                }

                return logic;
            }
        }

        public ControlEvent onValueChanged
        {
            get { return m_OnValueChanged; }
            set { m_OnValueChanged = value; }
        }

        //public SubstanceNativeGraph CachedNativeGraph { get; protected set; }
        //public int InputIndex { get; protected set; }

        /// <summary>
        /// Get the bound input value on the native graph.
        /// </summary>
        /// <returns>object representing the current value on the cached native graph's target input.</returns>
        public virtual object GetInputValueRaw()
        {
            return Logic.GetInputValueRaw();
        }
        /// <summary>
        /// Set the bound input value on the native graph.
        /// </summary>
        public virtual void SetInputValue()
        {
            Logic.SetInputValueFromControl(this);
            Logic.SetInputValue();
            onValueChanged.Invoke();
        }

        public virtual void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            //CachedNativeGraph = nativeGraph;
            //InputIndex = input.Index;
            Logic.Initialize(nativeGraph, input);
            Logic.SetControlValues(this);

            label.text = input.Description.Label;
            identifierLabel.text = input.Description.Identifier;
        }


        public void Cleanup()
        {
            if(logic) logic.Release();
        }
    }
}
