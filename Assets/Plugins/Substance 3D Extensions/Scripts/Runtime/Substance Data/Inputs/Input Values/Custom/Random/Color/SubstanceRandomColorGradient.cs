using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Sets an input value by randomly sampling along a gradient.
    /// </summary>
    [System.Serializable]
    [AddMenu("Color/Random/Gradient")]
    public class SubstanceRandomColorGradient : SubstanceColorValue
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


        public override Color GetValue()
        {
            return colors.Evaluate(Random.value);
        }
    }
}