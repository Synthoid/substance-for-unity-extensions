using UnityEngine;
using UnityEngine.Playables;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class SetSubstanceInputInt2Behaviour : SetSubstanceInputValueBehaviour
    {
        [SubstanceInputTypeFilter(SbsInputTypeFilter.Int2), Tooltip("Input to adjust.")]
        public SubstanceBindingParameter parameter = new SubstanceBindingParameter();
        [Round, Tooltip("Value for the target input.")]
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