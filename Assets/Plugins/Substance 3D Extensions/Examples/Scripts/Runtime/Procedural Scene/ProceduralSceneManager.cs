using UnityEngine;
using System.Threading.Tasks;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example class showcasing how to render substances in a scene.
    /// </summary>
    public class ProceduralSceneManager : MonoBehaviour
    {
        [Tooltip("Substances to render when the scene starts.")]
        public SceneSubstanceGraphData sceneSubstances = null;
        [Tooltip("Components mapping substance textures onto external materials.")]
        public EvilOrbMapper[] evilOrbs = new EvilOrbMapper[0];

        private async Task RenderSceneSubstances()
        {
            Task[] renderTasks = new Task[sceneSubstances.graphs.Length];

            for(int i = 0; i < sceneSubstances.graphs.Length; i++)
            {
                renderTasks[i] = sceneSubstances.graphs[i].RenderAndForgetAsync();
            }

            await Task.WhenAll(renderTasks);

            for(int i=0; i < evilOrbs.Length; i++)
            {
                evilOrbs[i].RefreshMaterials();
            }
        }


        private void Start()
        {
            _ = RenderSceneSubstances();
        }
    }
}