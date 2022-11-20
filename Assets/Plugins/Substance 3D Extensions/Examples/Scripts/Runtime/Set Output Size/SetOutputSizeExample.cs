using UnityEngine;
using System;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    public class SetOutputSizeExample : MonoBehaviour
    {
        [SerializeField, Tooltip("")]
        private SubstanceGraphSO substance = null;
        [SerializeField, Tooltip("")]
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

            nativeGraph.SetInputValue(outputSize, substance);

            //IntPtr renderResult = await nativeGraph.RenderAsync();

            //substance.CreateAndUpdateOutputTextures(renderResult, nativeGraph, true);
            //MaterialUtils.AssignOutputTexturesToMaterial(substance);

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