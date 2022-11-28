using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class RenderSubstancesBehaviour : PlayableBehaviour
    {
        //TODO: Need a class that knows about all substances in a timeline and can wait for all of them to be ready then render them...
        [Tooltip("[True] - Render the track's bound substance every frame.\n[False] - Render the track's bound substance once at the start of this clip.")]
        public bool renderEveryFrame = true;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            //if(!Application.isPlaying) return;
            //playable.GetGraph().get
            PlayableGraph graph = playable.GetGraph();
            int count = graph.GetOutputCount();

            //Debug.Log(count);
            Debug.Log(graph.GetOutputCount());

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
                Debug.Log(graph.GetOutput(i).GetReferenceObject()); //Gets the track asset
            }

            //base.ProcessFrame(playable, info, playerData);
        }
    }
}