using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Contains extension methods for <see cref="SubstanceGraphSO"/> runtime functionality.
    /// </summary>
    public static class SubstanceGraphRuntimeExtensions
    {

        #region Inputs

        /// <summary>
        /// Set the input values on the graph asset to match the inputs on the given native graph.
        /// </summary>
        /// <param name="graph">Graph asset to set input values on.</param>
        /// <param name="nativeGraph">Native graph to copy values from.</param>
        public static void SetInputValues(this SubstanceGraphSO graph, SubstanceNativeGraph nativeGraph)
        {
            List<ISubstanceInput> inputs = nativeGraph.GetInputs();

            graph.SetInputValues(inputs);
        }

        #endregion

        #region Rendering

        private static Dictionary<int, SubstanceNativeGraph> RuntimeNativeGraphs = new Dictionary<int, SubstanceNativeGraph>();

        /// <summary>
        /// Displose all cached runtime native graphs and clear the dictionary cache.
        /// </summary>
        public static void ClearCachedGraphs()
        {
            foreach(int id in RuntimeNativeGraphs.Keys)
            {
                RuntimeNativeGraphs[id].Dispose();
            }

            RuntimeNativeGraphs.Clear();
        }

        /// <summary>
        /// Get the cahced native graph associated with the given graph asset.
        /// </summary>
        /// <param name="graph">Graph associated with the chached native graph.</param>
        /// <param name="create">If true, graphs without a cached native graph will have a native graph generated and cached.</param>
        /// <returns><see cref="SubstanceNativeGraph"/> associated with the given graph.</returns>
        public static SubstanceNativeGraph GetCachedNativeGraph(this SubstanceGraphSO graph, bool create=true)
        {
            bool success = RuntimeNativeGraphs.TryGetValue(graph.GetInstanceID(), out SubstanceNativeGraph nativeGraph);

            if (!success && create)
            {
                nativeGraph = Engine.OpenFile(graph.RawData.FileContent, graph.Index);

                graph.RuntimeInitialize(nativeGraph, true);

                RuntimeNativeGraphs.Add(graph.GetInstanceID(), nativeGraph);
            }

            return nativeGraph;
        }

        /// <summary>
        /// Dispose the cached native graph associated with the given graph.
        /// </summary>
        /// <param name="graph">Graph associated with the chached native graph.</param>
        public static void DisposeNativeGraph(this SubstanceGraphSO graph)
        {
            DisposeNativeGraph(graph, GetCachedNativeGraph(graph, false));
        }

        /// <summary>
        /// Dispose the native graph associated with the given graph.
        /// </summary>
        /// <param name="graph">Graph associated with the native graph.</param>
        /// <param name="nativeGraph">Native graph to dispose.</param>
        public static void DisposeNativeGraph(this SubstanceGraphSO graph, SubstanceNativeGraph nativeGraph)
        {
            if(nativeGraph == null) return;

            RuntimeNativeGraphs.Remove(graph.GetInstanceID());

            nativeGraph.Dispose();
        }

        /// <summary>
        /// Initialize the graph for runtime generation and create a cached native graph for use in render operations.
        /// Note: You should call <see cref="EndRuntimeEditing(SubstanceGraphSO, SubstanceNativeGraph)"/> when you are done rendering with the returned native graph.
        /// </summary>
        /// <param name="graph">Graph to initialize for runtime.</param>
        /// <returns><see cref="SubstanceNativeGraph"/> associated with the given graph. This can be used when setting input parameters and during render operations.</returns>
        public static SubstanceNativeGraph BeginRuntimeEditing(this SubstanceGraphSO graph)
        {
            return GetCachedNativeGraph(graph);
        }

        /// <summary>
        /// Dispose the cached native graph associated with the given graph.
        /// </summary>
        /// <param name="graph">Graph to dispose the runtime native graph for.</param>
        public static void EndRuntimeEditing(this SubstanceGraphSO graph)
        {
            EndRuntimeEditing(graph, GetCachedNativeGraph(graph, false));
        }

        /// <summary>
        /// Dispose the native graph associated with the given graph.
        /// </summary>
        /// <param name="graph">Graph to dispose the runtime native graph for.</param>
        /// <param name="nativeGraph">Native graph being disposed.</param>
        public static void EndRuntimeEditing(this SubstanceGraphSO graph, SubstanceNativeGraph nativeGraph)
        {
            DisposeNativeGraph(graph, nativeGraph);
        }

        /// <summary>
        /// Render the graph. If the graph has not been initialized for runtime, it will be initialized as part of this operation.
        /// </summary>
        /// <param name="graph">Graph to render.</param>
        public static void Render(this SubstanceGraphSO graph)
        {
            Render(graph, GetCachedNativeGraph(graph, true));
        }

        /// <summary>
        /// Render the graph with the given native graph handle.
        /// </summary>
        /// <param name="graph">Graph to render.</param>
        /// <param name="nativeGraph">Native graph used for rendering.</param>
        public static void Render(this SubstanceGraphSO graph, SubstanceNativeGraph nativeGraph)
        {
            IntPtr renderResult = nativeGraph.Render();

            UpdateOutputTextureSizes(graph, renderResult);
        }

        /// <summary>
        /// Asynchronously render the graph.
        /// </summary>
        /// <param name="graph">Graph to render.</param>
        /// <returns><see cref="Task"/> for the render operation.</returns>
        public static Task RenderAsync(this SubstanceGraphSO graph)
        {
            return RenderAsync(graph, GetCachedNativeGraph(graph, true));
        }

        /// <summary>
        /// Asynchronously render the graph using the given native graph.
        /// </summary>
        /// <param name="graph">Graph to render.</param>
        /// <param name="nativeGraph">Native graph used for the render operation.</param>
        /// <returns><see cref="Task"/> for the render operation.</returns>
        public static async Task RenderAsync(this SubstanceGraphSO graph, SubstanceNativeGraph nativeGraph)
        {
            if(nativeGraph == null) return;

            Task<IntPtr> renderTask = nativeGraph.RenderAsync();

            await renderTask;

            UpdateOutputTextureSizes(graph, renderTask.Result);
        }

        /// <summary>
        /// Wrapper for <see cref="SubstanceGraphSO.UpdateOutputTextures(IntPtr)"/> that will resize output textures if needed based on the given render result.
        /// </summary>
        /// <param name="graph">Graph that was rendered.</param>
        /// <param name="renderResult">Result from a render operation.</param>
        public static void UpdateOutputTextureSizes(this SubstanceGraphSO graph, IntPtr renderResult)
        {
            List<(int, Vector2Int)> renderResultSizes = graph.GetResizedOutputs(renderResult);

            for(int i=0; i < renderResultSizes.Count; i++)
            {
                int index = renderResultSizes[i].Item1;
                Vector2Int size = renderResultSizes[i].Item2;
                SubstanceOutputTexture outputTexture = graph.Output[index];

#if UNITY_2021_2_OR_NEWER
                outputTexture.OutputTexture.Reinitialize(size.x, size.y);
#else
                outputTexture.OutputTexture.Resize(size.x, size.y);
#endif
            }

            graph.UpdateOutputTextures(renderResult);
        }

        /// <summary>
        /// Render the graph and immediately dispose of its native graph handle. This can be used when a graph only needs to render once and its handle doesn't need to be kept in memory.
        /// </summary>
        /// <param name="graph">Graph to render.</param>
        public static void RenderAndForget(this SubstanceGraphSO graph)
        {
            SubstanceNativeGraph nativeGraph = BeginRuntimeEditing(graph);

            Render(graph, nativeGraph);

            EndRuntimeEditing(graph, nativeGraph);
        }


        /// <summary>
        /// Asynchronously render the graph and dispose of its native graph handle after. This can be used when a graph only needs to render once and its handle doesn't need to be kept in memory.
        /// </summary>
        /// <param name="graph">Graph to render.</param>
        /// <param name="callback">[Optional] Callback invoked when the render is complete.</param>
        public static async void RenderAndForgetAsync(this SubstanceGraphSO graph, System.Action callback)
        {
            SubstanceNativeGraph nativeGraph = BeginRuntimeEditing(graph);

            await RenderAsync(graph, nativeGraph);

            EndRuntimeEditing(graph, nativeGraph);

            if(callback != null) callback.Invoke();
        }

        /// <summary>
        /// Asynchronously render the graph and dispose of its native graph handle after. This can be used when a graph only needs to render once and its handle doesn't need to be kept in memory.
        /// </summary>
        /// <param name="graph">Graph to render.</param>
        /// <returns>Task for the render operation.</returns>
        public static async Task RenderAndForgetAsync(this SubstanceGraphSO graph)
        {
            SubstanceNativeGraph nativeGraph = BeginRuntimeEditing(graph);

            await RenderAsync(graph, nativeGraph);

            EndRuntimeEditing(graph, nativeGraph);
        }

        /*public static void SetInputs(this SubstanceGraphSO substance, SubstanceNativeGraph handler, IList<SubstanceParameterValue> values)
        {
            for(int i = 0; i < values.Count; i++)
            {
                values[i].SetValue(handler);
            }
        }


        public static void SetInputAndRender(this SubstanceGraphSO substance, SubstanceParameterValue value)
        {
            using(SubstanceNativeGraph handler = Engine.OpenFile(substance.RawData.FileContent, substance.Index))
            {
                substance.RuntimeInitialize(handler, true);

                value.SetValue(handler);

                IntPtr result = handler.Render();
                substance.UpdateOutputTextures(result);
            }
        }


        public static void SetInputsAndRender(this SubstanceGraphSO substance, IList<SubstanceParameterValue> values)
        {
            using(SubstanceNativeGraph handler = Engine.OpenFile(substance.RawData.FileContent, substance.Index))
            {
                for(int j = 0; j < values.Count; j++)
                {
                    substance.RuntimeInitialize(handler, true);

                    values[j].SetValue(handler);
                }

                IntPtr result = handler.Render();
                substance.UpdateOutputTextures(result);
            }
        }


        public static void SetInputsAndRender(this SubstanceGraphSO substance, IList<SubstanceParameterValue> values, SubstanceNativeGraph handler)
        {
            substance.RuntimeInitialize(handler, true); //TODO: This is rendering output textures... Need a custom one...

            for(int i = 0; i < values.Count; i++)
            {
                values[i].SetValue(handler);
            }

            IntPtr result = handler.Render();
            substance.UpdateOutputTextures(result);
        }


        public static async Task SetInputsAndRenderAsync(this SubstanceGraphSO substance, IList<SubstanceParameterValue> values)
        {
            IntPtr pointer = default;
            string error = "";

            using(SubstanceNativeGraph handler = Engine.OpenFile(substance.RawData.FileContent, substance.Index))
            {
                substance.RuntimeInitialize(handler);

                for(int j = 0; j < values.Count; j++)
                {
                    values[j].SetValue(handler);
                }

                Task task = Task.Run(() =>
                {
                    try
                    {
                        pointer = handler.Render();
                    }
                    catch(Exception e)
                    {
                        error = e.Message;
                    }
                });

                await task;

                if(!string.IsNullOrEmpty(error))
                {
                    Debug.LogError(string.Format("Could not render graph:\n{0}", error));
                    return;
                }

                substance.UpdateOutputTextures(pointer);
            }
        }*/


        /*public Task RenderAsync(int graphID = 0)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Task.Run(() =>
            {
                try
                {
                    var result = _runtimeHandler.Render(graphID);
                    _asyncRenderQueue.Enqueue(new AsyncRenderResult(result, graphID, tcs));
                }
                catch(Exception e)
                {
                    _asyncRenderQueue.Enqueue(new AsyncRenderResult(e));
                }
            });

            return tcs.Task;
        }*/

        #endregion

    }
}