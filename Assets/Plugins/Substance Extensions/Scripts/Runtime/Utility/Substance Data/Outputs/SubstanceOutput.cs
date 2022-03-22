using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    [System.Serializable]
    public struct SubstanceOutput
    {
        /// <summary>
        /// GUID for the <see cref="SubstanceMaterialInstanceSO"/> asset containing the target output.
        /// </summary>
        [SerializeField]
        private string guid;
        /// <summary>
        /// Name for the target output.
        /// </summary>
        [SerializeField]
        private string name;
        /// <summary>
        /// Index for the graph associated with this output.
        /// </summary>
        [SerializeField]
        private int graphId;
        /// <summary>
        /// Index for the target output.
        /// </summary>
        [SerializeField]
        private int index;

        public string GUID
        {
            get { return guid; }
        }

        public string Name
        {
            get { return name; }
        }

        public int GraphId
        {
            get { return graphId; }
        }

        public int Index
        {
            get { return index; }
        }
    }
}