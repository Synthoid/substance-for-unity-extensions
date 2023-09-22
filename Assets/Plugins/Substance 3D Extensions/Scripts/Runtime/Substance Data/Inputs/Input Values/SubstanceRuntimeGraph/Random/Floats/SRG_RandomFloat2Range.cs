using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random float2 value on a SubstanceRuntimeGraph input from a range.
    /// </summary>
    [System.Serializable]
    [AddMenu("Float2/Random/Range")]
    public class SRG_RandomFloat2Range : SRG_RandomFloat2
    {
        [SerializeField, Tooltip("Minimum possible random value.")]
        protected Vector2 min = Vector2.zero;
        [SerializeField, Tooltip("Maximum possible random value.")]
        protected Vector2 max = Vector2.one;

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

        public override Vector2 GetRandomValue()
        {
            return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
    }
}