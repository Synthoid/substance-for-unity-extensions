namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Flags determining valid scene substance graphs.
    /// </summary>
    [System.Flags]
    public enum SceneGraphType
    {
        None    = 0,
        Runtime = 1,
        Static  = 2,
        All     = ~0
    }
}