using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example class showing how to reference substance output textures.
    /// </summary>
    public class GetOutputsExample : MonoBehaviour
    {
        [SerializeField, Tooltip("Substance to reference output textures from.")]
        private SubstanceGraphSO substance;
        [SerializeField, Tooltip("Parent transform for spawned output previews.")]
        private RectTransform outputParent = null;
        [SerializeField, Tooltip("Prefab spawned to display an output texture.")]
        private SubstanceOutputPreview outputPreviewPrefab = null;
        [SerializeField, Tooltip("Outputs to visualize when playing.")]
        private SubstanceOutput[] outputs = new SubstanceOutput[0];

        private void SpawnOutputPreviews()
        {
            for(int i=0; i < outputs.Length; i++)
            {
                Texture2D texture = substance.GetOutputTexture(outputs[i].Name);

                SubstanceOutputPreview preview = Instantiate(outputPreviewPrefab, outputParent);

                preview.Initialize(outputs[i].Name, texture);
            }
        }


        private void Start()
        {
            SpawnOutputPreviews();
        }
    }
}