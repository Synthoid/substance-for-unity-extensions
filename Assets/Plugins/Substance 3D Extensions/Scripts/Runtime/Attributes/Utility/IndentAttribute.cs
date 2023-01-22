using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Convenience attribute that indents a field in the inspector.
    /// </summary>
    public class IndentAttribute : PropertyAttribute
    {
        /// <summary>
        /// Level to add to the current indent index.
        /// </summary>
        public readonly int level = 1;

        /// <summary>
        /// Convenience attribute that indents a field in the inspector.
        /// </summary>
        /// <param name="level">Level to add to the current indent index.</param>
        public IndentAttribute(int level=1)
        {
            this.level = level;
        }
    }
}