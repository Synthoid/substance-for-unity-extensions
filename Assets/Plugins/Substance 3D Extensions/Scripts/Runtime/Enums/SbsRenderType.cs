namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Enum representing render status for a substance.
    /// </summary>
    public enum SbsRenderType
    {
        /// <summary>
        /// Substance has not rendered.
        /// </summary>
        None        = 0,
        /// <summary>
        /// Substance should be queued for immediate rendering.
        /// </summary>
        Queued   = 1,
        /// <summary>
        /// Substance should defer rendering.
        /// </summary>
        Deferred    = 2,
        /// <summary>
        /// Substance is currently rendering.
        /// </summary>
        Rendering   = 3,
        /// <summary>
        /// Substance has finished rendering.
        /// </summary>
        Rendered    = 4
    }
}