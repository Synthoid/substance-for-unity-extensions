using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random float2 value on a SubstanceNativeGraph input from a min and max range (inclusive).
    /// </summary>
    [System.Serializable]
    [AddMenu("Float2/Random/Range")]
    public class SubstanceRandomFloat2Range : SubstanceFloat2InputValue
    {
        [SerializeField, Tooltip("Minimum possible random values.")]
        private Vector2 min = Vector2.zero;
        [SerializeField, Tooltip("Max possible random values (inclusive).")]
        private Vector2 max = Vector2.one;

        public Vector2 Min
        {
            get { return min; }
            set { min = value; }
        }

        public Vector2 Max
        {
            get { return max; }
            set { max = value; }
        }

        public override Vector2 GetValue()
        {
            return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
    }
}