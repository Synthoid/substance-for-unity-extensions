using UnityEngine;
using Adobe.Substance;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Convenience struct for selecting substance output textures in the inspector.
    /// </summary>
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

        /// <summary>
        /// [Editor Only] GUID for the <see cref="SubstanceGraphSO"/> asset containing the target output. Primarily used for editor tooling, not used at runtime.
        /// </summary>
        public string GUID
        {
            get { return guid; }
        }

        /// <summary>
        /// Name for the target output.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Index for the graph associated with this output.
        /// </summary>
        public int GraphId
        {
            get { return graphId; }
        }

        /// <summary>
        /// Index for the target output.
        /// </summary>
        public int Index
        {
            get { return index; }
        }

#if UNITY_EDITOR
        /// <summary>
        /// [Editor Only] <see cref="SubstanceGraphSO"/> asset referenced for output values.
        /// </summary>
        public SubstanceGraphSO EditorAsset
        {
            get { return AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GUIDToAssetPath(guid)); }
        }
#endif
    }
}