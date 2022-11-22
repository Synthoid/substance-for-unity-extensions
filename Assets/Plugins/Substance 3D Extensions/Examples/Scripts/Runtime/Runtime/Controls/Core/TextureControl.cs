using UnityEngine;
using UnityEngine.UI;

namespace SOS.SubstanceExtensions.Examples
{
    public class TextureControl : RuntimeParameterControl
    {
        [SerializeField, Tooltip("RawImage displaying the texture value.")]
        private RawImage preview = null;

        public virtual Texture2D Texture
        {
            get { return (Texture2D)preview.texture; }
            set { preview.texture = value; }
        }


        public virtual void SetInputValueWithoutNotify(Texture2D value)
        {
            preview.texture = value;
        }
    }
}