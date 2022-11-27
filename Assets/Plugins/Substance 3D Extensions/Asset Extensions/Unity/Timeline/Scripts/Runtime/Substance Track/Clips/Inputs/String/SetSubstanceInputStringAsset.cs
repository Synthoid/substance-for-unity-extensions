using UnityEngine;
using UnityEngine.Playables;

namespace SOS.SubstanceExtensions.Timeline
{
#if UNITY_EDITOR
    [System.ComponentModel.DisplayName("Set Substance Input String Clip")]
#endif
    [System.Serializable]
    public class SetSubstanceInputStringAsset : SetSubstanceInputValueAsset<SetSubstanceInputStringBehaviour>
    {

    }
}