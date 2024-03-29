using UnityEngine;
using UnityEngine.Playables;

namespace SOS.SubstanceExtensions.Timeline
{
    /// <summary>
    /// Timeline clip for rendering a track's bound substance graph.
    /// </summary>
#if UNITY_EDITOR
    [System.ComponentModel.DisplayName("Render Substance Clip")]
#endif
    [System.Serializable]
    public class RenderSubstanceAsset : PlayableAsset
    {
        [SerializeField, NoFoldout]
        private RenderSubstanceBehaviour template = new RenderSubstanceBehaviour();

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<RenderSubstanceBehaviour>.Create(graph, template);
        }
    }
}