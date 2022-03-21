using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    public static class SubstanceGraphExtensions
    {
        public static List<ISubstanceInput> GetInputs(this SubstanceMaterialInstanceSO graph)
        {
            List<ISubstanceInput> inputs = new List<ISubstanceInput>();

            for(int i=0; i < graph.Graphs.Count; i++)
            {
                inputs.AddRange(graph.Graphs[i].Input);
            }

            return inputs;
        }


        public static ISubstanceInput GetInput(this SubstanceMaterialInstanceSO graph, string name, int subgraphIndex=0)
        {
            return GetInput(graph, GetInputIndex(graph, name, subgraphIndex), subgraphIndex);
        }


        public static ISubstanceInput GetInput(this SubstanceMaterialInstanceSO graph, int index, int subgraphIndex=0)
        {
            return graph.Graphs[subgraphIndex].Input[index];
        }


        public static T GetInput<T>(this SubstanceMaterialInstanceSO graph, string name, int subgraphIndex=0) where T : ISubstanceInput
        {
            return GetInput<T>(graph, GetInputIndex(graph, name, subgraphIndex), subgraphIndex);
        }


        public static T GetInput<T>(this SubstanceMaterialInstanceSO graph, int index, int subgraphIndex=0) where T : ISubstanceInput
        {
            return (T)graph.Graphs[subgraphIndex].Input[index];
        }


        public static Tuple<int, int> GetGraphAndInputIndexes(this SubstanceMaterialInstanceSO graph, string name)
        {
            for(int i=0; i < graph.Graphs.Count; i++)
            {
                for(int j=0; j < graph.Graphs[i].Input.Count; j++)
                {
                    if(graph.Graphs[i].Input[j].Description.Identifier == name)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }

            return Tuple.Create(-1, -1);
        }


        public static int GetInputIndex(this SubstanceMaterialInstanceSO graph, string name, int subgraphIndex=0)
        {
            for(int i=0; i < graph.Graphs[subgraphIndex].Input.Count; i++)
            {
                if(graph.Graphs[subgraphIndex].Input[i].Description.Identifier == name)
                {
                    return i;
                }
            }

            return -1;
        }

        public static bool GetBool(this SubstanceMaterialInstanceSO graph, string name, int subgraphIndex=0)
        {
            return GetBool(graph, GetInputIndex(graph, name, subgraphIndex), subgraphIndex);
        }


        public static bool GetBool(this SubstanceMaterialInstanceSO graph, int index, int subgraphIndex=0) //TODO: Should return true if the bool was found, and assign the value with an out parameter?
        {
            SubstanceInputInt input = GetInput<SubstanceInputInt>(graph, index, subgraphIndex);

            if(input != null) return input.Data != 0;

            return false;
        }


        public static float GetFloat(this SubstanceMaterialInstanceSO graph, string name, int subgraphIndex=0)
        {
            return GetFloat(graph, GetInputIndex(graph, name, subgraphIndex), subgraphIndex);
        }


        public static float GetFloat(this SubstanceMaterialInstanceSO graph, int index, int subgraphIndex=0)
        {
            SubstanceInputFloat input = GetInput<SubstanceInputFloat>(graph, index, subgraphIndex);

            if(input != null) return input.Data;

            return 0f;
        }

        public static Vector2 GetFloat2(this SubstanceMaterialInstanceSO graph, string name, int subgraphIndex=0)
        {
            return GetFloat2(graph, GetInputIndex(graph, name, subgraphIndex), subgraphIndex);
        }


        public static Vector2 GetFloat2(this SubstanceMaterialInstanceSO graph, int index, int subgraphIndex=0)
        {
            SubstanceInputFloat2 input = GetInput<SubstanceInputFloat2>(graph, index, subgraphIndex);

            if(input != null) return input.Data;

            return Vector2.zero;
        }


        public static void SetInputAndRender(this SubstanceMaterialInstanceSO graph, string name, SubstanceParameterValue value, int subgraphIndex=0)
        {
            SetInputAndRender(graph, GetInputIndex(graph, name, subgraphIndex), value, subgraphIndex);
        }


        public static void SetInputAndRender(this SubstanceMaterialInstanceSO graph, int index, SubstanceParameterValue value, int subgraphIndex=0)
        {
            using(SubstanceNativeHandler handler = Engine.OpenFile(graph.RawData.FileContent))
            {
                value.SetValue(handler);

                IntPtr result = handler.Render(subgraphIndex);
                graph.Graphs[subgraphIndex].UpdateOutputTextures(result);
            }
        }


        public static void SetInputsAndRender(this SubstanceMaterialInstanceSO graph, string name, IList<SubstanceParameterValue> values, int subgraphIndex = 0)
        {
            SetInputsAndRender(graph, GetInputIndex(graph, name, subgraphIndex), values, subgraphIndex);
        }


        public static void SetInputsAndRender(this SubstanceMaterialInstanceSO graph, int index, IList<SubstanceParameterValue> values, int subgraphIndex = 0)
        {
            using(SubstanceNativeHandler handler = Engine.OpenFile(graph.RawData.FileContent))
            {
                for(int i=0; i < values.Count; i++)
                {
                    values[i].SetValue(handler);
                }

                IntPtr result = handler.Render(subgraphIndex);
                graph.Graphs[subgraphIndex].UpdateOutputTextures(result);
            }
        }
    }
}