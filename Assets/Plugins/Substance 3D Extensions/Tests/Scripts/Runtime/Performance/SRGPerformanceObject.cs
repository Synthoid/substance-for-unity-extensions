using UnityEngine;
using UnityEngine.Profiling;
using System.Threading.Tasks;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions.Tests
{
    public class SRGPerformanceObject : MonoBehaviour
    {
        [SerializeField]
        private SubstanceRuntimeGraph runtimeGraph = null;
        [SerializeField]
        private SRG_InputValueProfile profile = null;

        public void Render()
        {
            //Set values
            Profiler.BeginSample("SOS - Set SRG Values");

            profile.SetValues(runtimeGraph);

            Profiler.EndSample();

            //Render
            Profiler.BeginSample("Adobe - SubstanceRuntimeGraph.Render()");

            runtimeGraph.Render();
            
            Profiler.EndSample();
        }


        public async Task RenderAsync()
        {
            profile.SetValues(runtimeGraph);

            //Profiler.BeginSample("Adobe - SubstanceRuntimeGraph.RenderAsync()");

            await runtimeGraph.RenderAsync();

            //Profiler.EndSample();
        }
    }
}