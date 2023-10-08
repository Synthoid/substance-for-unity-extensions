using UnityEngine;
using UnityEngine.Profiling;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Tests
{
    public class SubstancePerformanceObject : MonoBehaviour
    {
        [SerializeField]
        private SubstanceGraphSO substance = null;
        [SerializeField]
        private SubstanceInputValueProfile profile = null;

        public void Render()
        {
            SubstanceNativeGraph nativeGraph = substance.GetCachedNativeGraph();

            //Set values
            Profiler.BeginSample("SOS - Set Substance Values");

            profile.SetValues(nativeGraph);

            Profiler.EndSample();

            //Render
            Profiler.BeginSample("Custom - Substance.Render()");

            substance.Render(nativeGraph);
            
            Profiler.EndSample();
        }
    }
}
