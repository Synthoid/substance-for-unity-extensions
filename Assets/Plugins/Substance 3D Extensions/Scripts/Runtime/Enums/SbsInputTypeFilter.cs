using UnityEngine;

namespace SOS.SubstanceExtensions
{
    [System.Flags]
    public enum SbsInputTypeFilter
    {
        /// <summary>
        /// Show no inputs.
        /// </summary>
        None    = 0,
        Float   = 1,
        Float2  = 2,
        Float3  = 4,
        Float4  = 8,
        Int     = 16,
        Int2    = 32,
        Int3    = 64,
        Int4    = 128,
        Image   = 256,
        String  = 512,
        /// <summary>
        /// Added for pairity with SubstanceValueType. Doesn't seem to be used currently?
        /// </summary>
        Font    = 1024,
        Everything  = ~0
    }
}