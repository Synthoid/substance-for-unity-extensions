using UnityEngine;
using UnityEditor;
using Adobe.Substance;
using Adobe.Substance.Input;
using Adobe.SubstanceEditor;

namespace SOS.SubstanceExtensionsEditor
{
    public static class SubstanceGraphEditorExtensions
    {
        public static SubstanceNativeGraph GetCachedEditorNativeGraph(SubstanceGraphSO substance)
        {
            SubstanceNativeGraph nativeGraph = Engine.OpenFile(substance.RawData.FileContent, substance.Index);

            substance.RuntimeInitialize(nativeGraph, false);

            return null;
        }


        /*[MenuItem("CONTEXT/SubstanceGraphSO/Log Presets")]
        private static void Test()
        {
            SubstanceGraphSO graph = (SubstanceGraphSO)Selection.activeObject;

            Debug.Log($"Preset String:\n{graph.CurrentStatePreset}");

            SubstanceNativeGraph nativeGraph = Engine.OpenFile(graph.RawData.FileContent, graph.Index);

            Debug.Log($"Preset (Pre-init):\n{nativeGraph.CreatePresetFromCurrentState()}");

            graph.RuntimeInitialize(nativeGraph, false);

            Debug.Log($"Preset (Post-init):\n{nativeGraph.CreatePresetFromCurrentState()}");

            nativeGraph.Dispose();
        }*/
    }
}