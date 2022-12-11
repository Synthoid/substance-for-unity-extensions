using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions.Timeline
{
    /// <summary>
    /// Extension methods for <see cref="PlayableGraph"/>.
    /// </summary>
    public static class SubstancePlayableGraphExtensions
    {
        public static int GetTracks(this PlayableGraph graph, List<TrackAsset> tracks)
        {
            return GetTracks<TrackAsset>(graph, tracks);
        }


        public static int GetTracks<T>(this PlayableGraph graph, List<T> tracks) where T : TrackAsset
        {
            int count = graph.GetOutputCount();
            int trackCount = 0;

            for (int i = 0; i < count; i++)
            {
                PlayableOutput output = graph.GetOutput(i);

                if (!output.IsOutputValid() || !output.IsPlayableOutputOfType<ScriptPlayableOutput>()) continue;

                trackCount++;

                tracks.Add((T)output.GetReferenceObject());
            }

            return trackCount;
        }


        /*public static int GetBehaviours(this PlayableGraph graph, List<IPlayableBehaviour> behaviours)
        {
            return GetBehaviours<PlayableBehaviour>(graph, behaviours);
        }*/


        public static int GetBehaviours<T>(this PlayableGraph graph, List<T> behaviours) where T : class, IPlayableBehaviour, new()
        {
            int count = graph.GetOutputCount();
            int behaviourCount = 0;

            for(int i=0; i < count; i++)
            {
                PlayableOutput output = graph.GetOutput(i);

                if (!output.IsOutputValid() || !output.IsPlayableOutputOfType<ScriptPlayableOutput>()) continue;

                Debug.Log($"Output {i} valid!");

                int sourcePort = output.GetSourceOutputPort();
                Playable mixerPlayable = output.GetSourcePlayable().GetInput(sourcePort);

                Debug.Log($"Playable {sourcePort} is {(mixerPlayable.IsValid() ? "Valid" : "Invalid")}");

                behaviourCount += mixerPlayable.GetBehaviours<T>(behaviours);
            }

            return behaviourCount;
        }
    }
}