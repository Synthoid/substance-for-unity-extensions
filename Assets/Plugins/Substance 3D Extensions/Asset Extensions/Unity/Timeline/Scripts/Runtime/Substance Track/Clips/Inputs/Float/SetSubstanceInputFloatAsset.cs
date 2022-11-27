using UnityEngine;
using UnityEngine.Playables;

namespace SOS.SubstanceExtensions.Timeline
{
#if UNITY_EDITOR
    [System.ComponentModel.DisplayName("Set Substance Input Float Clip")]
#endif
    [System.Serializable]
    public class SetSubstanceInputFloatAsset : SetSubstanceInputValueAsset<SetSubstanceInputFloatBehaviour>
    {

    }
}