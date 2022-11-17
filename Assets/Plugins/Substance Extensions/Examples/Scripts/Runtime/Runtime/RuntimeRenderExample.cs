using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Simple example script showing how to initialize, render, and release a runtime graph.
    /// </summary>
    public class RuntimeRenderExample : MonoBehaviour
    {
        [SerializeField]
        private SubstanceGraphSO substance = null;
        [SerializeField]
        private SubstanceParameterValue[] parameters = new SubstanceParameterValue[0];
        [Header("Outputs")]
        [SerializeField]
        private RectTransform outputParent = null;
        [SerializeField]
        private SubstanceOutputPreview previewPrefab = null;
        [SerializeField]
        private SubstanceOutput[] outputs = new SubstanceOutput[0];

        private void RenderSubstance()
        {
            //Get a SubstanceNativeGraph for the render operations and initialize the substance graph for runtime.
            SubstanceNativeGraph nativeGraph = substance.BeginRuntimeEditing();

            //Set input values on the native graph.
            nativeGraph.SetInputValues(parameters);

            //Render the native graph and update output textures on the substance graph.
            substance.Render(nativeGraph);

            //Dispose the native graph to free up resources.
            substance.EndRuntimeEditing(nativeGraph);

            //Generate preview outputs.
            for(int i=0; i < outputs.Length; i++)
            {
                SubstanceOutputPreview outputPreview = Instantiate(previewPrefab, outputParent);

                outputPreview.Initialize(outputs[i].Name, substance.GetOutputMap(outputs[i].Name));
            }
        }


        private void Start()
        {
            RenderSubstance();
        }
    }
}
