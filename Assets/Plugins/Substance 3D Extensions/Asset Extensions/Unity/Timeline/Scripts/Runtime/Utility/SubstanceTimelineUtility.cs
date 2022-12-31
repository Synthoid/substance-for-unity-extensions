using UnityEngine;
using System.Collections.Generic;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Timeline
{
    /// <summary>
    /// Runtime utility class for substance timeline functionality.
    /// </summary>
    public static class SubstanceTimelineUtility
    {
        private static Dictionary<string, SubstanceGraphSO> QueuedSubstances = new Dictionary<string, SubstanceGraphSO>();
        private static Dictionary<string, SbsRenderType> SubstanceRenderTypes = new Dictionary<string, SbsRenderType>();

        /// <summary>
        /// Clear the current dictionaries of queued substances and their corresponding render types.
        /// This should only be called when you wish to manually clear tracked substances. Most Timeline classes will automatically handle cleaning up their queued substances.
        /// </summary>
        public static void ClearQueuedSubstances()
        {
            QueuedSubstances.Clear();
            SubstanceRenderTypes.Clear();
        }

        /// <summary>
        /// Queue the given substance for rendering via Timeline logic.
        /// </summary>
        /// <param name="substance">Substance to queue for rendering.</param>
        /// <param name="renderType">Default render type for the substance if it has not already been queued.</param>
        /// <returns>True if the substance was queued, false if it was already queued.</returns>
        public static bool QueueSubstance(SubstanceGraphSO substance, SbsRenderType renderType=SbsRenderType.None)
        {
            if(substance == null) return false;

            bool success = QueuedSubstances.TryAdd(substance.GUID, substance);

            if(success)
            {
                //Add the substance to the dictionary of render types...
                SubstanceRenderTypes.Add(substance.GUID, renderType);
            }

            return success;
        }

        /// <summary>
        /// Dequeue the given substance from the rendering queue.
        /// </summary>
        /// <param name="substance">Substance to dequeue.</param>
        /// <returns>True if the substance was dequeued, or false if the substance was not queued already.</returns>
        public static bool DequeueSubstance(SubstanceGraphSO substance)
        {
            if(substance == null) return false;

            return DequeueSubstance(substance.GUID);
        }

        /// <summary>
        /// Dequeue the substance with the given GUID from the rendering queue.
        /// </summary>
        /// <param name="substanceGuid">GUID for the substance to dequeue.</param>
        /// <returns>True if the substance was dequeued, or false if the substance was not queued already.</returns>
        public static bool DequeueSubstance(string substanceGuid)
        {
            if(string.IsNullOrEmpty(substanceGuid)) return false;

            bool success = QueuedSubstances.Remove(substanceGuid);

            if(success)
            {
                //Remove the substance from the dictionary of render types...
                SubstanceRenderTypes.Remove(substanceGuid);
            }

            return success;
        }

        /// <summary>
        /// Get the <see cref="SbsRenderType"/> associated with the given substance. If the substance has not been queued, this will always return <see cref="SbsRenderType.None"/>.
        /// </summary>
        /// <param name="substance">Substance associated with the render type.</param>
        /// <returns>Render type associated with the given substance, or <see cref="SbsRenderType.None"/> if the substance has not been queued.</returns>
        public static SbsRenderType GetQueuedRenderType(SubstanceGraphSO substance)
        {
            if(substance == null) return SbsRenderType.None;

            return GetQueuedRenderType(substance.GUID);
        }

        /// <summary>
        /// Get the <see cref="SbsRenderType"/> associated with the given substance. If the substance has not been queued, this will always return <see cref="SbsRenderType.None"/>.
        /// </summary>
        /// <param name="substanceGuid">GUID for the substance associated with the render type.</param>
        /// <returns>Render type associated with the given substance, or <see cref="SbsRenderType.None"/> if the substance has not been queued.</returns>
        public static SbsRenderType GetQueuedRenderType(string substanceGuid)
        {
            if(string.IsNullOrEmpty(substanceGuid)) return SbsRenderType.None;

            bool success = SubstanceRenderTypes.TryGetValue(substanceGuid, out SbsRenderType renderType);

            if(!success) renderType = SbsRenderType.None;

            return renderType;
        }

        /// <summary>
        /// Set the render type value for the given substance. Note, this will do nothing if the susbtance has not been queued.
        /// </summary>
        /// <param name="substance">Substance associated with the render type being set.</param>
        /// <param name="renderType">New value for the substance's render type.</param>
        public static void SetQueuedRenderType(SubstanceGraphSO substance, SbsRenderType renderType)
        {
            if(substance == null) return;

            SetQueuedRenderType(substance.GUID, renderType);
        }

        /// <summary>
        /// Set the render type value for the given substance. Note, this will do nothing if the susbtance has not been queued.
        /// </summary>
        /// <param name="substanceGuid">GUID for the target substance.</param>
        /// <param name="renderType">New value for the substance's render type.</param>
        public static void SetQueuedRenderType(string substanceGuid, SbsRenderType renderType)
        {
            if(string.IsNullOrEmpty(substanceGuid)) return;

            if(SubstanceRenderTypes.ContainsKey(substanceGuid)) SubstanceRenderTypes[substanceGuid] = renderType;
        }
    }
}