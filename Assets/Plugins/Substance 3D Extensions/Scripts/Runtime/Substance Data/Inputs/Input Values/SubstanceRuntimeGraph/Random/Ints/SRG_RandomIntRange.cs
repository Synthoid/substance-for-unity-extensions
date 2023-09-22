using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random int value on a SubstanceRuntimeGraph input from a range.
    /// </summary>
    [System.Serializable]
    [AddMenu("Int/Random/Range")]
    public class SRG_RandomIntRange : SRG_RandomInt
    {
        [SerializeField, Tooltip("Minimum possible random value.")]
        protected int min = 0;
        [SerializeField, Tooltip("Maximum possible random value.")]
        protected int max = 1;

        public int Min
        {
            get { return min; }
            set { min = value; }
        }

        public int Max
        {
            get { return max; }
            set { max = value; }
        }

        public override int GetRandomValue()
        {
            return Random.Range(min, max);
        }
    }
}