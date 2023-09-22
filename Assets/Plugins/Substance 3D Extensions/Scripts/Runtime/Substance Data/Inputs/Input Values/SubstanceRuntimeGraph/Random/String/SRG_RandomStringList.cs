using UnityEngine;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random string value on a SubstanceRuntimeGraph input from a list.
    /// </summary>
    [System.Serializable]
    [AddMenu("Strings/Random/List")]
    public class SRG_RandomStringList : SRG_RandomString
    {
        [SerializeField, Tooltip("Strings that can be randomly assigned.")]
        protected List<string> strings = new List<string>();

        public List<string> Strings
        {
            get { return strings; }
            set { strings = value; }
        }

        public override string GetRandomValue()
        {
            return strings[Random.Range(0, strings.Count)];
        }
    }
}