using UnityEngine;
using Adobe.Substance;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Tests
{
    [System.Serializable]
    public class SubstanceGraphRuntimeTestGroup
    {
        [TextArea(3, 5), Tooltip("[Optional] Notes shown during the test's visual confirmation.")]
        public string notes = "";
        [Tooltip("Runtime substance to use for the test.")]
        public SubstanceGraphSO substance = null;
        [Tooltip("Reference texture matching expected test output visuals.")]
        public Texture2D expectedVisual = null;
        [Tooltip("Output texture to pull from the substance after rendering.")]
        public SubstanceOutput resultOutput = new SubstanceOutput();
        [Tooltip("Default values applied to the substance before running the test.")]
        public SubstanceParameterValue[] defaultValues = new SubstanceParameterValue[0];
        [Tooltip("Values applied to the substance before rendering for the test.")]
        public SubstanceParameterValue[] renderValues = new SubstanceParameterValue[0];

        public void SetDefaultValues(SubstanceNativeGraph handler)
        {
            for(int i=0; i < defaultValues.Length; i++)
            {
                defaultValues[i].SetValue(handler);
            }
        }

        public void SetRenderValues(SubstanceNativeGraph handler)
        {
            for(int i = 0; i < renderValues.Length; i++)
            {
                renderValues[i].SetValue(handler);
            }
        }
    }
}