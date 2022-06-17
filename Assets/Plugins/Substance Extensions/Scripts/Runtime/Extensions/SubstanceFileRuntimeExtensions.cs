using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Adobe.Substance;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Contains extension methods for <see cref="SubstanceFileSO"/> runtime functionality.
    /// </summary>
    public static class SubstanceFileRuntimeExtensions
    {

        #region Rendering

        public static void RuntimeInitialize(this SubstanceFileSO substance, SubstanceNativeHandler handler)
        {
            for(int i = 0; i < substance.Instances.Count; i++)
            {
                substance.Instances[i].RuntimeInitialize(handler, true);
            }
        }


        public static void SetInputs(this SubstanceFileSO substance, IList<SubstanceParameterValue> values)
        {
            for(int i = 0; i < values.Count; i++)
            {
                values[i].SetValue(substance);
            }
        }


        public static void SetInputs(this SubstanceFileSO substance, SubstanceNativeHandler handler, IList<SubstanceParameterValue> values)
        {
            for(int i = 0; i < values.Count; i++)
            {
                values[i].SetValue(handler);
            }
        }


        public static void Render(this SubstanceFileSO substance, int graphId = 0)
        {
            using(SubstanceNativeHandler handler = Engine.OpenFile(substance.Instances[graphId].RawData.FileContent))
            {
                substance.Instances[graphId].RuntimeInitialize(handler, true);

                /*for(int i=0; i < substance.Graphs.Count; i++)
                {
                    substance.Graphs[i].RuntimeInitialize(handler);
                }*/

                IntPtr result = handler.Render(graphId);
                substance.Instances[graphId].UpdateOutputTextures(result);
            }
        }


        public static void Render(this SubstanceFileSO substance, SubstanceNativeHandler handler, int graphId = 0)
        {
            IntPtr result = handler.Render(graphId);
            substance.Instances[graphId].UpdateOutputTextures(result);
        }


        public static void SetInputAndRender(this SubstanceFileSO substance, SubstanceParameterValue value)
        {
            using(SubstanceNativeHandler handler = Engine.OpenFile(substance.Instances[value.GraphId].RawData.FileContent))
            {
                substance.Instances[value.GraphId].RuntimeInitialize(handler, true);
                /*for(int i=0; i < substance.Graphs.Count; i++)
                {
                    substance.Graphs[i].RuntimeInitialize(handler);
                }*/

                value.SetValue(handler);

                IntPtr result = handler.Render(value.GraphId);
                substance.Instances[value.GraphId].UpdateOutputTextures(result);
            }
        }


        public static void SetInputsAndRender(this SubstanceFileSO substance, IList<SubstanceParameterValue> values)
        {
            List<int> renderIndexes = new List<int>();

            for(int i = 0; i < values.Count; i++)
            {
                if(!renderIndexes.Contains(values[i].GraphId)) renderIndexes.Add(values[i].GraphId);

                //values[i].SetValue(handler);
            }

            for(int i = 0; i < renderIndexes.Count; i++)
            {
                using(SubstanceNativeHandler handler = Engine.OpenFile(substance.Instances[renderIndexes[i]].RawData.FileContent))
                {
                    for(int j = 0; j < values.Count; j++)
                    {
                        if(values[j].GraphId != renderIndexes[i]) continue;

                        substance.Instances[renderIndexes[i]].RuntimeInitialize(handler, true);

                        values[j].SetValue(handler);
                    }

                    IntPtr result = handler.Render(renderIndexes[i]);
                    substance.Instances[renderIndexes[i]].UpdateOutputTextures(result);
                }
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

                for(int i=0; i < renderIndexes.Count; i++)
                {
                    IntPtr result = handler.Render(renderIndexes[i]);
                    substance.Instances[renderIndexes[i]].UpdateOutputTextures(result);
                }
            }*/
        }


        public static void SetInputsAndRender(this SubstanceFileSO substance, IList<SubstanceParameterValue> values, SubstanceNativeHandler handler)
        {
            for(int i = 0; i < substance.Instances.Count; i++)
            {
                substance.Instances[i].RuntimeInitialize(handler, true); //TODO: This is rendering output textures... Need a custom one...
            }

            List<int> renderIndexes = new List<int>();

            for(int i = 0; i < values.Count; i++)
            {
                if(!renderIndexes.Contains(values[i].GraphId)) renderIndexes.Add(values[i].GraphId);

                values[i].SetValue(handler);
            }

            for(int i = 0; i < renderIndexes.Count; i++)
            {
                IntPtr result = handler.Render(renderIndexes[i]);
                substance.Instances[renderIndexes[i]].UpdateOutputTextures(result);
            }
        }


        public static async Task SetInputsAndRenderAsync(this SubstanceFileSO substance, IList<SubstanceParameterValue> values)
        {
            List<int> renderIndexes = new List<int>();

            for(int i = 0; i < values.Count; i++)
            {
                if(!renderIndexes.Contains(values[i].GraphId)) renderIndexes.Add(values[i].GraphId);

                //values[i].SetValue(handler);
            }

            Task[] tasks = new Task[renderIndexes.Count];
            IntPtr[] pointers = new IntPtr[tasks.Length];
            string[] errors = new string[tasks.Length];

            for(int i = 0; i < renderIndexes.Count; i++)
            {
                using(SubstanceNativeHandler handler = Engine.OpenFile(substance.Instances[renderIndexes[i]].RawData.FileContent))
                {
                    for(int j = 0; j < values.Count; j++)
                    {
                        if(values[j].GraphId != renderIndexes[i]) continue;

                        substance.Instances[renderIndexes[i]].RuntimeInitialize(handler, true);

                        values[j].SetValue(handler);
                    }

                    int index = i;

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

                    await Task.WhenAll(tasks);

                    //TODO: Need to get the result ptr and call substance.Graphs[renderIndexes[i]].UpdateOutputTextures(result);
                    for(int j = 0; j < tasks.Length; j++)
                    {
                        if(!string.IsNullOrEmpty(errors[j]))
                        {
                            Debug.LogError(string.Format("Could not render graph at index [{0}]:\n{1}", renderIndexes[j], errors[j]));
                            continue;
                        }

                        substance.Instances[renderIndexes[i]].UpdateOutputTextures(pointers[index]);
                    }
                }


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