using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions.Timeline
{
    /// <summary>
    /// Extension methods for <see cref="Playable"/> related classes.
    /// </summary>
    public static class SubstancePlayableExtensions
    {
        public static int GetBehaviours<T>(this Playable playable, List<T> behaviours) where T : class, IPlayableBehaviour, new()
        {
            if(!playable.IsValid())
            {
                return 0;
            }

            int count = playable.GetInputCount();
            int behaviourCount = 0;
            System.Type behaviourType = typeof(T);

            for(int i=0; i < count; i++)
            {
                Playable input = playable.GetInput(i);

                Debug.Log($"{input.GetPlayableType() == behaviourType}: {input.GetType().FullName} | {input.GetPlayableType().FullName}");

                if(input.GetPlayableType() == behaviourType)
                {
                    Debug.Log("Playable is target type!");

                    T behaviour = ((ScriptPlayable<T>)input).GetBehaviour();

                    behaviourCount++;

                    behaviours.Add(behaviour);
                }
                else if(input.GetInputCount() > 0)
                {
                    Debug.Log("Nested playable...");

                    behaviourCount += GetBehaviours(input, behaviours);
                }
            }

            return behaviourCount;
        }
    }
}