using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Draws a <see cref="Vector4"/> as a Substance Designer transform matrix field.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple=false)]
    public class TransformMatrixAttribute : PropertyAttribute
    {
        /// <summary>
        /// Determines if the angle field uses clockwise (forward) or counterclockwise (inverse) rotation.
        /// </summary>
        public readonly TransformMatrixType type = TransformMatrixType.Forward;

        /// <summary>
        /// Draws a <see cref="Vector4"/> as a Substance Designer transform matrix field.
        /// </summary>
        /// <param name="type">Determines if the angle field uses clockwise (forward) or counterclockwise (inverse) rotation.</param>
        public TransformMatrixAttribute(TransformMatrixType type=TransformMatrixType.Forward)
        {
            this.type = type;
        }
    }
}
