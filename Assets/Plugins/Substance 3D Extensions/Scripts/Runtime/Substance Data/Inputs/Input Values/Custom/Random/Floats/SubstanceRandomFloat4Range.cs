using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random float4 value on a SubstanceNativeGraph input from a min and max range (inclusive).
    /// </summary>
    [System.Serializable]
    [AddMenu("Float4/Random/Range")]
    public class SubstanceRandomFloat4Range : SubstanceFloat4InputValue
    {
        [SerializeField, Tooltip("Minimum possible random values.")]
        private Vector4 min = Vector4.zero;
        [SerializeField, Tooltip("Max possible random values (inclusive).")]
        private Vector4 max = Vector4.one;

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

        public override Vector4 GetValue()
        {
            return new Vector4(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z), Random.Range(min.w, max.w));
        }
    }
}