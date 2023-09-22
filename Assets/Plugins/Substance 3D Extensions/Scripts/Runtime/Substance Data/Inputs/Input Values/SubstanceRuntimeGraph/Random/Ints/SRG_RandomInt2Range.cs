using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random int2 value on a SubstanceRuntimeGraph input from a range.
    /// </summary>
    [System.Serializable]
    [AddMenu("Int2/Random/Range")]
    public class SRG_RandomInt2Range : SRG_RandomInt2
    {
        [SerializeField, Tooltip("Minimum possible random value.")]
        protected Vector2Int min = Vector2Int.zero;
        [SerializeField, Tooltip("Maximum possible random value.")]
        protected Vector2Int max = Vector2Int.one;

        public Vector2Int Min
        {
            get { return min; }
            set { min = value; }
        }

        public Vector2Int Max
        {
            get { return max; }
            set { max = value; }
        }

        public override Vector2Int GetRandomValue()
        {
            return new Vector2Int(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
    }
}
