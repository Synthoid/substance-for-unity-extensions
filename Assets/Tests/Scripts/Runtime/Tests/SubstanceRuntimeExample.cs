using UnityEngine;
using Adobe.Substance;
using SOS.SubstanceExtensions;

public class SubstanceRuntimeExample : MonoBehaviour
{
    [Tooltip("Substance to render.")]
    public SubstanceGraphSO substance = null;
    [Tooltip("Float input to set the value of.")]
    public SubstanceParameter floatInput = new SubstanceParameter();
    [Tooltip("New value for the float input.")]
    public float value = 1f;
    [Tooltip("If true, the substance will render asynchronously.")]
    public bool useAsync = false;

    private void Render()
    {
        //Get the native graph for the target substance...
        SubstanceNativeGraph nativeGraph = substance.BeginRuntimeEditing();

        //Set the input value...
        nativeGraph.SetInputFloat(floatInput.Index, value);

        //Render the graph...
        substance.Render(nativeGraph);

        //Release the native graph...
        substance.EndRuntimeEditing(nativeGraph);
    }


    private async void RenderAsync()
    {
        //Get the native graph for the target substance...
        SubstanceNativeGraph nativeGraph = substance.BeginRuntimeEditing();

        //Set the input value...
        nativeGraph.SetInputFloat(floatInput.Index, value);

        //Render the graph...
        await substance.RenderAsync(nativeGraph);

        //Release the native graph...
        substance.EndRuntimeEditing(nativeGraph);
    }


    private void Start()
    {
        if(useAsync) RenderAsync();
        else Render();
    }
}