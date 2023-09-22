using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets a random Color value on a SubstanceRuntimeGraph input from a gradient.
    /// </summary>
    [System.Serializable]
    [AddMenu("Colors/Random/Gradient")]
    public class SRG_RandomColorGradient : SRG_RandomColor
    {
        [SerializeField, Tooltip("Possible color values.")]
        protected Gradient colors = new Gradient()
        {
            alphaKeys = new GradientAlphaKey[1] { new GradientAlphaKey(1f, 0f) },
            colorKeys = new GradientColorKey[2] { new GradientColorKey(Color.black, 0f), new GradientColorKey(Color.white, 1f) }
        };

        public Gradient Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        public override Color GetRandomValue()
        {
            return colors.Evaluate(Random.value);
        }
    }
}