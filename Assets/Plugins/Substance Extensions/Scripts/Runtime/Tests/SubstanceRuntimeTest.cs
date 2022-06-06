using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    public class SubstanceRuntimeTest : MonoBehaviour
    {
        [SerializeField]
        private KeyCode renderKey = KeyCode.Space;
        [SerializeField]
        private SubstanceFileSO substance = null;
        [SerializeField]
        private SubstanceParameterValue targetParameter = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test Tooltip")]
        private SubstanceParameterValue[] targetParameters = new SubstanceParameterValue[0];

        //private SubstanceNativeHandler cachedHandler = null;

        private void RenderSubstance()
        {
            //substance.SetInputAndRender(targetParameter);
            //substance.SetInputsAndRender(targetParameters);

            SubstanceNativeHandler handler = substance.Instances[0].BeginRuntimeEditing();
            substance.SetInputs(targetParameters);
            substance.Render(handler);
            substance.Instances[0].EndRuntimeEditing(handler);

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
            //if(cachedHandler != null) cachedHandler.Dispose();
        }


        private void Update()
        {
            if(Input.GetKeyDown(renderKey))
            {
                RenderSubstance();
            }
        }
    }
}