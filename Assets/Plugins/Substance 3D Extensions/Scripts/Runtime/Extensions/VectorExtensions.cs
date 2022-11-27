using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Contains extension methods for vector functionality.
    /// </summary>
    public static class VectorExtensions
    {
        #region Utility
        
        /// <summary>
        /// High precision version of the standard <see cref="Vector2.ToString"/> method.
        /// </summary>
        /// <param name="vector">Vector to stringify.</param>
        public static string ToVectorString(this Vector2 vector)
        {
            return string.Format("({0}, {1})", vector.x, vector.y);
        }

        /// <summary>
        /// High precision version of the standard <see cref="Vector3.ToString"/> method.
        /// </summary>
        /// <param name="vector">Vector to stringify.</param>
        public static string ToVectorString(this Vector3 vector)
        {
            return string.Format("({0}, {1}, {2})", vector.x, vector.y, vector.z);
        }

        /// <summary>
        /// High precision version of the standard <see cref="Vector4.ToString"/> method.
        /// </summary>
        /// <param name="vector">Vector to stringify.</param>
        public static string ToVectorString(this Vector4 vector)
        {
            return string.Format("({0}, {1}, {2}, {3})", vector.x, vector.y, vector.z, vector.w);
        }

        #endregion

        #region TransformMatrix

        //https://www.mathplanet.com/education/geometry/transformations/transformation-using-matrices

        /// <summary>
        /// Stretches the width of a <see cref="Vector4"/> representing a transform matrix.
        /// </summary>
        /// <param name="vector">Matrix to stretch the width of.</param>
        /// <param name="percent">Percent of the matrix's width to stretch by.</param>
        public static Vector4 StretchWidth(this Vector4 vector, float percent)
        {
            vector.x /= percent * 0.01f;

            return vector;
        }

        /// <summary>
        /// Stretches the height of a <see cref="Vector4"/> representing a transform matrix.
        /// </summary>
        /// <param name="vector">Matrix to stretch the height of.</param>
        /// <param name="percent">Percent of the matrix's height to stretch by.</param>
        public static Vector4 StretchHeight(this Vector4 vector, float percent)
        {
            vector.w /= percent * 0.01f;

            return vector;
        }

        /// <summary>
        /// Stretches the width and height of a <see cref="Vector4"/> representing a transform matrix.
        /// </summary>
        /// <param name="vector">Matrix to stretch.</param>
        /// <param name="widthPercent">Percent of the matrix's width to stretch by.</param>
        /// <param name="heightPercent">Percent of the matrix's height to stretch by.</param>
        public static Vector4 Stretch(this Vector4 vector, float widthPercent, float heightPercent)
        {
            vector.x /= widthPercent * 0.01f;
            vector.w /= heightPercent * 0.01f;

            return vector;
        }

        /// <summary>
        /// Rotates the given matrix vector clockwise around the origin.
        /// </summary>
        /// <param name="vector">Matrix to rotate.</param>
        /// <param name="angle">Amount to rotate clockwise.</param>
        public static Vector4 RotateCW(this Vector4 vector, float angle)
        {
            // Clockwise rotation is handled via the following matrix:
            // [cos(angle), sin(angle)]
            // [-sin(angle), cos(angle)]
            Vector4 rotationMatrix = new Vector4(Mathf.Cos(angle * Mathf.Deg2Rad), -Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
            
            float x1 = (vector.x * rotationMatrix.x) + (vector.z * rotationMatrix.y);
            float x2 = (vector.x * rotationMatrix.z) + (vector.z * rotationMatrix.w);
            float y1 = (vector.y * rotationMatrix.x) + (vector.w * rotationMatrix.y);
            float y2 = (vector.y * rotationMatrix.z) + (vector.w * rotationMatrix.w);

            vector.x = x1.Round(4);
            vector.z = x2.Round(4);
            vector.y = y1.Round(4);
            vector.w = y2.Round(4);

            return vector;
        }

        /// <summary>
        /// Rotates the given matrix vector counterclockwise around the origin.
        /// </summary>
        /// <param name="vector">Matrix to rotate.</param>
        /// <param name="angle">Amount to rotate counterclockwise.</param>
        public static Vector4 RotateCCW(this Vector4 vector, float angle)
        {
            // Clockwise rotation is handled via the following matrix:
            // [cos(angle), -sin(angle)]
            // [sin(angle), cos(angle)]
            Vector4 rotationMatrix = new Vector4(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), -Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

            float x1 = (vector.x * rotationMatrix.x) + (vector.z * rotationMatrix.y);
            float x2 = (vector.x * rotationMatrix.z) + (vector.z * rotationMatrix.w);
            float y1 = (vector.y * rotationMatrix.x) + (vector.w * rotationMatrix.y);
            float y2 = (vector.y * rotationMatrix.z) + (vector.w * rotationMatrix.w);

            vector.x = x1.Round(4);
            vector.z = x2.Round(4);
            vector.y = y1.Round(4);
            vector.w = y2.Round(4);

            return vector;
        }

        /// <summary>
        /// Mirrors the target matrix vector along the horizontal axis.
        /// </summary>
        /// <param name="vector">Matrix to mirror.</param>
        public static Vector4 MirrorHorizontal(this Vector4 vector)
        {
            vector.x *= -1;

            return vector;
        }

        /// <summary>
        /// Mirrors the target matrix vector along the vertixal axis.
        /// </summary>
        /// <param name="vector">Matrix to mirror.</param>
        public static Vector4 MirrorVertical(this Vector4 vector)
        {
            vector.w *= -1f;

            return vector;
        }

        /// <summary>
        /// Multiplies the target matrix vector uniformly by the given amount.
        /// </summary>
        /// <param name="vector">Matrix to muliply.</param>
        /// <param name="amount">Amount being multiplied by.</param>
        public static Vector4 Multiply(this Vector4 vector, float amount)
        {
            vector.x *= amount;
            vector.y *= amount;
            vector.z *= amount;
            vector.w *= amount;

            return vector;
        }

        /// <summary>
        /// Divides the target matrix vector uniformly by the given amount.
        /// </summary>
        /// <param name="vector">Matrix to divide.</param>
        /// <param name="amount">Amount being divided by.</param>
        public static Vector4 Divide(this Vector4 vector, float amount)
        {
            vector.x /= amount;
            vector.y /= amount;
            vector.z /= amount;
            vector.w /= amount;

            return vector;
        }

        #endregion
    }
}
