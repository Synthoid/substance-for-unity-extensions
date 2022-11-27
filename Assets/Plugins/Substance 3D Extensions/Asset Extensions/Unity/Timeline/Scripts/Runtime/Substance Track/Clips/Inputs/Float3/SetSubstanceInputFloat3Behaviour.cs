using UnityEngine;
using UnityEngine.Playables;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class SetSubstanceInputFloat3Behaviour : SetSubstanceInputValueBehaviour
    {
        [SubstanceInputTypeFilter(SbsInputTypeFilter.Float3), Tooltip("Input to adjust.")]
        public SubstanceBindingParameter parameter = new SubstanceBindingParameter();
        [Tooltip("Value for the target input.")]
        public Vector3 value = Vector3.zero;

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