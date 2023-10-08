using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace SOS.SubstanceExtensions.Tests
{
    public class SubstancePerformanceTest : MonoBehaviour
    {
        [SerializeField, Tooltip("Number of times a substance is rendered before moving on to the next prefab.")]
        private int rendersPerPrefab = 3;
        [SerializeField, Tooltip("Delay between test spawns.")]
        private float testDelay = 1f;
        [SerializeField, Tooltip("Delay after spawning a performance game object and rendering it.")]
        private float spawnDelay = 1f;
        [SerializeField, Tooltip("Delay after rendering a substance before its performance game object is destroyed.")]
        private float destroyDelay = 1f;
        [SerializeField]
        private SubstancePerformanceObject[] performancePrefabs = new SubstancePerformanceObject[0];

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

                SubstancePerformanceObject performanceObject = Instantiate(performancePrefabs[index % performancePrefabs.Length]);

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

                    performanceObject.Render();

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