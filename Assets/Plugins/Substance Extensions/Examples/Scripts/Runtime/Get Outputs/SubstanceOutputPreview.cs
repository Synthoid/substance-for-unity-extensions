using UnityEngine;
using UnityEngine.UI;

namespace SOS.SubstanceExtensions.Examples
{
    public class SubstanceOutputPreview : MonoBehaviour
    {
        [SerializeField, Tooltip("Image showing the output texture.")]
        private RawImage previewImage = null;
        [SerializeField, Tooltip("Label showing the target output's identifier.")]
        private Text outputIdentifierLabel = null;
        [SerializeField, Tooltip("Label showing the target output texture asset's name.")]
        private Text outputNameLabel = null;

        public void Initialize(string identifier, Texture2D texture)
        {
            previewImage.texture = texture;

            previewImage.enabled = texture != null;

            outputIdentifierLabel.text = identifier;
            outputNameLabel.text = texture != null ? texture.name : "<NULL>";
        }
    }
}