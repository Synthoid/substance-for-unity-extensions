using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Data asset containing an array of <see cref="SubstanceGraphSO"/> assets with materials referenced in a scene.
    /// </summary>
    [CreateAssetMenu(fileName="Scene Substance Graphs", menuName="SOS/Substance Extensions/Scene Substance Graph Data")]
    public class SceneSubstanceGraphData : ScriptableObject
    {
        [Tooltip("SubstanceGraphSO assets with materials in the scene.")]
        public SubstanceGraphSO[] graphs = new SubstanceGraphSO[0];
    }
}