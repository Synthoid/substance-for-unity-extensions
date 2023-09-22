using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random int3 value on a SubstanceRuntimeGraph input from a range.
    /// </summary>
    [System.Serializable]
    [AddMenu("Int3/Random/Range")]
    public class SRG_RandomInt3Range : SRG_RandomInt3
    {
        [SerializeField, Tooltip("Minimum possible random value.")]
        protected Vector3Int min = Vector3Int.zero;
        [SerializeField, Tooltip("Maximum possible random value.")]
        protected Vector3Int max = Vector3Int.one;

        public Vector3Int Min
        {
            get { return min; }
            set { min = value; }
        }

        public Vector3Int Max
        {
            get { return max; }
            set { max = value; }
        }

        public override Vector3Int GetRandomValue()
        {
            return new Vector3Int(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
        }
    }
}