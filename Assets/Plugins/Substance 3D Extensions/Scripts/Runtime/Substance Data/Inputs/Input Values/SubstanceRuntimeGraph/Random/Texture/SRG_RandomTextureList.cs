using UnityEngine;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random texture value on a SubstanceRuntimeGraph input from a list.
    /// </summary>
    [System.Serializable]
    [AddMenu("Textures/Random/List")]
    public class SRG_RandomTextureList : SRG_RandomTexture
    {
        [SerializeField, Tooltip("Textures that can be randomly assigned.")]
        protected List<Texture2D> textures = new List<Texture2D>();

        public List<Texture2D> Textures
        {
            get { return textures; }
            set { textures = value; }
        }

        public override Texture2D GetRandomValue()
        {
            return textures[Random.Range(0, textures.Count)];
        }
    }
}