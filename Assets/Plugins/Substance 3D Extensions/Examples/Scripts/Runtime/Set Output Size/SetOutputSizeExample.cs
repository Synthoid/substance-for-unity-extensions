using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example script showing how to set an $outputsize input value.
    /// </summary>
    public class SetOutputSizeExample : MonoBehaviour
    {
        public enum OutputSizeMethod
        {
            Parameter   = 0,
            Enum        = 1,
            Vector2Int  = 2
        }

        [SerializeField, Tooltip("Substance to render.")]
        private SubstanceGraphSO substance = null;
        [SerializeField, Tooltip("How to set the $outputsize input on the target substance.\n\n[Parameter] - Use the SubstanceNativeGraphExtensions.SetInputValue() extension method.\n[Enum] - Use the SubstanceNativeGraphExtensions.SetOutputSize(int, SbsOutputSize) extension method.\n[Vector2Int] - Use the base plugin SetInputInt2() method.")]
        private OutputSizeMethod setMethod = OutputSizeMethod.Parameter;
        [SerializeField, Tooltip("$outputsize parameter on the target substance.")]
        private SubstanceParameterValue outputSizeParameter = new SubstanceParameterValue();
        [SerializeField, Tooltip("Convenience enum value for the $outputsize input. These enum values are synced up to the Substance engine's expected values for specific resolutions.")]
        private SbsOutputSize outputSize = SbsOutputSize._32;
        [SerializeField, Tooltip("Raw Vector2Int value for the $outputsize input. These values are not the resolution in pixels, but the nuber of bit shifts for the resolution. ie:\n32x32 would be (5, 5)\n64x64 would be (6, 6)\n1024x1024 would be (10, 10)\netc")]
        private Vector2Int rawOutputSize = new Vector2Int(5, 5);

        private async void RenderAsync()
        {
            float t = 0f;

            while(t < 1f)
            {
                t += Time.deltaTime;
                await System.Threading.Tasks.Task.Yield();
            }

            SubstanceNativeGraph nativeGraph = substance.BeginRuntimeEditing();

            switch(setMethod)
            {
                case OutputSizeMethod.Parameter:
                    //Set the outputsize with the generic SetInputValue extension method.
                    nativeGraph.SetInputValue(outputSizeParameter);
                    break;
                case OutputSizeMethod.Enum:
                    //Set the outputsize with the specific SetOutputSize extension method. Note that the outputsize input ID is usually 0.
                    nativeGraph.SetOutputSize(0, outputSize);
                    break;
                case OutputSizeMethod.Vector2Int:
                    //Set the outputsize with the builtin plugin SetInputInt2 method.
                    nativeGraph.SetInputInt2(0, rawOutputSize);
                    break;
            }

            await substance.RenderAsync(nativeGraph);

            substance.EndRuntimeEditing(nativeGraph);
        }


        private void SetOutputSize(SbsOutputSize newSize)
        {
            rawOutputSize = new Vector2Int((int)newSize, (int)newSize);
            outputSizeParameter.Int2Value = rawOutputSize;
            outputSize = newSize;
        }


        private void Start()
        {
            RenderAsync();
        }


        private void Update()
        {
            if(!Input.anyKeyDown) return;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                RenderAsync();
            }
            else if(Input.GetKeyDown(KeyCode.Alpha5))
            {
                SetOutputSize(SbsOutputSize._32);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha6))
            {
                SetOutputSize(SbsOutputSize._64);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha7))
            {
                SetOutputSize(SbsOutputSize._128);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha8))
            {
                SetOutputSize(SbsOutputSize._256);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha9))
            {
                SetOutputSize(SbsOutputSize._512);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                SetOutputSize(SbsOutputSize._1024);
            }
        }
    }
}