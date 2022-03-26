using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    public class SubstanceRuntimeTest : MonoBehaviour
    {
        [SerializeField]
        private KeyCode renderKey = KeyCode.Space;
        [SerializeField]
        private SubstanceMaterialInstanceSO substance = null;
        //[SerializeField]
        //private SubstanceParameterValue targetParameter = new SubstanceParameterValue();
        [SerializeField]
        private SubstanceParameterValue[] targetParameters = new SubstanceParameterValue[0];

        private void RenderSubstance()
        {
            //substance.SetInputAndRender(targetParameter);
            //substance.SetInputsAndRender(targetParameters);

            /*SubstanceNativeHandler handler = substance.BeginEditingSubstance();
            substance.SetInputs(targetParameters);
            substance.Render(handler);
            substance.EndEditingSubstance(handler);*/

            //TODO: Seems like directly setting parameters on the substance updates (semi) correctly.
            //But the below doesn't...
            SubstanceNativeHandler handler = substance.BeginEditingSubstance();
            substance.SetInputs(handler, targetParameters);
            substance.Render(handler);
            substance.EndEditingSubstance(handler);
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