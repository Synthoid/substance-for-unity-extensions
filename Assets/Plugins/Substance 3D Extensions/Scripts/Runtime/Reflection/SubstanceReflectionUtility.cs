using UnityEngine;
using System;
using System.Reflection;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Runtime reflection utility to access internal and private functionality of Adobe's code.
    /// </summary>
    public static class SubstanceReflectionUtility
    {
        #region SubstanceNativeGraph

        private static MethodInfo setInputTexture2D_RGBA32_MethodInfo = null;
        private static MethodInfo setInputTexture2D_RGBA64_MethodInfo = null;

        public static MethodInfo SetInputTexture2D_RGBA32_MethodInfo
        {
            get
            {
                if(setInputTexture2D_RGBA32_MethodInfo == null)
                {
                    Type nativeType = typeof(SubstanceNativeGraph);

                    setInputTexture2D_RGBA32_MethodInfo = nativeType.GetMethod("SetInputTexture2D_RGBA32", BindingFlags.Instance | BindingFlags.NonPublic);
                }

                return setInputTexture2D_RGBA32_MethodInfo;
            }
        }

        public static MethodInfo SetInputTexture2D_RGBA64_MethodInfo
        {
            get
            {
                if(setInputTexture2D_RGBA64_MethodInfo == null)
                {
                    Type nativeType = typeof(SubstanceNativeGraph);

                    setInputTexture2D_RGBA64_MethodInfo = nativeType.GetMethod("setInputTexture2D_RGBA64", BindingFlags.Instance | BindingFlags.NonPublic);
                }

                return setInputTexture2D_RGBA64_MethodInfo;
            }
        }

        public static void SetInputTexture2D_RGBA32(SubstanceNativeGraph nativeGraph, int inputID, Color32[] pixelData, int width, int height)
        {
            SetInputTexture2D_RGBA32_MethodInfo.Invoke(nativeGraph, new object[4] { inputID, pixelData, width, height });
        }


        public static void SetInputTexture2D_RGBA64(SubstanceNativeGraph nativeGraph, int inputID, Color[] pixelData, int width, int height)
        {
            SetInputTexture2D_RGBA64_MethodInfo.Invoke(nativeGraph, new object[4] { inputID, pixelData, width, height });
        }

        #endregion
    }
}