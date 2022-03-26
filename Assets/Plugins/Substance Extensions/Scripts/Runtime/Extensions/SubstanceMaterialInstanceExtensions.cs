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
        public const string PARAM_OUTPUT_SIZE = "$outputsize";
        public const string PARAM_RANDOM_SEED = "$randomseed";

        public static List<ISubstanceInput> GetInputs(this SubstanceMaterialInstanceSO substance)
        {
            List<ISubstanceInput> inputs = new List<ISubstanceInput>();

            for(int i=0; i < substance.Graphs.Count; i++)
            {
                inputs.AddRange(substance.Graphs[i].Input);
            }

            return inputs;
        }


        public static ISubstanceInput GetInput(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetInput(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static ISubstanceInput GetInput(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            return substance.Graphs[graphId].Input[index];
        }


        public static T GetInput<T>(this SubstanceMaterialInstanceSO substance, string name, int graphId=0) where T : ISubstanceInput
        {
            return GetInput<T>(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static T GetInput<T>(this SubstanceMaterialInstanceSO substance, int index, int graphId=0) where T : ISubstanceInput
        {
            return (T)substance.Graphs[graphId].Input[index];
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


        public static int GetInputIndex(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            for(int i=0; i < substance.Graphs[graphId].Input.Count; i++)
            {
                if(substance.Graphs[graphId].Input[i].Description.Identifier == name)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Initialize a substance for runtime and return a handler to begin editing it. Note, the returned handler must be disposed when you are done with it.
        /// </summary>
        /// <param name="substance">Substance to begin editing.</param>
        /// <param name="graphId"></param>
        /// <returns><see cref="SubstanceNativeHandler"/> controlling the substance editing.</returns>
        public static SubstanceNativeHandler BeginEditingSubstance(this SubstanceMaterialInstanceSO substance)
        {
            SubstanceNativeHandler handler = Engine.OpenFile(substance.RawData.FileContent);

            for(int i = 0; i < substance.Graphs.Count; i++)
            {
                substance.Graphs[i].RuntimeInitialize(handler);
            }

            return handler;
        }


        public static void EndEditingSubstance(this SubstanceMaterialInstanceSO substance, SubstanceNativeHandler handler)
        {
            handler.Dispose();
        }

        #region Get/Set Values

        #region Texture

        public static Texture2D GetTexture(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetTexture(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static Texture2D GetTexture(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            SubstanceInputTexture input = GetInput<SubstanceInputTexture>(substance, index, graphId);

            return input == null ? null : input.Data;
        }

        #endregion

        #region String

        public static string GetString(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetString(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static string GetString(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            SubstanceInputString input = GetInput<SubstanceInputString>(substance, index, graphId);

            return input == null ? string.Empty : input.Data;
        }

        #endregion

        #region Float

        public static float GetFloat(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetFloat(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static float GetFloat(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(substance, index, graphId);

            return input == null ? 0f : input.Data;
        }

        #endregion

        #region Float2

        public static Vector2 GetFloat2(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetFloat2(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static Vector2 GetFloat2(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(substance, index, graphId);

            return input == null ? Vector2.zero : input.Data;
        }

        #endregion

        #region Float3

        public static Vector3 GetFloat3(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetFloat3(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static Vector3 GetFloat3(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            SubstanceInputFloat3 input = GetInput<SubstanceInputFloat3>(substance, index, graphId);

            return input == null ? Vector3.zero : input.Data;
        }

        #endregion

        #region Float4

        public static Vector4 GetFloat4(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetFloat4(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static Vector4 GetFloat4(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            SubstanceInputFloat4 input = GetInput<SubstanceInputFloat4>(substance, index, graphId);

            return input == null ? Vector4.zero : input.Data;
        }

        #endregion

        #region Int

        public static int GetRandomSeed(this SubstanceMaterialInstanceSO substance, int graphId=0)
        {
            return GetInt(substance, PARAM_RANDOM_SEED, graphId);
        }


        public static bool GetBool(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetBool(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static bool GetBool(this SubstanceMaterialInstanceSO substance, int index, int graphId = 0) //TODO: Should return true if the bool was found, and assign the value with an out parameter?
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index, graphId);

            return input == null ? false : input.Data != 0;
        }


        public static int GetInt(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetInt(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static int GetInt(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(substance, index, graphId);

            return input == null ? 0 : input.Data;
        }

        #endregion

        #region Int2

        public static Vector2Int GetOutputSize(this SubstanceMaterialInstanceSO substance, int graphId=0)
        {
            return GetInt2(substance, PARAM_OUTPUT_SIZE, graphId);
        }


        public static void GetOutputSize(this SubstanceMaterialInstanceSO substance, out SbsOutputSize width, out SbsOutputSize height, int graphId=0)
        {
            Vector2Int value = GetOutputSize(substance, graphId);

            width = (SbsOutputSize)value.x;
            height = (SbsOutputSize)value.y;
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="size">Width and height of the size.</param>
        /// <param name="graphId">Index for the specific graph being targeted. Usually this can be left as 0.</param>
        public static void SetOutputSize(this SubstanceMaterialInstanceSO substance, SbsOutputSize size, int graphId=0)
        {
            SetOutputSize(substance, new Vector2Int((int)size, (int)size), graphId);
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="width">Width of the size value.</param>
        /// <param name="height">Height of the size value.</param>
        /// <param name="graphId">Index for the specific graph being targeted. Usually this can be left as 0.</param>
        public static void SetOutputSize(this SubstanceMaterialInstanceSO substance, SbsOutputSize width, SbsOutputSize height, int graphId=0)
        {
            SetOutputSize(substance, new Vector2Int((int)width, (int)height), graphId);
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="width">Width of the size value. Note that this value isn't the resolution in pixels, but specific int values associated with sizes. See <see cref="SbsOutputSize"/> for coresponding resolutions and ints.</param>
        /// <param name="height">Height of the size value. Note that this value isn't the resolution in pixels, but specific int values associated with sizes. See <see cref="SbsOutputSize"/> for coresponding resolutions and ints.</param>
        /// <param name="graphId">Index for the specific graph being targeted. Usually this can be left as 0.</param>
        public static void SetOutputSize(this SubstanceMaterialInstanceSO substance, int width, int height, int graphId=0)
        {
            SetOutputSize(substance, new Vector2Int(width, height), graphId);
        }

        /// <summary>
        /// Set the $outputsize parameter on the given substance.
        /// </summary>
        /// <param name="substance">Substance to set the output size on.</param>
        /// <param name="size">Width and height values of the size. Note that this value isn't the resolution in pixels, but specific int values associated with sizes. See <see cref="SbsOutputSize"/> for coresponding resolutions and ints.</param>
        /// <param name="graphId">Index for the specific graph being targeted. Usually this can be left as 0.</param>
        public static void SetOutputSize(this SubstanceMaterialInstanceSO substance, Vector2Int size, int graphId=0)
        {
            SetInt2(substance, PARAM_OUTPUT_SIZE, size, graphId);
        }


        public static Vector2Int GetInt2(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetInt2(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static Vector2Int GetInt2(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index, graphId);

            return input == null ? Vector2Int.zero : input.Data;
        }



        public static void SetInt2(this SubstanceMaterialInstanceSO substance, string name, Vector2Int value, int graphId=0)
        {
            SetInt2(substance, GetInputIndex(substance, name, graphId), value, graphId);
        }


        public static void SetInt2(this SubstanceMaterialInstanceSO substance, int index, Vector2Int value, int graphId=0)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index, graphId);

            if(input != null) input.Data = value;
        }

        #endregion

        #region Int3

        public static Vector3Int GetInt3(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetInt3(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static Vector3Int GetInt3(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            SubstanceInputInt3 input = GetInput<SubstanceInputInt3>(substance, index, graphId);

            return input == null ? Vector3Int.zero : input.Data;
        }

        #endregion

        #region Int4

        public static Vector4Int GetInt4(this SubstanceMaterialInstanceSO substance, string name, int graphId=0)
        {
            return GetInt4(substance, GetInputIndex(substance, name, graphId), graphId);
        }


        public static Vector4Int GetInt4(this SubstanceMaterialInstanceSO substance, int index, int graphId=0)
        {
            SubstanceInputInt4 input = GetInput<SubstanceInputInt4>(substance, index, graphId);

            return input == null ? Vector4Int.zero : new Vector4Int(input._Data0, input._Data1, input._Data2, input._Data3);
        }

        #endregion

        #endregion

        #region Rendering

        public static void RuntimeInitialize(this SubstanceMaterialInstanceSO substance, SubstanceNativeHandler handler)
        {
            for(int i=0; i < substance.Graphs.Count; i++)
            {
                substance.Graphs[i].RuntimeInitialize(handler);
            }
        }


        public static void SetInputs(this SubstanceMaterialInstanceSO substance, IList<SubstanceParameterValue> values)
        {
            for(int i=0; i < values.Count; i++)
            {
                values[i].SetValue(substance);
            }
        }


        public static void SetInputs(this SubstanceMaterialInstanceSO substance, SubstanceNativeHandler handler, IList<SubstanceParameterValue> values)
        {
            for(int i = 0; i < values.Count; i++)
            {
                values[i].SetValue(handler);
            }
        }


        public static void Render(this SubstanceMaterialInstanceSO substance, int graphId=0)
        {
            using(SubstanceNativeHandler handler = Engine.OpenFile(substance.RawData.FileContent))
            {
                for(int i=0; i < substance.Graphs.Count; i++)
                {
                    substance.Graphs[i].RuntimeInitialize(handler);
                }

                IntPtr result = handler.Render(graphId);
                substance.Graphs[graphId].UpdateOutputTextures(result);
            }
        }


        public static void Render(this SubstanceMaterialInstanceSO substance, SubstanceNativeHandler handler, int graphId=0)
        {
            IntPtr result = handler.Render(graphId);
            substance.Graphs[graphId].UpdateOutputTextures(result);
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

        #endregion
    }
}