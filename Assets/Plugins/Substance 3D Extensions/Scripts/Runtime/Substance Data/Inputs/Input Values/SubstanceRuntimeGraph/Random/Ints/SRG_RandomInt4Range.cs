using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random int4 value on a SubstanceRuntimeGraph input from a range.
    /// </summary>
    [System.Serializable]
    [AddMenu("Int4/Random/Range")]
    public class SRG_RandomInt4Range : SRG_RandomInt4
    {
        [SerializeField, Tooltip("Minimum possible random value.")]
        protected Vector4Int min = Vector4Int.zero;
        [SerializeField, Tooltip("Maximum possible random value.")]
        protected Vector4Int max = Vector4Int.one;

        public Vector4Int Min
        {
            get { return min; }
            set { min = value; }
        }

        public Vector4Int Max
        {
            get { return max; }
            set { max = value; }
        }

        public override Vector4Int GetRandomValue()
        {
            return new Vector4Int(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z), Random.Range(min.w, max.w));
        }
    }
}