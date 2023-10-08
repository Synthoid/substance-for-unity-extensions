using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random float value on a SubstanceNativeGraph input from a min and max range (inclusive).
    /// </summary>
    [System.Serializable]
    [AddMenu("Float/Random/Range")]
    public class SubstanceRandomFloatRange : SubstanceFloatInputValue
    {
        [SerializeField, Tooltip("Minimum possible random value.")]
        private float min = 0f;
        [SerializeField, Tooltip("Max possible random value (inclusive).")]
        private float max = 1f;

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

        public override float GetValue()
        {
            return Random.Range(min, max);
        }
    }
}