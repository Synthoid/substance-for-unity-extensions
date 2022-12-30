using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Contains extensions for <see cref="Texture"/> classes.
    /// </summary>
    public static class TextureExtensions
    {
        /// <summary>
        /// <see cref="GraphicsFormat"/> values representing compressed formats.
        /// </summary>
        public static GraphicsFormat[] CompressedGraphicsFormats = new GraphicsFormat[]
        {
            GraphicsFormat.RGBA_DXT1_SRGB,
            GraphicsFormat.RGBA_DXT1_UNorm,
            GraphicsFormat.RGBA_DXT3_SRGB,
            GraphicsFormat.RGBA_DXT3_UNorm,
            GraphicsFormat.RGBA_DXT5_SRGB,
            GraphicsFormat.RGBA_DXT5_UNorm,
            GraphicsFormat.R_BC4_UNorm,
            GraphicsFormat.R_BC4_SNorm,
            GraphicsFormat.RG_BC5_UNorm,
            GraphicsFormat.RG_BC5_SNorm,
            GraphicsFormat.RGB_BC6H_UFloat,
            GraphicsFormat.RGB_BC6H_SFloat,
            GraphicsFormat.RGBA_BC7_SRGB,
            GraphicsFormat.RGBA_BC7_UNorm,
            GraphicsFormat.RGB_PVRTC_2Bpp_SRGB,
            GraphicsFormat.RGB_PVRTC_2Bpp_UNorm,
            GraphicsFormat.RGB_PVRTC_4Bpp_SRGB,
            GraphicsFormat.RGB_PVRTC_4Bpp_UNorm,
            GraphicsFormat.RGBA_PVRTC_2Bpp_SRGB,
            GraphicsFormat.RGBA_PVRTC_2Bpp_UNorm,
            GraphicsFormat.RGBA_PVRTC_4Bpp_SRGB,
            GraphicsFormat.RGBA_PVRTC_4Bpp_UNorm,
            GraphicsFormat.RGB_ETC_UNorm,
            GraphicsFormat.RGB_ETC2_SRGB,
            GraphicsFormat.RGB_ETC2_UNorm,
            GraphicsFormat.RGB_A1_ETC2_SRGB,
            GraphicsFormat.RGB_A1_ETC2_UNorm,
            GraphicsFormat.RGBA_ETC2_SRGB,
            GraphicsFormat.RGBA_ETC2_UNorm,
            GraphicsFormat.R_EAC_UNorm,
            GraphicsFormat.R_EAC_SNorm,
            GraphicsFormat.RG_EAC_UNorm,
            GraphicsFormat.RG_EAC_SNorm,
            GraphicsFormat.RGBA_ASTC4X4_SRGB,
            GraphicsFormat.RGBA_ASTC4X4_UNorm,
            GraphicsFormat.RGBA_ASTC5X5_SRGB,
            GraphicsFormat.RGBA_ASTC5X5_UNorm,
            GraphicsFormat.RGBA_ASTC6X6_SRGB,
            GraphicsFormat.RGBA_ASTC6X6_UNorm,
            GraphicsFormat.RGBA_ASTC8X8_SRGB,
            GraphicsFormat.RGBA_ASTC8X8_UNorm,
            GraphicsFormat.RGBA_ASTC10X10_SRGB,
            GraphicsFormat.RGBA_ASTC10X10_UNorm,
            GraphicsFormat.RGBA_ASTC12X12_SRGB,
            GraphicsFormat.RGBA_ASTC12X12_UNorm,
            GraphicsFormat.RGBA_ASTC4X4_UFloat,
            GraphicsFormat.RGBA_ASTC5X5_UFloat,
            GraphicsFormat.RGBA_ASTC6X6_UFloat,
            GraphicsFormat.RGBA_ASTC8X8_UFloat,
            GraphicsFormat.RGBA_ASTC10X10_UFloat,
            GraphicsFormat.RGBA_ASTC12X12_UFloat
        };

        /// <summary>
        /// Returns true if the texture asset is using a compressed graphics format. These formats are referenced via <see cref="CompressedGraphicsFormats"/>.
        /// </summary>
        /// <param name="texture">Texture to check.</param>
        /// <returns>True if the texture asset is using a compressed graphics format.</returns>
        public static bool IsCompressed(this Texture texture)
        {
            return CompressedGraphicsFormats.Contains(texture.graphicsFormat);
        }
    }
}