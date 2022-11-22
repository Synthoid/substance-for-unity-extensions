using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    public class TextureInputControlLogic : InputControlLogic<Texture2D>
    {
        public override void Initialize(SubstanceNativeGraph nativeGraph, ISubstanceInput input)
        {
            base.Initialize(nativeGraph, input);

            SubstanceInputTexture valueInput = (SubstanceInputTexture)input;

            value = valueInput.GetTexture();
        }


        public override Texture2D GetInputValue()
        {
            return value; //Hack since SubstanceNativeGraph doesn't expose texture values...
        }


        public override void SetInputValue()
        {
            _ = CachedNativeGraph.SetInputTextureGPUAsync(InputIndex, Value);
        }


        public override void SetInputValueFromControl(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case TextureControl control:
                    Value = control.Texture;
                    break;
                default:
                    Debug.LogWarning(string.Format(kControlTypeInvalidFormatString, inputControl.GetType().FullName, this.GetType().FullName));
                    break;
            }
        }


        public override void SetControlValues(RuntimeParameterControl inputControl)
        {
            switch (inputControl)
            {
                case TextureControl control:
                    control.SetInputValueWithoutNotify(Value);
                    break;
            }
        }
    }
}