using UnityEngine;
using System.Collections.Generic;
using Adobe.Substance;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Timeline
{
    public static class SubstanceTimelineUtility
    {
        private static Dictionary<string, SubstanceGraphSO> QueuedSubstances = new Dictionary<string, SubstanceGraphSO>();
        private static Dictionary<string, SbsRenderType> SubstanceRenderTypes = new Dictionary<string, SbsRenderType>();

        public static bool QueueSubstance(SubstanceGraphSO substance, SbsRenderType renderType=SbsRenderType.None)
        {
            if(substance == null) return false;

            bool success = QueuedSubstances.TryAdd(substance.GUID, substance);

            if(success)
            {
                SubstanceRenderTypes.Add(substance.GUID, renderType);
            }

            return success;
        }


        public static bool DequeueSubstance(SubstanceGraphSO substance)
        {
            if(substance == null) return false;

            bool success = QueuedSubstances.Remove(substance.GUID);

            if(success)
            {
                SubstanceRenderTypes.Remove(substance.GUID);
            }

            return success;
        }


        public static SbsRenderType GetQueuedRenderType(SubstanceGraphSO substance)
        {
            if(substance == null) return SbsRenderType.None;

            bool success = SubstanceRenderTypes.TryGetValue(substance.GUID, out SbsRenderType renderType);

            if(!success) renderType = SbsRenderType.None;

            return renderType;
        }


        public static void SetQueuedRenderType(SubstanceGraphSO substance, SbsRenderType renderType)
        {
            if(substance == null) return;

            if(SubstanceRenderTypes.ContainsKey(substance.GUID)) SubstanceRenderTypes[substance.GUID] = renderType;
        }
    }
}