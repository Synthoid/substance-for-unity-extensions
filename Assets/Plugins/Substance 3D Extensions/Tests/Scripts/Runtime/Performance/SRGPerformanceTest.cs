using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions.Tests
{
    public class SRGPerformanceTest : MonoBehaviour
    {
        [SerializeField, Tooltip("If true, render SubstanceRuntimeGraph components using async methods. If false, render synchronously.")]
        private bool useAsync = true;
        [SerializeField, Tooltip("Number of times a substance is rendered before moving on to the next prefab.")]
        private int rendersPerPrefab = 3;
        [SerializeField, Tooltip("Delay between test spawns.")]
        private float testDelay = 1f;
        [SerializeField, Tooltip("Delay after spawning a performance game object and rendering it.")]
        private float spawnDelay = 1f;
        [SerializeField, Tooltip("Delay after rendering a substance before its performance game object is destroyed.")]
        private float destroyDelay = 1f;
        [SerializeField]
        private SRGPerformanceObject[] performancePrefabs = new SRGPerformanceObject[0];

        private CancellationTokenSource cancelTokenSource = null;

        private async Task PerformanceTaskAsync(CancellationToken cancelToken)
        {
            int index = 0;

            while(!cancelToken.IsCancellationRequested)
            {
                float t = 0f;

                while(t < testDelay)
                {
                    if(cancelToken.IsCancellationRequested) return;

                    t += Time.deltaTime;
                    await Task.Yield();
                }

                SRGPerformanceObject performanceObject = Instantiate(performancePrefabs[index % performancePrefabs.Length]);

                int count = 0;

                while(count < rendersPerPrefab)
                {
                    t = 0f;

                    while(t < spawnDelay)
                    {
                        if(cancelToken.IsCancellationRequested) return;

                        t += Time.deltaTime;
                        await Task.Yield();
                    }

                    if(useAsync)
                    {
                        await performanceObject.RenderAsync();

                        if(cancelToken.IsCancellationRequested) return;
                    }
                    else
                    {
                        performanceObject.Render();
                    }

                    count++;
                }

                t = 0f;

                while(t < destroyDelay)
                {
                    if(cancelToken.IsCancellationRequested) return;

                    t += Time.deltaTime;
                    await Task.Yield();
                }

                Destroy(performanceObject.gameObject);

                index++;
            }
        }


        private void OnDestroy()
        {
            cancelTokenSource.Cancel();
        }


        private void Start()
        {
            cancelTokenSource = new CancellationTokenSource();

            _ = PerformanceTaskAsync(cancelTokenSource.Token);
        }
    }
}