using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random int value on a SubstanceNativeGraph input from a min and max range (exclusive).
    /// </summary>
    [System.Serializable]
    [AddMenu("Int/Random/Range")]
    public class SubstanceRandomIntRange : SubstanceIntInputValue
    {
        [SerializeField, Tooltip("Minimum possible random value.")]
        private int min = 0;
        [SerializeField, Tooltip("Max possible random value (exclusive).")]
        private int max = 1;

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

        public override int GetValue()
        {
            return Random.Range(min, max);
        }
    }
}