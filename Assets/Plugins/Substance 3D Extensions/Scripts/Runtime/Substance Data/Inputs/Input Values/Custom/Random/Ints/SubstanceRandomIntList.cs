using UnityEngine;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random int value on a SubstanceNativeGraph input from list.
    /// </summary>
    [System.Serializable]
    [AddMenu("Int/Random/List")]
    public class SubstanceRandomIntList : SubstanceIntInputValue
    {
        [SerializeField, Tooltip("Ints that can be randomly assigned.")]
        private List<int> values = new List<int>();

        public List<int> Values
        {
            get { return values; }
            set { values = value; }
        }

        public override int GetValue()
        {
            return values[Random.Range(0, values.Count)];
        }
    }
}