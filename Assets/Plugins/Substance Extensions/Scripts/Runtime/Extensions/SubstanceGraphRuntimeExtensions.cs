using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Adobe.Substance;
using Adobe.Substance.Runtime;
using System.Reflection;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Contains extension methods for <see cref="SubstanceGraphSO"/> runtime functionality.
    /// </summary>
    public static class SubstanceGraphRuntimeExtensions
    {

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
            if (nativeGraph == null) return;

            RuntimeNativeGraphs.Remove(graph.GetInstanceID());

            nativeGraph.Dispose();
        }

        /// <summary>
        /// Initialize the graph for runtime generation and create a cached native graph for use in render operations.
        /// Note: You should call <see cref="EndRuntimeEditing(SubstanceGraphSO, SubstanceNativeGraph)"/> when you are done rendering with the returned native graph..
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
            IntPtr result = nativeGraph.Render();

            graph.UpdateOutputTextures(result);
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

            graph.UpdateOutputTextures(renderTask.Result);
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
        public static async void RenderAndForgetAsync(this SubstanceGraphSO graph)
        {
            SubstanceNativeGraph nativeGraph = BeginRuntimeEditing(graph);

            await RenderAsync(graph, nativeGraph);

            EndRuntimeEditing(graph, nativeGraph);
        }


        public static void SetInputs(this SubstanceGraphSO substance, IList<SubstanceParameterValue> values)
        {
            for(int i = 0; i < values.Count; i++)
            {
                values[i].SetValue(substance);
            }
        }


        public static void SetInputs(this SubstanceGraphSO substance, SubstanceNativeGraph handler, IList<SubstanceParameterValue> values)
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
                /*for(int i=0; i < substance.Graphs.Count; i++)
                {
                    substance.Graphs[i].RuntimeInitialize(handler);
                }*/

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

                /*Task.Run(() =>
                        {
                            try
                            {
                                IntPtr result = handler.Render(renderIndexes[i]);
                                _asyncRenderQueue.Enqueue(new AsyncRenderResult(result, graphID, tcs));
                            }
                            catch(Exception e)
                            {
                                _asyncRenderQueue.Enqueue(new AsyncRenderResult(e));
                            }
                        });*/

                /*IntPtr result = handler.Render(renderIndexes[i]);
                substance.Graphs[renderIndexes[i]].UpdateOutputTextures(result);*/

                await task;

                //TODO: Need to get the result ptr and call substance.Graphs[renderIndexes[i]].UpdateOutputTextures(result);
                if(!string.IsNullOrEmpty(error))
                {
                    Debug.LogError(string.Format("Could not render graph:\n{0}", error));
                    return;
                }

                substance.UpdateOutputTextures(pointer);
            }

            /*using(SubstanceNativeHandler handler = Engine.OpenFile(substance.RawData.FileContent))
            {
                for(int i = 0; i < substance.Instances.Count; i++)
                {
                    substance.Instances[i].RuntimeInitialize(handler, true);
                }

                List<int> renderIndexes = new List<int>();

                for(int i = 0; i < values.Count; i++)
                {
                    if(!renderIndexes.Contains(values[i].GraphId)) renderIndexes.Add(values[i].GraphId);

                    values[i].SetValue(handler);
                }

                Task[] tasks = new Task[renderIndexes.Count];
                IntPtr[] pointers = new IntPtr[tasks.Length];
                string[] errors = new string[tasks.Length];

                for(int i = 0; i < renderIndexes.Count; i++)
                {
                    int index = i;
                    //TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

                    tasks[index] = Task.Run(() =>
                    {
                        try
                        {
                            pointers[index] = handler.Render(renderIndexes[index]);
                        }
                        catch(Exception e)
                        {
                            errors[index] = e.Message;
                        }
                    });

                    /*Task.Run(() =>
                    {
                        try
                        {
                            IntPtr result = handler.Render(renderIndexes[i]);
                            _asyncRenderQueue.Enqueue(new AsyncRenderResult(result, graphID, tcs));
                        }
                        catch(Exception e)
                        {
                            _asyncRenderQueue.Enqueue(new AsyncRenderResult(e));
                        }
                    });*/

            /*IntPtr result = handler.Render(renderIndexes[i]);
            substance.Graphs[renderIndexes[i]].UpdateOutputTextures(result);*/
            /*}

            await Task.WhenAll(tasks);

            //TODO: Need to get the result ptr and call substance.Graphs[renderIndexes[i]].UpdateOutputTextures(result);
            for(int i=0; i < tasks.Length; i++)
            {
                if(!string.IsNullOrEmpty(errors[i]))
                {
                    Debug.LogError(string.Format("Could not render graph at index [{0}]:\n{1}", renderIndexes[i], errors[i]));
                    continue;
                }

                substance.Instances[renderIndexes[i]].UpdateOutputTextures(pointers[i]);
            }
        }*/
        }


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