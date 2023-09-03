using UnityEngine;
using UnityEngine.UI;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example showcasing how to interact with and listen for runtime engine events via scripting.
    /// </summary>
    public class EngineInteractionsExample : MonoBehaviour
    {
        [SerializeField, Tooltip("Substance to render after initializing the engine.")]
        private SubstanceGraphSO substance = null;
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

            if(substance != null) substance.RenderAndForget();
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