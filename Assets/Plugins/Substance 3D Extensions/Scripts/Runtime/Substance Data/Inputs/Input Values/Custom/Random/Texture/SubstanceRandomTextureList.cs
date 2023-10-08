using UnityEngine;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random texture value on a SubstanceNativeGraph input from a list.
    /// </summary>
    [System.Serializable]
    [AddMenu("Texture/Random/List")]
    public class SubstanceRandomTextureList : SubstanceTextureInputValue
    {
        [SerializeField, Tooltip("Textures that can be randomly assigned.")]
        private List<Texture> textures = new List<Texture>();

        public List<Texture> Textures
        {
            get { return textures; }
            set { textures = value; }
        }

        public override Texture GetValue()
        {
            return textures[Random.Range(0, textures.Count)];
        }
    }
}