using UnityEngine;
using UnityEngine.UI;
using Adobe.Substance;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example showcasing how to interact with and listen for runtime engine events while supporting Adobe's SubstanceRuntimeGraph class.
    /// </summary>
    public class EngineInteractionsCompatibleExample : MonoBehaviour
    {
        [SerializeField, Tooltip("Substance to render after initializing the engine.")]
        private SubstanceRuntimeGraph substance = null;
        [SerializeField, Tooltip("Parameter for the target substance's hue value.")]
        private string hueParameter = "hue";
        [SerializeField, Tooltip("Parameter for the target substance's perlin noise disorder.")]
        private string disorderParameter = "disorder";
        [SerializeField, Tooltip("Parameter for the target substance's pattern scale.")]
        private string scaleParameter = "scale";
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


        private void OnInitializeClicked()
        {
            SubstanceEngineManager.Instance.InitializeEngine();

            RefreshButtons();
        }


        private void OnShutdownClicked()
        {
            SubstanceEngineManager.Instance.ShutdownEngine();

            RefreshButtons();
        }


        private void OnEngineInitialized()
        {
            Debug.Log("Engine Initialized!");

            //Render the target substance...
            if(substance != null)
            {
                substance.SetInputFloat(hueParameter, Random.Range(0f, 1f));
                substance.SetInputFloat(disorderParameter, Random.Range(0f, 1f));
                substance.SetInputFloat(scaleParameter, Random.Range(0.1f, 0.9f));

                substance.Render();
            }
        }


        private void OnEnginePreShutdown()
        {
            Debug.Log("Engine PRE Shutdown!");
        }


        private void OnEnginePostShutdown()
        {
            Debug.Log("Engine POST Shutdown!");
        }


        private void Start()
        {
            SubstanceEngineManager.Instance.onEngineInitialized.AddListener(OnEngineInitialized);
            SubstanceEngineManager.Instance.onEnginePreShutdown.AddListener(OnEnginePreShutdown);
            SubstanceEngineManager.Instance.onEnginePostShutdown.AddListener(OnEnginePostShutdown);

            initializeButton.onClick.AddListener(OnInitializeClicked);
            shutdownButton.onClick.AddListener(OnShutdownClicked);

            RefreshButtons();
        }
    }
}