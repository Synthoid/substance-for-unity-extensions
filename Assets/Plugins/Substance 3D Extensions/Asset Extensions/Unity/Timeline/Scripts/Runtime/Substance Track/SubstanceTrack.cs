using UnityEngine;
using UnityEngine.Timeline;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Timeline
{
    [TrackColor(0.6f, 0.9f, 0.25f)]
    [TrackClipType(typeof(SetSubstanceInputColorAsset))]
    [TrackBindingType(typeof(SubstanceGraphSO))]
    public class SubstanceTrack : TrackAsset
    {
        
    }
}