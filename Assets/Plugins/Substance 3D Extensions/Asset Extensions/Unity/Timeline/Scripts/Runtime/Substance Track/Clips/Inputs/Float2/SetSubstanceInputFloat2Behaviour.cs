using UnityEngine;
using UnityEngine.Playables;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class SetSubstanceInputFloat2Behaviour : SetSubstanceInputValueBehaviour
    {
        [SubstanceInputTypeFilter(SbsInputTypeFilter.Float2), Tooltip("Input to adjust.")]
        public SubstanceBindingParameter parameter = new SubstanceBindingParameter();
        [Tooltip("Value for the target input.")]
        public Vector2 value = Vector2.zero;

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