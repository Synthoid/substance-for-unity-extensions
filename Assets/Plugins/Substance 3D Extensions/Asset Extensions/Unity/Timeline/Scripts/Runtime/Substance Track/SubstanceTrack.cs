using UnityEngine;
using UnityEngine.Timeline;
using Adobe.Substance;
using UnityEngine.Playables;

namespace SOS.SubstanceExtensions.Timeline
{
    /// <summary>
    /// Timeline track asset that manages setting substance native graph input values.
    /// </summary>
    [TrackColor(0.6f, 0.9f, 0.25f)]
    [TrackBindingType(typeof(SubstanceGraphSO))]
    [TrackClipType(typeof(SetSubstanceInputColorAsset))]
    [TrackClipType(typeof(SetSubstanceInputFloatAsset))]
    [TrackClipType(typeof(SetSubstanceInputFloat2Asset))]
    [TrackClipType(typeof(SetSubstanceInputFloat3Asset))]
    [TrackClipType(typeof(SetSubstanceInputFloat4Asset))]
    [TrackClipType(typeof(SetSubstanceInputIntAsset))]
    [TrackClipType(typeof(SetSubstanceInputInt2Asset))]
    [TrackClipType(typeof(SetSubstanceInputInt3Asset))]
    [TrackClipType(typeof(SetSubstanceInputInt4Asset))]
    [TrackClipType(typeof(SetSubstanceInputStringAsset))]
    [TrackClipType(typeof(SetSubstanceInputTextureAsset))]
    [TrackClipType(typeof(RenderSubstanceAsset))]
    public class SubstanceTrack : TrackAsset
    {
        private const string kDisplayNameFormat = "{0} - {1}";

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<SubstanceTrackMixerBehaviour>.Create(graph, inputCount);
        }


        protected override Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip)
        {
#if UNITY_EDITOR
            if(clip.asset is SetSubstanceInputValueAsset)
            {
                clip.displayName = ((SetSubstanceInputValueAsset)clip.asset).GetClipName();
            }
#endif

            return base.CreatePlayable(graph, gameObject, clip);
        }
    }
}