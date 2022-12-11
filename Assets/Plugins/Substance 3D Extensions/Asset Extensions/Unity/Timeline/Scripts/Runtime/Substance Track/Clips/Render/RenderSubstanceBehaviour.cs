using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using System.Collections.Generic;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class RenderSubstanceBehaviour : PlayableBehaviour
    {
        //TODO: Need a class that knows about all substances in a timeline and can wait for all of them to be ready then render them...
        [Tooltip("[True] - Render the track's bound substance every frame.\n[False] - Render the track's bound substance once at the start of this clip.")]
        public bool renderEveryFrame = true;

        private SubstanceGraphSO drivenGraph = null;
        private SubstanceNativeGraph drivenNativeGraph = null;
        private bool hasRendered = false;
        //private List<SubstanceTrackMixerBehaviour> mixerBehaviours = new List<SubstanceTrackMixerBehaviour>();

        public override void OnGraphStop(Playable playable)
        {
            if(drivenNativeGraph == null) return;

            SubstanceTimelineUtility.DequeueSubstance(drivenGraph);

            drivenGraph.EndRuntimeEditing(drivenNativeGraph);

            drivenGraph = null;
            drivenNativeGraph = null;
        }


        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            //TODO: Need to figure out when to dispose of the native graph... OnGraphStop?
            if(!Application.isPlaying) return;

            drivenGraph = (SubstanceGraphSO)playerData;

            InitializeDefaultValues(drivenGraph, playable);

            if(drivenNativeGraph == null) return;

            SbsRenderType renderType = SubstanceTimelineUtility.GetQueuedRenderType(drivenGraph);

            Debug.Log(renderType);

            if(renderType == SbsRenderType.Immediate)
            {
                if(renderEveryFrame || !hasRendered)
                {
                    hasRendered = true;

                    drivenGraph.Render(drivenNativeGraph);

                    Debug.Log("Render...");
                }
            }

            //mixerBehaviours.Clear();

            //TODO: This seems to get child playables (ie clips) but NOT the mixer behaviour...
            //int count = playable.GetGraph().GetBehaviours<SubstanceTrackMixerBehaviour>(mixerBehaviours);

            //Debug.Log(count);
            //if(!Application.isPlaying) return;
            //playable.GetGraph().get
            /*PlayableGraph graph = playable.GetGraph();
            int count = graph.GetOutputCount();

            //Debug.Log(count);

            //graph.GetOutputByType<SubstanceTrack>(0);

            for(int i=0; i < count; i++)
            {
                SubstanceTrack track = (SubstanceTrack)graph.GetOutput(i).GetReferenceObject();

                if(track == null) continue;

                //track.GetClips().First();
                //https://forum.unity.com/threads/get-all-behaviours-from-a-timeline.1275197/
                //TODO: Get all behaviours from a track and check that they are ready to render...

                //((SetSubstanceInputValueAsset)track.GetClips().First().asset).
                //Debug.Log(track.outputs.Count());
                //IEnumerator<PlayableBinding> outputs = track.outputs.GetEnumerator();

                //outputs.MoveNext();



                //track.timelineAsset.editorSettings.frameRate;
                //Debug.Log(graph.GetOutput(i).GetUserData().name); //Gets track binding
                //Debug.Log(graph.GetOutput(i).GetReferenceObject()); //Gets the track asset
                //Debug.Log(graph.GetOutput(i).GetReferenceObject()); //Gets the track asset
            }*/

            //base.ProcessFrame(playable, info, playerData);
        }

        private void InitializeDefaultValues(SubstanceGraphSO graph, Playable playable)
        {
            if(graph == null || drivenNativeGraph != null) return;

            //TODO: Move to a try add method...
            SubstanceTimelineUtility.QueueSubstance(graph, SbsRenderType.None);

            drivenNativeGraph = graph.BeginRuntimeEditing();
        }
    }
}