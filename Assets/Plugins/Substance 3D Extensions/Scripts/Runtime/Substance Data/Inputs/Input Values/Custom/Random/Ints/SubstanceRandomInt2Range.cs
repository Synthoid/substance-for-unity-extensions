using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random int2 value on a SubstanceNativeGraph input from a min and max range (exclusive).
    /// </summary>
    [System.Serializable]
    [AddMenu("Int2/Random/Range")]
    public class SubstanceRandomInt2Range : SubstanceInt2InputValue
    {
        [SerializeField, Tooltip("Minimum possible random values.")]
        private Vector2Int min = Vector2Int.zero;
        [SerializeField, Tooltip("Max possible random values (exclusive).")]
        private Vector2Int max = Vector2Int.one;

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

        public override Vector2Int GetValue()
        {
            return new Vector2Int(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
    }
}