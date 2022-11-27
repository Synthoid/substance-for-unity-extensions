using UnityEngine;
using UnityEngine.Playables;
using Adobe.Substance;
using System;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions.Timeline
{
    /// <summary>
    /// Mixer behaviour for <see cref="SubstanceTrack"/> assets.
    /// </summary>
    public class SubstanceTrackMixerBehaviour : PlayableBehaviour
    {
        private SubstanceGraphSO drivenGraph = null;
        private SubstanceNativeGraph drivenNativeGraph = null;

        //TODO: CanRender property?
        public bool CanRender { get { return true; } }

        //Clip values, referenced by target input index
        private Dictionary<int, object> defaultPropertyValues = new Dictionary<int, object>();
        private Dictionary<int, float> weights = new Dictionary<int, float>();
        private Dictionary<int, float> floats = new Dictionary<int, float>();
        private Dictionary<int, Vector2> float2s = new Dictionary<int, Vector2>();
        private Dictionary<int, Vector3> float3s = new Dictionary<int, Vector3>();
        private Dictionary<int, Vector4> float4s = new Dictionary<int, Vector4>();
        private Dictionary<int, float> integers = new Dictionary<int, float>();
        private Dictionary<int, Vector2> integer2s = new Dictionary<int, Vector2>();
        private Dictionary<int, Vector3> integer3s = new Dictionary<int, Vector3>();
        private Dictionary<int, Vector4> integer4s = new Dictionary<int, Vector4>();
        private Dictionary<int, string> strings = new Dictionary<int, string>();
        private Dictionary<int, Texture> textures = new Dictionary<int, Texture>();
        private Dictionary<int, Texture> previousTextures = new Dictionary<int, Texture>();

        public override void OnGraphStop(Playable playable)
        {
            if(drivenNativeGraph == null) return;

            drivenGraph.EndRuntimeEditing(drivenNativeGraph);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            //TODO: Should get the playable graph and manually evaluate it when all textures have been set?
            //Move actual render code to a specific clip type? This clip can wait until all set value clips are ready in the graph on this frame?
            //playable.GetGraph().Evaluate()

            if(!Application.isPlaying) return;

            drivenGraph = (SubstanceGraphSO)playerData;

            InitializeDefaultValues(drivenGraph, playable);

            if(drivenNativeGraph == null) return;

            int inputCount = playable.GetInputCount();

            //Clear current values...
            weights.Clear();
            floats.Clear();
            float2s.Clear();
            float3s.Clear();
            float4s.Clear();
            integers.Clear();
            integer2s.Clear();
            integer3s.Clear();
            integer4s.Clear();
            strings.Clear();
            textures.Clear();

            for(int i=0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);

                if(inputWeight <= 0f) continue;

                Playable inputPlayable = playable.GetInput(i);
                Type playableType = inputPlayable.GetPlayableType();

                //Floats
                if(playableType == typeof(SetSubstanceInputFloatAsset))
                {
                    SetSubstanceInputFloatBehaviour behaviour = ((ScriptPlayable<SetSubstanceInputFloatBehaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(!weights.TryAdd(behaviour.Parameter.Index, inputWeight))
                    {
                        weights[behaviour.Parameter.Index] += inputWeight;
                    }

                    if(!floats.TryAdd(behaviour.Parameter.Index, behaviour.value * inputWeight))
                    {
                        floats[behaviour.Parameter.Index] += behaviour.value * inputWeight;
                    }

                    continue;
                }

                if(playableType == typeof(SetSubstanceInputFloat2Asset))
                {
                    SetSubstanceInputFloat2Behaviour behaviour = ((ScriptPlayable<SetSubstanceInputFloat2Behaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(!weights.TryAdd(behaviour.Parameter.Index, inputWeight))
                    {
                        weights[behaviour.Parameter.Index] += inputWeight;
                    }

                    if(!float2s.TryAdd(behaviour.Parameter.Index, behaviour.value * inputWeight))
                    {
                        float2s[behaviour.Parameter.Index] += behaviour.value * inputWeight;
                    }

                    continue;
                }

                if(playableType == typeof(SetSubstanceInputFloat3Asset))
                {
                    SetSubstanceInputFloat3Behaviour behaviour = ((ScriptPlayable<SetSubstanceInputFloat3Behaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(!weights.TryAdd(behaviour.Parameter.Index, inputWeight))
                    {
                        weights[behaviour.Parameter.Index] += inputWeight;
                    }

                    if(!float3s.TryAdd(behaviour.Parameter.Index, behaviour.value * inputWeight))
                    {
                        float3s[behaviour.Parameter.Index] += behaviour.value * inputWeight;
                    }

                    continue;
                }

                if(playableType == typeof(SetSubstanceInputFloat4Asset))
                {
                    SetSubstanceInputFloat4Behaviour behaviour = ((ScriptPlayable<SetSubstanceInputFloat4Behaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(!weights.TryAdd(behaviour.Parameter.Index, inputWeight))
                    {
                        weights[behaviour.Parameter.Index] += inputWeight;
                    }

                    if(!float4s.TryAdd(behaviour.Parameter.Index, behaviour.value * inputWeight))
                    {
                        float4s[behaviour.Parameter.Index] += behaviour.value * inputWeight;
                    }

                    continue;
                }

                if(playableType == typeof(SetSubstanceInputColorAsset))
                {
                    SetSubstanceInputColorBehaviour behaviour = ((ScriptPlayable<SetSubstanceInputColorBehaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(!weights.TryAdd(behaviour.Parameter.Index, inputWeight))
                    {
                        weights[behaviour.Parameter.Index] += inputWeight;
                    }

                    if(!float4s.TryAdd(behaviour.Parameter.Index, behaviour.value * inputWeight))
                    {
                        float4s[behaviour.Parameter.Index] += (Vector4)(behaviour.value * inputWeight);
                    }

                    continue;
                }

                //Integers
                if(playableType == typeof(SetSubstanceInputIntAsset))
                {
                    SetSubstanceInputIntBehaviour behaviour = ((ScriptPlayable<SetSubstanceInputIntBehaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(!weights.TryAdd(behaviour.Parameter.Index, inputWeight))
                    {
                        weights[behaviour.Parameter.Index] += inputWeight;
                    }

                    if(!integers.TryAdd(behaviour.Parameter.Index, behaviour.value * inputWeight))
                    {
                        integers[behaviour.Parameter.Index] += behaviour.value * inputWeight;
                    }

                    continue;
                }

                if(playableType == typeof(SetSubstanceInputInt2Asset))
                {
                    SetSubstanceInputInt2Behaviour behaviour = ((ScriptPlayable<SetSubstanceInputInt2Behaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(!weights.TryAdd(behaviour.Parameter.Index, inputWeight))
                    {
                        weights[behaviour.Parameter.Index] += inputWeight;
                    }

                    if(!integer2s.TryAdd(behaviour.Parameter.Index, behaviour.value * inputWeight))
                    {
                        integer2s[behaviour.Parameter.Index] += behaviour.value * inputWeight;
                    }

                    continue;
                }

                if(playableType == typeof(SetSubstanceInputInt3Asset))
                {
                    SetSubstanceInputInt3Behaviour behaviour = ((ScriptPlayable<SetSubstanceInputInt3Behaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(!weights.TryAdd(behaviour.Parameter.Index, inputWeight))
                    {
                        weights[behaviour.Parameter.Index] += inputWeight;
                    }

                    if(!integer3s.TryAdd(behaviour.Parameter.Index, behaviour.value * inputWeight))
                    {
                        integer3s[behaviour.Parameter.Index] += behaviour.value * inputWeight;
                    }

                    continue;
                }

                if(playableType == typeof(SetSubstanceInputInt4Asset))
                {
                    SetSubstanceInputInt4Behaviour behaviour = ((ScriptPlayable<SetSubstanceInputInt4Behaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(!weights.TryAdd(behaviour.Parameter.Index, inputWeight))
                    {
                        weights[behaviour.Parameter.Index] += inputWeight;
                    }

                    if(!integer4s.TryAdd(behaviour.Parameter.Index, behaviour.value * inputWeight))
                    {
                        integer4s[behaviour.Parameter.Index] += behaviour.value * inputWeight;
                    }

                    continue;
                }

                //Strings
                if(playableType == typeof(SetSubstanceInputStringAsset))
                {
                    SetSubstanceInputStringBehaviour behaviour = ((ScriptPlayable<SetSubstanceInputStringBehaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(strings.TryAdd(behaviour.Parameter.Index, behaviour.value))
                    {
                        weights[behaviour.Parameter.Index] = inputWeight;
                        strings[behaviour.Parameter.Index] = behaviour.value;
                    }
                    else if(inputWeight > weights[behaviour.Parameter.Index])
                    {
                        weights[behaviour.Parameter.Index] = inputWeight;
                        strings[behaviour.Parameter.Index] = behaviour.value;
                    }

                    continue;
                }

                //Textures
                if(playableType == typeof(SetSubstanceInputTextureAsset))
                {
                    SetSubstanceInputTextureBehaviour behaviour = ((ScriptPlayable<SetSubstanceInputTextureBehaviour>)inputPlayable).GetBehaviour();

                    if(string.IsNullOrEmpty(behaviour.Parameter.Name)) continue;

                    if(textures.TryAdd(behaviour.Parameter.Index, behaviour.value))
                    {
                        weights[behaviour.Parameter.Index] = inputWeight;
                        textures[behaviour.Parameter.Index] = behaviour.value;
                    }
                    else if(inputWeight > weights[behaviour.Parameter.Index])
                    {
                        weights[behaviour.Parameter.Index] = inputWeight;
                        textures[behaviour.Parameter.Index] = behaviour.value;
                    }

                    continue;
                }

                //Floats
                foreach(int s in floats.Keys)
                {
                    drivenNativeGraph.SetInputFloat(s, Mathf.Lerp(0f, floats[s], weights[s]));
                }

                foreach(int s in float2s.Keys)
                {
                    drivenNativeGraph.SetInputFloat2(s, Vector2.Lerp((Vector2)defaultPropertyValues[s], float2s[s], weights[s]));
                }

                foreach(int s in float3s.Keys)
                {
                    drivenNativeGraph.SetInputFloat3(s, Vector3.Lerp((Vector3)defaultPropertyValues[s], float3s[s], weights[s]));
                }

                foreach(int s in float4s.Keys)
                {
                    drivenNativeGraph.SetInputFloat4(s, Vector4.Lerp((Vector4)defaultPropertyValues[s], float4s[s], weights[s]));
                }

                //Integers
                foreach(int s in integers.Keys)
                {
                    drivenNativeGraph.SetInputInt(s, Mathf.RoundToInt(Mathf.Lerp(0f, integers[s], weights[s])));
                }

                foreach(int s in integer2s.Keys)
                {
                    drivenNativeGraph.SetInputInt2(s, Vector2Int.RoundToInt(Vector2.Lerp((Vector2)(Vector2Int)defaultPropertyValues[s], float2s[s], weights[s])));
                }

                foreach(int s in integer3s.Keys)
                {
                    drivenNativeGraph.SetInputInt3(s, Vector3Int.RoundToInt(Vector3.Lerp((Vector3)(Vector3Int)defaultPropertyValues[s], float3s[s], weights[s])));
                }

                foreach(int s in integer4s.Keys)
                {
                    drivenNativeGraph.SetInputInt4(s, Vector4Int.RoundToInt(Vector4.Lerp((Vector4)(Vector4Int)defaultPropertyValues[s], float4s[s], weights[s])));
                }

                //Non-lerpable values
                foreach(int s in strings.Keys)
                {
                    drivenNativeGraph.SetInputString(s, strings[s]);
                }
                
                foreach(int s in textures.Keys)
                {
                    previousTextures.TryGetValue(s, out Texture previousTexture);

                    if(previousTexture == textures[s]) continue;
                    else previousTextures[s] = textures[s];

                    _ = drivenNativeGraph.SetInputTextureGPUAsync(s, textures[s]); //Should cache previous value
                }
            }
        }


        private void InitializeDefaultValues(SubstanceGraphSO graph, Playable playable)
        {
            if(graph == null || drivenNativeGraph != null) return;

            drivenNativeGraph = graph.BeginRuntimeEditing();

            int count = graph.Input.Count;

            defaultPropertyValues.Clear();

            for(int i=0; i < count; i++)
            {
                int index = i;
                object value = graph.Input[index].GetValue();

                defaultPropertyValues.Add(index, value);

                if(value is Texture)
                {
                    previousTextures.TryAdd(index, (Texture)value);
                }
            }
        }
    }
}