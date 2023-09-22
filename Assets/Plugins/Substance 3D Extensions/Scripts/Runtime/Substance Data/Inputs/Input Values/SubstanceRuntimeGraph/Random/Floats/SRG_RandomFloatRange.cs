using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random float value on a SubstanceRuntimeGraph input from a range.
    /// </summary>
    [System.Serializable]
    [AddMenu("Float/Random/Range")]
    public class SRG_RandomFloatRange : SRG_RandomFloat
    {
        [SerializeField, Tooltip("Minimum possible random value.")]
        protected float min = 0f;
        [SerializeField, Tooltip("Maximum possible random value.")]
        protected float max = 1f;

        public float Min
        {
            get { return min; }
            set { min = value; }
        }

        public float Max
        {
            get { return max; }
            set { max = value; }
        }

        public override float GetRandomValue()
        {
            return Random.Range(min, max);
        }
    }
}