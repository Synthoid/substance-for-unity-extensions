using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random int3 value on a SubstanceNativeGraph input from a min and max range (exclusive).
    /// </summary>
    [System.Serializable]
    [AddMenu("Int3/Random/Range")]
    public class SubstanceRandomInt3Range : SubstanceInt3InputValue
    {
        [SerializeField, Tooltip("Minimum possible random values.")]
        private Vector3Int min = Vector3Int.zero;
        [SerializeField, Tooltip("Max possible random values (exclusive).")]
        private Vector3Int max = Vector3Int.one;

        public Vector3Int Min
        {
            get { return min; }
            set { min = value; }
        }

        public Vector3Int Max
        {
            get { return max; }
            set { max = value; }
        }

        public override Vector3Int GetValue()
        {
            return new Vector3Int(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
        }
    }
}