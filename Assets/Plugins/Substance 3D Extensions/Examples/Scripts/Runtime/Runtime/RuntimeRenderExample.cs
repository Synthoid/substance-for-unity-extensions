using UnityEngine;
using System.Collections;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Simple example script showing how to initialize, render, and release a runtime graph.
    /// </summary>
    public class RuntimeRenderExample : MonoBehaviour
    {
        [SerializeField, RuntimeGraphOnly, Tooltip("Substance to render.")]
        private SubstanceGraphSO substance = null;
        [SerializeField, Tooltip("If true, rendering will be done asynchronously on a separate thread. This typically prevents frame drops from rendering a substance.")]
        private bool renderAsync = false;
        [SerializeField, Tooltip("Delay (in seconds) before the render occurs.")]
        private float renderDelay = 3f;
        [SerializeField, Tooltip("Input values to set on the target substance.")]
        private SubstanceParameterValue[] parameters = new SubstanceParameterValue[0];
        [Header("Outputs")]
        [SerializeField, Tooltip("Parent transform for spawned output previews.")]
        private RectTransform outputParent = null;
        [SerializeField, Tooltip("Prefab for generated output textures.")]
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

            GenerateOutputs();
        }


        private async void RenderSubstanceAsync()
        {
            //Get a SubstanceNativeGraph for the render operations and initialize the substance graph for runtime.
            SubstanceNativeGraph nativeGraph = substance.BeginRuntimeEditing();

            //Set input values on the native graph.
            await nativeGraph.SetInputValuesAsync(parameters);

            //Render the native graph asynchronously and update output textures on the substance graph.
            await substance.RenderAsync(nativeGraph);

            //Dispose the native graph to free up resources.
            substance.EndRuntimeEditing(nativeGraph);

            GenerateOutputs();
        }


        private void GenerateOutputs()
        {
            //Generate preview outputs.
            for (int i = 0; i < outputs.Length; i++)
            {
                SubstanceOutputPreview outputPreview = Instantiate(previewPrefab, outputParent);

                outputPreview.Initialize(outputs[i].Name, substance.GetOutputTexture(outputs[i].Name));
            }
        }


        private IEnumerator Start()
        {
            float t = 0f;

            //Delay before rendering. There seems to be some weirdness around rendering immediately on start...
            //Notably async rendering does NOT have this issue.
            while(t < renderDelay)
            {
                t += Time.deltaTime;
                yield return null;
            }

            if (renderAsync)
            {
                RenderSubstanceAsync();
            }
            else
            {
                RenderSubstance();
            }
        }
    }
}
