using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    public class RuntimeRenderTimeExample : MonoBehaviour
    {
        [SerializeField, Tooltip("Substance to render.")]
        private SubstanceGraphSO substance = null;
        [SerializeField, Tooltip("Prefab spawned to mark a render event.")]
        private RectTransform renderMarkerParent = null;
        [SerializeField, Tooltip("Parent for spawned render markers.")]
        private RectTransform renderMarkerPrefab = null;
        [SerializeField, Tooltip("Delay between renders (in seconds).")]
        private float renderDelay = 3f;

        private float cooldown = 0f;
        private SubstanceNativeGraph nativeGraph = null;

        private async void RenderGraph()
        {
            if(nativeGraph == null) nativeGraph = substance.BeginRuntimeEditing();

            await substance.RenderAsync();

            Instantiate(renderMarkerPrefab, renderMarkerParent);
        }


        private void OnDestroy()
        {
            if(nativeGraph != null) substance.EndRuntimeEditing(nativeGraph);
        }


        private void Update()
        {
            if(cooldown > 0f)
            {
                cooldown -= Time.deltaTime;
                return;
            }

            cooldown += renderDelay;
            RenderGraph();
        }
    }
}