using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random float3 value on a SubstanceRuntimeGraph input from a range.
    /// </summary>
    [System.Serializable]
    [AddMenu("Float3/Random/Range")]
    public class SRG_RandomFloat3Range : SRG_RandomFloat3
    {
        [SerializeField, Tooltip("Minimum possible random value.")]
        protected Vector3 min = Vector3.zero;
        [SerializeField, Tooltip("Maximum possible random value.")]
        protected Vector3 max = Vector3.one;

        public Vector3 Min
        {
            get { return min; }
            set { min = value; }
        }

        public Vector3 Max
        {
            get { return max; }
            set { max = value; }
        }

        public override Vector3 GetRandomValue()
        {
            return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
        }
    }
}