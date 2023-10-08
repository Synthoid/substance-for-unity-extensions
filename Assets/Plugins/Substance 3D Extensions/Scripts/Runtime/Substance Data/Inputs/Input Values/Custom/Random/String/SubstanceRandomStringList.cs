using UnityEngine;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random string value on a SubstanceNativeGraph input from a list.
    /// </summary>
    [System.Serializable]
    [AddMenu("String/Random/List")]
    public class SubstanceRandomStringList : SubstanceStringInputValue
    {
        [SerializeField, Tooltip("Strings that can be randomly assigned.")]
        private List<string> values = new List<string>();

        public List<string> Values
        {
            get { return values; }
            set { values = value; }
        }

        public override string GetValue()
        {
            return values[Random.Range(0, values.Count)];
        }
    }
}