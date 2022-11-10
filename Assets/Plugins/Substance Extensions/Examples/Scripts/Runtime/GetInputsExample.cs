using UnityEngine;
using UnityEngine.UI;
using System.Text;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example class showing how to reference substance input parameters and get their values.
    /// </summary>
    public class GetInputsExample : MonoBehaviour
    {
        [SerializeField, Tooltip("Label showing output text.")]
        private Text label = null;
        [SerializeField, Tooltip("Substance to pull values from.")]
        private SubstanceGraphSO substance = null;
        [SerializeField]
        private SubstanceParameter[] targetInputs = new SubstanceParameter[0];

        private string GetInputString(int index)
        {
            return string.Format("\n{0}: {1}", targetInputs[index].Name, substance.GetValue(index));
        }


        private void Start()
        {
            StringBuilder output = new StringBuilder("Substance Values:");

            for(int i=0; i < targetInputs.Length; i++)
            {
                output.Append(GetInputString(i));
            }

            label.text = output.ToString();
        }
    }
}