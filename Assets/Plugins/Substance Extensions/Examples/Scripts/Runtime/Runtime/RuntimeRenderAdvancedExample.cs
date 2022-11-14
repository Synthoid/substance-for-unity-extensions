using UnityEngine;
using UnityEngine.UI;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    public class RuntimeRenderAdvancedExample : MonoBehaviour
    {
        [SerializeField]
        private SubstanceGraphSO substance = null;
        [Header("Controls")]
        [SerializeField]
        private Toggle autoRenderToggle = null;
        [SerializeField]
        private Button renderButton = null;
        //TODO: Runtime UI controls for parameters.

        private SubstanceNativeGraph nativeGraph = null;

        private bool AutoRender
        {
            get { return autoRenderToggle.isOn; }
        }


        private async void RenderSubstanceAsync()
        {
            //Initialize substance...
            if(nativeGraph == null) nativeGraph = substance.BeginRuntimeEditing();

            //TODO: Set input values

            //Render substance...
            await substance.RenderAsync(nativeGraph);

            //TODO: Add label for showing starting render, setting inputs, rendering, complete.
        }


        private void OnAutoRenderToggled(bool value)
        {
            renderButton.interactable = !value;
        }


        private void OnRenderClicked()
        {
            RenderSubstanceAsync();
        }


        private void OnDestroy()
        {
            if(nativeGraph != null) nativeGraph.Dispose();
        }


        private void Start()
        {
            renderButton.interactable = !AutoRender;
        }
    }
}