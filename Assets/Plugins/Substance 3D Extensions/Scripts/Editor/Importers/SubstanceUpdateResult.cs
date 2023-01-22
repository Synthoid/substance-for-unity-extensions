using UnityEngine;
using System.Collections.Generic;
using Adobe.Substance;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Results of an update operation.
    /// </summary>
    public struct SubstanceUpdateResult
    {
        /// <summary>
        /// Updated graph assets.
        /// </summary>
        public List<SubstanceGraphSO> updatedGraphs;
        /// <summary>
        /// Updated graphs that added output textures.
        /// </summary>
        public List<SubstanceGraphSO> newOutputGraphs;


        public bool TryAddUpdatedGraph(SubstanceGraphSO graph)
        {
            if(updatedGraphs == null) newOutputGraphs = new List<SubstanceGraphSO>();
            if(updatedGraphs.Contains(graph)) return false;

            updatedGraphs.Add(graph);

            return true;
        }


        public bool TryAddNewOutputGraph(SubstanceGraphSO graph)
        {
            if(newOutputGraphs == null) newOutputGraphs = new List<SubstanceGraphSO>();
            if(newOutputGraphs.Contains(graph)) return false;

            newOutputGraphs.Add(graph);

            return true;
        }
    }
}