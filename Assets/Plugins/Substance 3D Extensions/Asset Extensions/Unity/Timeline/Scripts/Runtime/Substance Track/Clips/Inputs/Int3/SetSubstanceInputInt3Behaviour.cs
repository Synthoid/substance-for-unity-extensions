using UnityEngine;
using UnityEngine.Playables;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class SetSubstanceInputInt3Behaviour : SetSubstanceInputValueBehaviour
    {
        [SubstanceInputTypeFilter(SbsInputTypeFilter.Int3), Tooltip("Input to adjust.")]
        public SubstanceBindingParameter parameter = new SubstanceBindingParameter();
        [Round, Tooltip("Value for the target input.")]
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