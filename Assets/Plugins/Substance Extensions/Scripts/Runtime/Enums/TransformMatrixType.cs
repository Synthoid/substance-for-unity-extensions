namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Determines if transform matrix rotations are applied clockwise or counterclockwise.
    /// </summary>
    public enum TransformMatrixType : byte
    {
        /// <summary>
        /// Angle field rotations go clockwise.
        /// </summary>
        Forward = 0,
        /// <summary>
        /// Angle field rotations go counterclockwise.
        /// </summary>
        Inverse = 1
    }
}
