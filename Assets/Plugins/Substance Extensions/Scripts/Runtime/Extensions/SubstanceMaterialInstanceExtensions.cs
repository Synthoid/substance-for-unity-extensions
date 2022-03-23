using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    public static class SubstanceMaterialInstanceExtensions
    {
        public static List<ISubstanceInput> GetInputs(this SubstanceMaterialInstanceSO substance)
        {
            List<ISubstanceInput> inputs = new List<ISubstanceInput>();

            for(int i=0; i < substance.Graphs.Count; i++)
            {
                inputs.AddRange(substance.Graphs[i].Input);
            }

            return inputs;
        }


        public static ISubstanceInput GetInput(this SubstanceMaterialInstanceSO substance, string name, int subgraphIndex=0)
        {
            return GetInput(substance, GetInputIndex(substance, name, subgraphIndex), subgraphIndex);
        }


        public static ISubstanceInput GetInput(this SubstanceMaterialInstanceSO substance, int index, int subgraphIndex=0)
        {
            return substance.Graphs[subgraphIndex].Input[index];
        }


        public static T GetInput<T>(this SubstanceMaterialInstanceSO substance, string name, int subgraphIndex=0) where T : ISubstanceInput
        {
            return GetInput<T>(substance, GetInputIndex(substance, name, subgraphIndex), subgraphIndex);
        }


        public static T GetInput<T>(this SubstanceMaterialInstanceSO substance, int index, int subgraphIndex=0) where T : ISubstanceInput
        {
            return (T)substance.Graphs[subgraphIndex].Input[index];
        }


        public static Tuple<int, int> GetGraphAndInputIndexes(this SubstanceMaterialInstanceSO substance, string name)
        {
            for(int i=0; i < substance.Graphs.Count; i++)
            {
                for(int j=0; j < substance.Graphs[i].Input.Count; j++)
                {
                    if(substance.Graphs[i].Input[j].Description.Identifier == name)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }

            return Tuple.Create(-1, -1);
        }


        public static int GetInputIndex(this SubstanceMaterialInstanceSO substance, string name, int subgraphIndex=0)
        {
            for(int i=0; i < substance.Graphs[subgraphIndex].Input.Count; i++)
            {
                if(substance.Graphs[subgraphIndex].Input[i].Description.Identifier == name)
                {
                    return i;
                }
            }

            return -1;
        }

        public static bool GetBool(this SubstanceMaterialInstanceSO substance, string name, int subgraphIndex=0)
        {
            return GetBool(substance, GetInputIndex(substance, name, subgraphIndex), subgraphIndex);
        }


        public static bool GetBool(this SubstanceMaterialInstanceSO substance, int index, int subgraphIndex=0) //TODO: Should return true if the bool was found, and assign the value with an out parameter?
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index, subgraphIndex);

            if(input != null) return input.Data != 0;

            return false;
        }


        public static float GetFloat(this SubstanceMaterialInstanceSO substance, string name, int subgraphIndex=0)
        {
            return GetFloat(substance, GetInputIndex(substance, name, subgraphIndex), subgraphIndex);
        }


        public static float GetFloat(this SubstanceMaterialInstanceSO substance, int index, int subgraphIndex=0)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(substance, index, subgraphIndex);

            if(input != null) return input.Data;

            return 0f;
        }

        public static Vector2 GetFloat2(this SubstanceMaterialInstanceSO substance, string name, int subgraphIndex=0)
        {
            return GetFloat2(substance, GetInputIndex(substance, name, subgraphIndex), subgraphIndex);
        }


        public static Vector2 GetFloat2(this SubstanceMaterialInstanceSO substance, int index, int subgraphIndex=0)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(substance, index, subgraphIndex);

            if(input != null) return input.Data;

            return Vector2.zero;
        }


        public static void SetInputAndRender(this SubstanceMaterialInstanceSO substance, SubstanceParameterValue value)
        {
            using(SubstanceNativeHandler handler = Engine.OpenFile(substance.RawData.FileContent))
            {
                for(int i=0; i < substance.Graphs.Count; i++)
                {
                    substance.Graphs[i].RuntimeInitialize(handler);
                }

                value.SetValue(handler);

                IntPtr result = handler.Render(value.GraphId);
                substance.Graphs[value.GraphId].UpdateOutputTextures(result);
            }
        }


        public static void SetInputs(this SubstanceMaterialInstanceSO substance, IList<SubstanceParameterValue> values)
        {
            for(int i = 0; i < values.Count; i++)
            {
                values[i].SetValue(substance);
            }
        }


        public static void SetInputsAndRender(this SubstanceMaterialInstanceSO substance, IList<SubstanceParameterValue> values)
        {
            using(SubstanceNativeHandler handler = Engine.OpenFile(substance.RawData.FileContent))
            {
                for(int i = 0; i < substance.Graphs.Count; i++)
                {
                    substance.Graphs[i].RuntimeInitialize(handler);
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
                    substance.Graphs[renderIndexes[i]].UpdateOutputTextures(result);
                }
            }
        }


        public static async Task SetInputsAndRenderAsync(this SubstanceMaterialInstanceSO substance, IList<SubstanceParameterValue> values)
        {
            using(SubstanceNativeHandler handler = Engine.OpenFile(substance.RawData.FileContent))
            {
                for(int i = 0; i < substance.Graphs.Count; i++)
                {
                    substance.Graphs[i].RuntimeInitialize(handler);
                }

                List<int> renderIndexes = new List<int>();

                for(int i = 0; i < values.Count; i++)
                {
                    if(!renderIndexes.Contains(values[i].GraphId)) renderIndexes.Add(values[i].GraphId);

                    values[i].SetValue(handler);
                }

                Task[] tasks = new Task[renderIndexes.Count];

                for(int i = 0; i < renderIndexes.Count; i++)
                {
                    //TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

                    tasks[i] = Task.Run(() =>
                    {
                        try
                        {
                            IntPtr result = handler.Render(renderIndexes[i]);
                            return true;
                        }
                        catch(Exception e)
                        {
                            return false;
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
                }

                await Task.WhenAll(tasks);

                //TODO: Need to get the result ptr and call substance.Graphs[renderIndexes[i]].UpdateOutputTextures(result);
            }
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
    }
}