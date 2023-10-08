using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random int4 value on a SubstanceNativeGraph input from a min and max range (exclusive).
    /// </summary>
    [System.Serializable]
    [AddMenu("Int4/Random/Range")]
    public class SubstanceRandomInt4Range : SubstanceInt4InputValue
    {
        [SerializeField, Tooltip("Minimum possible random values.")]
        private Vector4Int min = Vector4Int.zero;
        [SerializeField, Tooltip("Max possible random values (exclusive).")]
        private Vector4Int max = Vector4Int.one;

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

        public override Vector4Int GetValue()
        {
            return new Vector4Int(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z), Random.Range(min.w, max.w));
        }
    }
}