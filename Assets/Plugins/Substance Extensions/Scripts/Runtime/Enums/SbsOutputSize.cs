using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Corresponds to the expected int values for the $outputsize Int2 input value on substances.
    /// </summary>
    public enum SbsOutputSize
    {
        [InspectorName("16")]
        _16 = 4,
        [InspectorName("32")]
        _32 = 5,
        [InspectorName("64")]
        _64 = 6,
        [InspectorName("128")]
        _128 = 7,
        [InspectorName("256")]
        _256 = 8,
        [InspectorName("512")]
        _512 = 9,
        [InspectorName("1024 (1K)")]
        _1024 = 10,
        [InspectorName("2048 (2K)")]
        _2048 = 11,
        [InspectorName("4096 (4K)")]
        _4096 = 12,
        [InspectorName("8192 (8K)")]
        _8192 = 13,
    }
}