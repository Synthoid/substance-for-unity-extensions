using UnityEngine;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random int value on a SubstanceRuntimeGraph input from a list.
    /// </summary>
    [System.Serializable]
    [AddMenu("Int/Random/List")]
    public class SRG_RandomIntList : SRG_RandomInt
    {
        [SerializeField, Tooltip("Ints that can be randomly assigned.")]
        protected List<int> ints = new List<int>();

        public List<int> Ints
        {
            get { return ints; }
            set { ints = value; }
        }

        public override int GetRandomValue()
        {
            return ints[Random.Range(0, ints.Count)];
        }
    }
}
