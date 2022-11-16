using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Adobe.Substance;
using Adobe.Substance.Input;

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
        [SerializeField]
        private RectTransform controlsParent = null;
        [SerializeField, Tooltip("Asset containing data for spawnable controls.")]
        private RuntimeParameterControlsAsset controlsData = null;

        private SubstanceNativeGraph nativeGraph = null;
        private List<RuntimeParameterControl> controls = new List<RuntimeParameterControl>();

        private bool AutoRender
        {
            get { return autoRenderToggle.isOn; }
        }


        private async void RenderSubstanceAsync()
        {
            //Initialize substance if needed...
            if(nativeGraph == null) nativeGraph = substance.BeginRuntimeEditing();

            //Render substance...
            await substance.RenderAsync(nativeGraph);
        }


        private void SetupInputControls()
        {
            if(substance == null || controlsData == null) return;
            if(nativeGraph == null) nativeGraph = substance.BeginRuntimeEditing();

            List<ISubstanceInput> inputs = substance.Input;

            for(int i=0; i < inputs.Count; i++)
            {
                RuntimeParameterControl controlPrefab = controlsData.GetControlPrefab(inputs[i]);
                RuntimeParameterControl control = Instantiate<RuntimeParameterControl>(controlPrefab, controlsParent);

                control.Initialize(nativeGraph, inputs[i]);
                control.onValueChanged.AddListener(OnControlValueChanged);

                controls.Add(control);
            }
        }


        private void OnControlValueChanged()
        {
            if(AutoRender) OnRenderClicked();
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
            autoRenderToggle.onValueChanged.AddListener(OnAutoRenderToggled);
            renderButton.onClick.AddListener(OnRenderClicked);

            renderButton.interactable = !AutoRender;

            SetupInputControls();
        }
    }
}