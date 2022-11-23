using UnityEngine;
using UnityEngine.Timeline;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Timeline
{
    [TrackColor(0.1f, 0.8f, 0.2f)]
    [TrackClipType(typeof(SetSubstanceInputColorAsset))]
    [TrackBindingType(typeof(SubstanceGraphSO))]
    public class SubstanceTrack : TrackAsset
    {
        
    }
}