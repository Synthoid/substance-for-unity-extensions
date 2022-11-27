using UnityEngine;
using UnityEngine.Playables;

namespace SOS.SubstanceExtensions.Timeline
{
#if UNITY_EDITOR
    [System.ComponentModel.DisplayName("Set Substance Input Int Clip")]
#endif
    [System.Serializable]
    public class SetSubstanceInputIntAsset : SetSubstanceInputValueAsset<SetSubstanceInputIntBehaviour>
    {

    }
}