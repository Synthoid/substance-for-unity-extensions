using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public abstract class SetSubstanceInputValueAsset : PlayableAsset, ITimelineClipAsset
    {
        protected const string kClipNameFormat = "{0} - {1}";

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Blending; }
        }

        public abstract SubstanceBindingParameter TargetInputParameter { get; }

        public abstract string GetClipName();
    }
}