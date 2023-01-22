using UnityEngine;
using System.Collections.Generic;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Extension methods for substance output texture classes.
    /// </summary>
    public static class SubstanceOutputTextureExtensions
    {
        /// <summary>
        /// Updates the destination list of substance output textures with valid textures from the source list.
        /// </summary>
        /// <param name="source">List containing output textures to copy values from.</param>
        /// <param name="destination">List containing outputs to paste values into.</param>
        /// <returns>Number of outputs from the source list that were updated on the destination list.</returns>
        public static int UpdateOutputList(List<SubstanceOutputTexture> source, List<SubstanceOutputTexture> destination)
        {
            int count = 0;

            for(int i=0; i < source.Count; i++)
            {
                int index = i;
                int targetIndex = destination.IndexOf((output) => { return output.Description.Identifier == source[index].Description.Identifier; });

                //Skip if no output with matching identifier exists
                if(targetIndex < 0)
                {
                    Debug.LogWarning(string.Format("[SOS - Substance Extensions] No output with identifier [{0}] in destination outputs.", source[index].Description.Identifier));
                    continue;
                }

                destination[targetIndex].OutputTexture = source[index].OutputTexture;

                count++;
            }

            return count;
        }


        public static bool OutputIdentifiersMatch(List<SubstanceOutputTexture> a, List<SubstanceOutputTexture> b)
        {
            for(int i=0; i < a.Count; i++)
            {
                if(!b.Contains((output) => { return output.Description.Identifier == a[i].Description.Identifier; }))
                {
                    return false;
                }
            }

            for(int i=0; i < b.Count; i++)
            {
                if(!b.Contains((output) => { return output.Description.Identifier == b[i].Description.Identifier; }))
                {
                    return false;
                }
            }

            return true;
        }
    }
}