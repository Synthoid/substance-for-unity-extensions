using UnityEngine;
using UnityEngine.Playables;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class RenderSubstanceBehaviour : PlayableBehaviour
    {
        [SerializeField, Tooltip("[True] - Render the track's bound substance every frame.\n[False] - Render the track's bound substance once at the start of this clip.")]
        private bool renderEveryFrame = true;
        [SerializeField, Tooltip("If true, the native handle for the track's bound substance will be released when the graph stops playing.\n\nSet this to false if you want to continue to edit and render the substance after the timeline has finished playing.")]
        private bool releaseSubstanceOnStop = true;
        //Render async option?

        private SubstanceGraphSO drivenGraph = null;
        private SubstanceNativeGraph drivenNativeGraph = null;
        private bool hasRendered = false;

        public override void OnGraphStop(Playable playable)
        {
            if(drivenNativeGraph == null) return;

            SubstanceTimelineUtility.DequeueSubstance(drivenGraph);

            if(releaseSubstanceOnStop) drivenGraph.EndRuntimeEditing(drivenNativeGraph);

            drivenGraph = null;
            drivenNativeGraph = null;
        }


        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if(!Application.isPlaying) return;

            drivenGraph = (SubstanceGraphSO)playerData;

            InitializeDefaultValues(drivenGraph, playable);

            if(drivenNativeGraph == null) return;

            SbsRenderType renderType = SubstanceTimelineUtility.GetQueuedRenderType(drivenGraph);

            if(renderType == SbsRenderType.Immediate)
            {
                if(renderEveryFrame || !hasRendered)
                {
                    hasRendered = true;

                    drivenGraph.Render(drivenNativeGraph);
                }
            }
        }

        private void InitializeDefaultValues(SubstanceGraphSO graph, Playable playable)
        {
            if(graph == null || drivenNativeGraph != null) return;

            SubstanceTimelineUtility.QueueSubstance(graph, SbsRenderType.None);

            drivenNativeGraph = graph.BeginRuntimeEditing();
        }
    }
}