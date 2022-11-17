using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example script showing how to set input parameter values on runtime graphs.
    /// </summary>
    public class SetInputValuesExample : MonoBehaviour
    {
        [SerializeField, Tooltip("Substance to render.")]
        private SubstanceGraphSO substance = null;
        [SerializeField, Tooltip("Float parameter on the target substance to set.")]
        private SubstanceParameter floatInput = new SubstanceParameter();
        [SerializeField, Tooltip("Value for the float parameter.")]
        private float floatInputValue = 0f;
        [SerializeField, Tooltip("$randomseed input parameter on the target substance.")]
        private SubstanceParameterValue randomSeedValue = new SubstanceParameterValue();

        private void SetInputValuesAndRender()
        {
            //Initialize the graph for runtime generation.
            SubstanceNativeGraph nativeGraph = substance.BeginRuntimeEditing();

            //Set a float value using native plugin API. This requires the target input's index value and the new value.
            nativeGraph.SetInputFloat(floatInput.Index, floatInputValue);

            //Set a float value using custom wrapper code. This method automatically detects the target input type and sets the value accordningly.
            //This is shorthand for:
            //nativeGraph.SetInputFloat(randomSeedValue.Index, randomSeedValue.FloatValue);
            randomSeedValue.SetValue(nativeGraph);

            //Render the graph.
            substance.Render(nativeGraph);

            //Dispose resources to cleanup the native graph.
            substance.EndRuntimeEditing(nativeGraph);
        }


        private void Start()
        {
            SetInputValuesAndRender();
        }
    }
}