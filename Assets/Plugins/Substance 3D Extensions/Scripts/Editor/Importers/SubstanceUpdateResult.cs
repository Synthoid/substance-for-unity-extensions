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
    }
}