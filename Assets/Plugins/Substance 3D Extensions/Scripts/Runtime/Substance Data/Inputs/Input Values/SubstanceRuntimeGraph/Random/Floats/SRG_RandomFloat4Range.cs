using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random float4 value on a SubstanceRuntimeGraph input from a range.
    /// </summary>
    [System.Serializable]
    [AddMenu("Float4/Random/Range")]
    public class SRG_RandomFloat4Range : SRG_RandomFloat4
    {
        [SerializeField, Tooltip("Minimum possible random value.")]
        protected Vector4 min = Vector4.zero;
        [SerializeField, Tooltip("Maximum possible random value.")]
        protected Vector4 max = Vector4.one;

        public Vector4 Min
        {
            get { return min; }
            set { min = value; }
        }

        public Vector4 Max
        {
            get { return max; }
            set { max = value; }
        }

        public override Vector4 GetRandomValue()
        {
            return new Vector4(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z), Random.Range(min.w, max.w));
        }
    }
}