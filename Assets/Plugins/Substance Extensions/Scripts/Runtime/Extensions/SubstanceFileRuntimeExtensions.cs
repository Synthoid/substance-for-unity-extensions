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
    /// Contains extension methods for <see cref="SubstanceFileSO"/> runtime functionality.
    /// </summary>
    public static class SubstanceFileRuntimeExtensions
    {

        #region Rendering

        public static void RuntimeInitialize(this SubstanceGraphSO substance, SubstanceNativeGraph handler)
        {
            substance.RuntimeInitialize(handler, true);
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


        public static void Render(this SubstanceGraphSO substance)
        {
            using(SubstanceNativeGraph handler = Engine.OpenFile(substance.RawData.FileContent, substance.Index))
            {
                substance.RuntimeInitialize(handler, true);

                /*for(int i=0; i < substance.Graphs.Count; i++)
                {
                    substance.Graphs[i].RuntimeInitialize(handler);
                }*/

                IntPtr result = handler.Render();
                substance.UpdateOutputTextures(result);
            }
        }


        public static void Render(this SubstanceGraphSO substance, SubstanceNativeGraph handler)
        {
            IntPtr result = handler.Render();
            substance.UpdateOutputTextures(result);
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