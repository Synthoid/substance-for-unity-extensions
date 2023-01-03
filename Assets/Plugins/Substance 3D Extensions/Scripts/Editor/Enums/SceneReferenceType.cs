namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Determines how multiple open scenes are handled in operations.
    /// </summary>
    public enum SceneReferenceType
    {
        /// <summary>
        /// Use all open scenes.
        /// </summary>
        All         = 0,
        /// <summary>
        /// Only use the active open scene.
        /// </summary>
        ActiveOnly  = 1
    }
}