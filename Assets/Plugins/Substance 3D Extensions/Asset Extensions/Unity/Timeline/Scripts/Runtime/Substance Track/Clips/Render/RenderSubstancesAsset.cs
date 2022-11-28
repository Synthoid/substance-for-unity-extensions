using UnityEngine;
using UnityEngine.Playables;

namespace SOS.SubstanceExtensions.Timeline
{
#if UNITY_EDITOR
    [System.ComponentModel.DisplayName("Render Substance Clip")]
#endif
    [System.Serializable]
    public class RenderSubstancesAsset : PlayableAsset
    {
        [SerializeField, NoFoldout]
        private RenderSubstancesBehaviour template = new RenderSubstancesBehaviour();

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<RenderSubstancesBehaviour>.Create(graph, template);
        }
    }
}