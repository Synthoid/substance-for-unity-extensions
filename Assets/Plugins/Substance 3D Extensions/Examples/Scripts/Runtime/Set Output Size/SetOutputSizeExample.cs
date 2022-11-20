using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example script showing how to set an $outputsize input value.
    /// </summary>
    public class SetOutputSizeExample : MonoBehaviour
    {
        [SerializeField, Tooltip("Substance to render.")]
        private SubstanceGraphSO substance = null;
        [SerializeField, Tooltip("$outputsize parameter on the target substance.")]
        private SubstanceParameterValue outputSize = new SubstanceParameterValue();

        private async void RenderAsync()
        {
            float t = 0f;

            while(t < 1f)
            {
                t += Time.deltaTime;
                await System.Threading.Tasks.Task.Yield();
            }

            SubstanceNativeGraph nativeGraph = substance.BeginRuntimeEditing();

            nativeGraph.SetInputValue(outputSize);

            await substance.RenderAsync(nativeGraph);

            substance.EndRuntimeEditing(nativeGraph);
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
                outputSize.Int2Value = new Vector2Int((int)SbsOutputSize._32, (int)SbsOutputSize._32);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha6))
            {
                outputSize.Int2Value = new Vector2Int((int)SbsOutputSize._64, (int)SbsOutputSize._64);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha7))
            {
                outputSize.Int2Value = new Vector2Int((int)SbsOutputSize._128, (int)SbsOutputSize._128);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha8))
            {
                outputSize.Int2Value = new Vector2Int((int)SbsOutputSize._256, (int)SbsOutputSize._256);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha9))
            {
                outputSize.Int2Value = new Vector2Int((int)SbsOutputSize._512, (int)SbsOutputSize._512);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                outputSize.Int2Value = new Vector2Int((int)SbsOutputSize._1024, (int)SbsOutputSize._1024);
            }
        }
    }
}