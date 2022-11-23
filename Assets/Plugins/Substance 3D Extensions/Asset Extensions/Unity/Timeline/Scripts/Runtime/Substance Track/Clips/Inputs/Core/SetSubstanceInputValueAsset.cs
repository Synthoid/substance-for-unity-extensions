using UnityEngine;
using UnityEngine.Playables;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public abstract class SetSubstanceInputValueAsset : PlayableAsset
    {
        protected const string kClipNameFormat = "{0} - {1}";

        public abstract SubstanceBindingParameter TargetInputParameter { get; }

        public abstract string GetClipName();
    }
}