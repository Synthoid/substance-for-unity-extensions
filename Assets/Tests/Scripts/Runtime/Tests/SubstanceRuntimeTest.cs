using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    public class SubstanceRuntimeTest : MonoBehaviour
    {
        [SerializeField]
        private KeyCode renderKey = KeyCode.Space;
        [SerializeField]
        private bool cacheRenderHandle = true;
        [SerializeField]
        private SubstanceGraphSO substance = null;
        [SerializeField, Tooltip("Test Tooltip")]
        private SubstanceParameterValue[] targetParameters = new SubstanceParameterValue[0];

        private SubstanceNativeGraph cachedHandler = null;

        private async void RenderSubstanceAsync()
        {
            if(cachedHandler == null) cachedHandler = substance.BeginRuntimeEditing();

            await cachedHandler.SetInputValuesAsync(targetParameters);

            await substance.RenderAsync(cachedHandler);
        }


        private async void RenderSubstance()
        {
            //Debug.Log("Start render...");

            //Debug.Log("Begin runtime editing...");

            SubstanceNativeGraph handler = substance.BeginRuntimeEditing();

            //Debug.Log("Set input values...");

            await handler.SetInputValuesAsync(targetParameters);

            //Debug.Log("Render async...");

            await substance.RenderAsync(handler);

            //Debug.Log("Finish rendering...");

            substance.EndRuntimeEditing(handler);

            //Debug.Log("Complete!");

            //TODO: Seems like directly setting parameters on the substance updates (semi) correctly.
            //But the below doesn't...
            /*SubstanceNativeHandler handler = substance.BeginRuntimeEditing();
            substance.SetInputs(handler, targetParameters);
            substance.Render(handler);
            substance.EndRuntimeEditing(handler);*/

            //TODO: This causes a crash!
            /*if(cachedHandler == null) cachedHandler = substance.BeginRuntimeEditing();

            substance.SetInputsAndRender(targetParameters, cachedHandler);*/
        }


        private void OnDestroy()
        {
            if(cachedHandler != null) cachedHandler.Dispose();
        }


        private void Update()
        {
            if(Input.GetKeyDown(renderKey))
            {
                if(cacheRenderHandle)
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
}