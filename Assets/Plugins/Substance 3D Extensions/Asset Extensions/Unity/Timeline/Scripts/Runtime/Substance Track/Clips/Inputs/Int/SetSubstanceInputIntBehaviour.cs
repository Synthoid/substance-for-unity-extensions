using UnityEngine;
using UnityEngine.Playables;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class SetSubstanceInputIntBehaviour : SetSubstanceInputValueBehaviour
    {
        [SubstanceInputTypeFilter(SbsInputTypeFilter.Int), Tooltip("Input to adjust.")]
        public SubstanceBindingParameter parameter = new SubstanceBindingParameter();
        [Tooltip("Value for the target input.")]
        public int value = 0;

        public override ISubstanceInputParameter Parameter
        {
            get { return parameter; }
        }


        public override object ValueRaw
        {
            get { return value; }
        }
    }
}