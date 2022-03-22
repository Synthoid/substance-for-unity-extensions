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
            substance.SetInputsAndRender(targetParameters);
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