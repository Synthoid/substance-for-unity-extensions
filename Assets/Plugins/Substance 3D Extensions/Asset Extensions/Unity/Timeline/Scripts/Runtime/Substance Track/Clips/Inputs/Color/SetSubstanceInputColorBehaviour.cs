using UnityEngine;
using UnityEngine.Playables;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class SetSubstanceInputColorBehaviour : SetSubstanceInputValueBehaviour
    {
        [SubstanceInputTypeFilter(SbsInputTypeFilter.Float4)]
        public SubstanceBindingParameter parameter = new SubstanceBindingParameter();
        public Color value = Color.white;

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