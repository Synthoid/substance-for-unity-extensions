using UnityEngine;
using UnityEngine.UI;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example showcasing how to interact with and listen for runtime engine events via the inspector.
    /// </summary>
    public class EngineInteractionsInspectorExample : MonoBehaviour
    {
        [SerializeField, Tooltip("Substance to render after initializing the engine.")]
        private SubstanceGraphSO substance = null;
        [SerializeField, Tooltip("Parameter for the target substance's hue value.")]
        private SubstanceParameter hueParameter = new SubstanceParameter();
        [SerializeField, Tooltip("Parameter for the target substance's perlin noise disorder.")]
        private SubstanceParameter disorderParameter = new SubstanceParameter();
        [SerializeField, Tooltip("Parameter for the target substance's pattern scale.")]
        private SubstanceParameter scaleParameter = new SubstanceParameter();
        [Header("Controls")]
        [SerializeField, Tooltip("Button clicked to initialize the substance engine.")]
        private Button initializeButton = null;
        [SerializeField, Tooltip("Button clicked to shutdown the substance engine.")]
        private Button shutdownButton = null;

        private void RefreshButtons()
        {
            if(SubstanceEngineManager.Instance.IsInitialized)
            {
                initializeButton.interactable = false;
                shutdownButton.interactable = true;
            }
            else
            {
                initializeButton.interactable = true;
                shutdownButton.interactable = false;
            }
        }


        public void OnInitializeClicked()
        {
            SubstanceEngineManager.Instance.InitializeEngine();

            RefreshButtons();
        }


        public void OnShutdownClicked()
        {
            SubstanceEngineManager.Instance.ShutdownEngine();

            RefreshButtons();
        }


        public void OnEngineInitialized()
        {
            Debug.Log("Engine Initialized!");

            //Render the target substance...
            if(substance != null)
            {
                SubstanceNativeGraph nativeGraph = substance.BeginRuntimeEditing();

                nativeGraph.SetInputFloat(hueParameter.Index, Random.Range(0f, 1f));
                nativeGraph.SetInputFloat(disorderParameter.Index, Random.Range(0f, 1f));
                nativeGraph.SetInputFloat(scaleParameter.Index, Random.Range(0.1f, 0.9f));

                substance.EndRuntimeEditing(nativeGraph);
            }
        }


        public void OnEnginePreShutdown()
        {
            Debug.Log("Engine PRE Shutdown!");

            //Clear cached substance native graphs. This is important as any native graphs not cleared will throw errors if used again.
            SubstanceGraphRuntimeExtensions.ClearCachedGraphs();
        }


        public void OnEnginePostShutdown()
        {
            Debug.Log("Engine POST Shutdown!");
        }


        private void Start()
        {
            RefreshButtons();
        }
    }
}