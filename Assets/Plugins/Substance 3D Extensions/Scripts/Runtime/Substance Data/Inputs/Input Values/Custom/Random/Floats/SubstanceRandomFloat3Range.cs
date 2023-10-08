using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random float3 value on a SubstanceNativeGraph input from a min and max range (inclusive).
    /// </summary>
    [System.Serializable]
    [AddMenu("Float3/Random/Range")]
    public class SubstanceRandomFloat3Range : SubstanceFloat3InputValue
    {
        [SerializeField, Tooltip("Minimum possible random values.")]
        private Vector3 min = Vector3.zero;
        [SerializeField, Tooltip("Max possible random values (inclusive).")]
        private Vector3 max = Vector3.one;

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

        public override Vector3 GetValue()
        {
            return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
        }
    }
}