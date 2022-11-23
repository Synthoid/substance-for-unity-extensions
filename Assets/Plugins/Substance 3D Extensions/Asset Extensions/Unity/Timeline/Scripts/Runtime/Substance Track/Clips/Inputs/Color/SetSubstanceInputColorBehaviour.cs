using UnityEngine;
using UnityEngine.Playables;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class SetSubstanceInputColorBehaviour : PlayableBehaviour
    {
        [SubstanceInputTypeFilter(SbsInputTypeFilter.Float4)]
        public SubstanceBindingParameter parameter = new SubstanceBindingParameter();
        public Color value = Color.white;
    }
}
