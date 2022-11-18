using UnityEngine;
using System;

namespace SOS.SubstanceExtensions
{
    public static class FloatExtensions
    {
        /// <summary>
        /// Rounds a float value to the given decimal range.
        /// </summary>
        /// <param name="value">Value to round.</param>
        /// <param name="decimals">Number of decimal places to round.</param>
        public static float Round(this float value, int decimals)
        {
            return (float)Math.Round(value, decimals);
        }
    }
}
