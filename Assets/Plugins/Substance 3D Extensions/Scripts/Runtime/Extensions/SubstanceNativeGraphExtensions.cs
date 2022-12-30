using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Extension methods for <see cref="SubstanceNativeGraph"/>.
    /// </summary>
    public static class SubstanceNativeGraphExtensions
    {
        public const string kOutputSizeIdentifier = "$outputsize";

        #region Utility

        public static void SetOutputSize(this SubstanceNativeGraph nativeGraph, int inputID, Vector2Int size)
        {
            nativeGraph.SetInputInt2(inputID, size);
        }


        public static void SetOutputSize(this SubstanceNativeGraph nativeGraph, int inputID, SbsOutputSize width, SbsOutputSize height)
        {
            Vector2Int size = new Vector2Int((int)width, (int)height);

            nativeGraph.SetInputInt2(inputID, size);
        }


        public static void SetOutputSize(this SubstanceNativeGraph nativeGraph, int inputID, SbsOutputSize size)
        {
            Vector2Int newSize = new Vector2Int((int)size, (int)size);

            nativeGraph.SetInputInt2(inputID, newSize);
        }

        /// <summary>
        /// Sets an int4 input's value.
        /// </summary>
        /// <param name="nativeGraph">Graph to set the value on.</param>
        /// <param name="inputID">Index for the target input.</param>
        /// <param name="value">New value for the target input.</param>
        public static void SetInputInt4(this SubstanceNativeGraph nativeGraph, int inputID, Vector4Int value)
        {
            nativeGraph.SetInputInt4(inputID, value.x, value.y, value.z, value.w);
        }

        #endregion

        #region Set

        /// <summary>
        /// Set all native graph input values targeted in the given IList.
        /// </summary>
        /// <typeparam name="T">Expected type for the values data.</typeparam>
        /// <param name="nativeGraph">Graph to set input values on.</param>
        /// <param name="values">New values for graph inputs.</param>
        public static void SetInputValues<T>(this SubstanceNativeGraph nativeGraph, IList<T> values) where T : ISubstanceInputParameterValue
        {
            SetInputValues(nativeGraph, (IList<ISubstanceInputParameterValue>)values);
        }

        /// <summary>
        /// Set all native graph input values targeted in the given IList.
        /// </summary>
        /// <param name="nativeGraph">Graph to set input values on.</param>
        /// <param name="values">New values for graph inputs.</param>
        public static void SetInputValues(this SubstanceNativeGraph nativeGraph, IList<ISubstanceInputParameterValue> values)
        {
            for (int i=0; i < values.Count; i++)
            {
                values[i].SetValue(nativeGraph);
            }
        }

        /// <summary>
        /// Asynchronously set all native graph input values targeted in the given IList. This allows for GPU based texture assignments.
        /// </summary>
        /// <typeparam name="T">Expected type for the values data.</typeparam>
        /// <param name="nativeGraph">Graph to set input values on.</param>
        /// <param name="values">New values for graph inputs.</param>
        /// <returns>Task for the set operation.</returns>
        public static Task SetInputValuesAsync<T>(this SubstanceNativeGraph nativeGraph, IList<T> values) where T : ISubstanceInputParameterValue
        {
            return SetInputValuesAsync(nativeGraph, (IList<ISubstanceInputParameterValue>)values);
        }

        /// <summary>
        /// Asynchronously set all native graph input values targeted in the given IList. This allows for GPU based texture assignments.
        /// </summary>
        /// <param name="nativeGraph">Graph to set input values on.</param>
        /// <param name="values">New values for graph inputs.</param>
        /// <returns>Task for the set operation.</returns>
        public static async Task SetInputValuesAsync(this SubstanceNativeGraph nativeGraph, IList<ISubstanceInputParameterValue> values)
        {
            List<Task> tasks = new List<Task>();

            for (int i=0; i < values.Count; i++)
            {
                if(values[i].ValueType == SubstanceValueType.Image)
                {
                    tasks.Add(values[i].SetValueAsync(nativeGraph));
                }
                else
                {
                    values[i].SetValue(nativeGraph);
                }
            }

            if(tasks.Count > 0) await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Set an input value on the runtime graph.
        /// </summary>
        /// <param name="nativeGraph">Runtime graph to set an input value on.</param>
        /// <param name="parameterValue">Data for the target input and its new value.</param>
        public static void SetInputValue(this SubstanceNativeGraph nativeGraph, ISubstanceInputParameterValue parameterValue)
        {
            parameterValue.SetValue(nativeGraph);
        }

        /// <summary>
        /// Asynchronously set an input value on the runtime graph.
        /// </summary>
        /// <param name="nativeGraph">Runtime graph to set an input value on.</param>
        /// <param name="parameterValue">Data for the target input and its new value.</param>
        /// <returns>Task for the set operation.</returns>
        public static Task SetInputValueAsync(this SubstanceNativeGraph nativeGraph, ISubstanceInputParameterValue parameterValue)
        {
            return parameterValue.SetValueAsync(nativeGraph);
        }


        /// <summary>
        /// Set a texture input value on the target native graph.
        /// This uses an <see cref="AsyncGPUReadbackRequest"/> so the given texture doesn't need it's read/write flag enabled.
        /// NOTE: This method will halt the main thread while the texture is read. Use SetInputTextureGPUAsync() for better performance.
        /// </summary>
        /// <param name="nativeGraph">Graph to set an input texture value on.</param>
        /// <param name="inputID">Index for the input being set.</param>
        /// <param name="texture">Texture to assign.</param>
        public static void SetInputTextureGPU(this SubstanceNativeGraph nativeGraph, int inputID, Texture texture)
        {
            if(texture == null)
            {
                nativeGraph.SetInputTexture2DNull(inputID);
                return;
            }

            if(texture.IsCompressed())
            {
                Debug.LogWarning("[Substance Extensions - SetInputTextureGPU]\nOnly uncompressed texture formats are supported currently. This will be addressed in a later update.");
                return;
            }

            byte[] bytes = null;

            AsyncGPUReadbackRequest request = AsyncGPUReadback.Request(texture, 0, (request) =>
            {
                bytes = request.GetData<byte>().ToArray();
            });

            request.WaitForCompletion();

            Color32[] pixels = new Color32[bytes.Length / 4];

            for(int i=0; i < pixels.Length; i++)
            {
                int index = i * 4;
                pixels[i] = new Color32(bytes[index], bytes[index + 1], bytes[index + 2], bytes[index + 3]);
            }

            nativeGraph.SetInputTexture2D(inputID, pixels, texture.width, texture.height);
        }


        /// <summary>
        /// Asynchronously set a texture input value on the target native graph.
        /// This uses an <see cref="AsyncGPUReadbackRequest"/> so the given texture doesn't need it's read/write flag enabled.
        /// </summary>
        /// <param name="nativeGraph">Graph to set an input texture value on.</param>
        /// <param name="inputID">Index for the input being set.</param>
        /// <param name="texture">Texture to assign.</param>
        /// <param name="callback">[Optional] Callback invoked when the operation completes.</param>
        /// <returns>Awaitable <see cref="Task"/> representing the set operation.</returns>
        public static async Task SetInputTextureGPUAsync(this SubstanceNativeGraph nativeGraph, int inputID, Texture texture, System.Action callback=null)
        {
            if(texture == null)
            {
                nativeGraph.SetInputTexture2DNull(inputID);

                if(callback != null) callback.Invoke();

                return;
            }

            if(texture.IsCompressed())
            {
                Debug.LogWarning("[Substance Extensions - SetInputTextureGPUAsync]\nOnly uncompressed texture formats are supported currently. This will be addressed in a later update.");

                if(callback != null) callback.Invoke();

                return;
            }

            bool wait = true;
            byte[] bytes = null;

            AsyncGPUReadback.Request(texture, 0, (request) =>
            {
                bytes = request.GetData<byte>().ToArray();
                wait = false;
            });

            while(wait) await Task.Yield();

            Color32[] pixels = new Color32[bytes.Length / 4];

            for(int i=0; i < pixels.Length; i++)
            {
                int index = i * 4;
                pixels[i] = new Color32(bytes[index], bytes[index + 1], bytes[index + 2], bytes[index + 3]);
            }

            nativeGraph.SetInputTexture2D(inputID, pixels, texture.width, texture.height);

            if(callback != null) callback.Invoke();
        }

        /// <summary>
        /// Set the native graph's texture input value. Note: the given texture must have read/write enabled for this to work. For non-CPU operations, use <see cref="SetInputTextureGPUAsync"/> instead.
        /// </summary>
        /// <param name="nativeGraph">Graph to set an input texture value on.</param>
        /// <param name="inputID">Index for the input being set.</param>
        /// <param name="texture">Texture to assign. The texture must have read/write enabled.</param>
        public static void SetInputTextureCPU(this SubstanceNativeGraph nativeGraph, int inputID, Texture2D texture)
        {
            if(texture == null)
            {
                nativeGraph.SetInputTexture2DNull(inputID);
                return;
            }

            nativeGraph.SetInputTexture2D(inputID, texture.GetPixels32(), texture.width, texture.height);
        }

        #endregion

        #region Render

        /// <summary>
        /// Asynchronously render the given native graph. Note that after rendering the native graph, you will need to call <see cref="SubstanceGraphSO.UpdateOutputTextures(IntPtr)"/> on the original graph asset to see the updated textures.
        /// </summary>
        /// <param name="nativeGraph">Native graph to render.</param>
        /// <returns><see cref="Task"/> representing the render operaion. Task result is the result pointer for the render operation.</returns>
        public static Task<IntPtr> RenderAsync(this SubstanceNativeGraph nativeGraph)
        {
            TaskCompletionSource<IntPtr> completionSource = new TaskCompletionSource<IntPtr>();

            Task.Run(() =>
            {
                try
                {
                    IntPtr renderResult = nativeGraph.Render();

                    completionSource.SetResult(renderResult);
                }
                catch(Exception e)
                {
                    completionSource.SetException(e);
                }
            });

            return completionSource.Task;
        }

        #endregion
    }
}