using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEditor.SceneManagement;
using SOS.SubstanceExtensions;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Tests
{
    /// <summary>
    /// Unit tests for SubstanceGraohSO related runtime extensions.
    /// </summary>
    public class SubstanceGraphRuntimeTests
    {
        //private const float kValidationTimeout = 30f;
        private const string kNoAssetWarning = "No test asset exists.";
        private const string kNoSceneWarning = "No test scene has been set up.";
        private const string kValidationInvalidMessage = "Validation test manually failed!.";
        private const string kValidationIgnoredMessage = "Ignored validation test.";
        private const string kValidationCheckBreak = "Breakout of validation tests. Remaining tests will be ignored...";

        private SubstanceGraphRuntimeTestAsset testAsset = null;
        private SubstanceRuntimeTestConfirmationView confirmationView = null;

        private Dictionary<int, SubstanceNativeGraph> nativeGraphs = new Dictionary<int, SubstanceNativeGraph>();

        private string breakoutMessage { get; set; } = "";
        private bool breakoutOfTests { get; set; } = false;
        private bool testSceneLoaded { get; set; } = false;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            EditorSceneManager.sceneLoaded += OnSceneLoaded;

            breakoutMessage = "";
            breakoutOfTests = false;

            testAsset = SubstanceGraphRuntimeTestAsset.Instance;

            if(testAsset == null)
            {
                breakoutMessage = kNoAssetWarning;
                breakoutOfTests = true;

                Debug.LogWarning("No test asset set up for substance graph runtime tests. Ignoring tests...");
                return;
            }

            SceneAsset testScene = testAsset.TestScene;

            if(testScene == null)
            {
                breakoutMessage = kNoSceneWarning;
                breakoutOfTests = true;
                return;
            }

            EditorSceneManager.LoadSceneAsyncInPlayMode(AssetDatabase.GetAssetPath(testScene), new LoadSceneParameters(LoadSceneMode.Single));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            EditorSceneManager.sceneLoaded -= OnSceneLoaded;

            //TODO: Iterate through test groups and dispose of native handles and reset parameter defaults?
            foreach(int id in nativeGraphs.Keys)
            {
                nativeGraphs[id].Dispose();
            }

            nativeGraphs.Clear();
        }


        private SubstanceNativeGraph GetNativeGraph(SubstanceGraphSO graph)
        {
            bool success = nativeGraphs.TryGetValue(graph.GetInstanceID(), out SubstanceNativeGraph nativeGraph);

            if(!success)
            {
                nativeGraph = Engine.OpenFile(graph.RawData.FileContent, graph.Index);
                graph.RuntimeInitialize(nativeGraph, true);

                nativeGraphs.Add(graph.GetInstanceID(), nativeGraph);
            }

            return nativeGraph;
        }


        private void AssertValidationStatus(int validationStatus)
        {
            switch(validationStatus)
            {
                case SubstanceRuntimeTestConfirmationView.kStatusValid:
                    Assert.Pass();
                    break;
                case SubstanceRuntimeTestConfirmationView.kStatusInvalid:
                    Assert.Fail(kValidationInvalidMessage);
                    break;
                case SubstanceRuntimeTestConfirmationView.kStatusIgnore:
                    Assert.Ignore(kValidationIgnoredMessage);
                    break;
                case SubstanceRuntimeTestConfirmationView.kStatusBreak:
                    Assert.Fail(kValidationCheckBreak);
                    breakoutMessage = kValidationCheckBreak;
                    breakoutOfTests = true;
                    break;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            confirmationView = SubstanceRuntimeTestConfirmationView.Instance;
            testSceneLoaded = true;
        }


        private IEnumerator TestGroupCoroutine(SubstanceGraphRuntimeTestGroup testGroup)
        {
            while(!testSceneLoaded) yield return null;

            confirmationView.ShowResults(testGroup.expectedVisual, testAsset.PlaceholderRenderTexture, "", null);

            SubstanceGraphSO testGraph = testGroup.substance;
            SubstanceNativeGraph nativeGraph = GetNativeGraph(testGraph);

            testGroup.SetDefaultValues(nativeGraph);
            testGroup.SetRenderValues(nativeGraph);

            IntPtr renderResult = nativeGraph.Render();

            while(nativeGraph.InRenderWork) yield return null;

            testGraph.UpdateOutputTextures(renderResult);

            bool wait = true;
            int validationStatus = 0;

            //Allow and wait for user input.
            confirmationView.ShowResults(testGroup.expectedVisual, testGraph.GetOutputTexture(testGroup.resultOutput.Name), testGroup.notes, (status) =>
            {
                validationStatus = status;
                wait = false;
            });

            while(wait) yield return null;

            AssertValidationStatus(validationStatus);
        }


        private IEnumerator TestGroupAsyncCoroutine(SubstanceGraphRuntimeTestGroup testGroup)
        {
            while (!testSceneLoaded) yield return null;

            confirmationView.ShowResults(testGroup.expectedVisual, testAsset.PlaceholderRenderTexture, "", null);

            SubstanceGraphSO testGraph = testGroup.substance;
            SubstanceNativeGraph nativeGraph = GetNativeGraph(testGraph);
            bool wait = true;

            TestGroupDefaultsAsync(testGroup, nativeGraph, () => { wait = false; });

            while (wait) yield return null;

            wait = true;

            TestGroupRendersAsync(testGroup, nativeGraph, () => { wait = false; });

            while (wait) yield return null;

            IntPtr renderResult = nativeGraph.Render();

            while(nativeGraph.InRenderWork) yield return null;

            testGraph.UpdateOutputTextures(renderResult);

            wait = true;
            int validationStatus = 0;

            //Allow and wait for user input.
            confirmationView.ShowResults(testGroup.expectedVisual, testGraph.GetOutputTexture(testGroup.resultOutput.Name), testGroup.notes, (status) =>
            {
                validationStatus = status;
                wait = false;
            });

            while(wait) yield return null;

            AssertValidationStatus(validationStatus);
        }


        private async void TestGroupDefaultsAsync(SubstanceGraphRuntimeTestGroup testGroup, SubstanceNativeGraph nativeGraph, System.Action callback)
        {
            await testGroup.SetDefaultValuesAsync(nativeGraph);

            callback.Invoke();
        }


        private async void TestGroupRendersAsync(SubstanceGraphRuntimeTestGroup testGroup, SubstanceNativeGraph nativeGraph, System.Action callback)
        {
            await testGroup.SetRenderValuesAsync(nativeGraph);

            callback.Invoke();
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a string parameter can be set and looks correct after rendering.")]
        public IEnumerator StringTest()
        {
            if(breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.StringTest;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a bool parameter can be set and looks correct after rendering.")]
        public IEnumerator BoolTest()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.BoolTest;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum parameter can be set and looks correct after rendering.")]
        public IEnumerator EnumTest()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.EnumTest;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int parameter can be set and looks correct after rendering.")]
        public IEnumerator IntTest()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.IntTest;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 parameter can be set and looks correct after rendering.")]
        public IEnumerator Int2Test()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.Int2Test;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 parameter can be set and looks correct after rendering.")]
        public IEnumerator Int3Test()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.Int3Test;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 parameter can be set and looks correct after rendering.")]
        public IEnumerator Int4Test()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.Int4Test;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float parameter can be set and looks correct after rendering.")]
        public IEnumerator FloatTest()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.FloatTest;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float2 parameter can be set and looks correct after rendering.")]
        public IEnumerator Float2Test()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.Float2Test;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float3 parameter can be set and looks correct after rendering.")]
        public IEnumerator Float3Test()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.Float3Test;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a color float4 parameter can be set and looks correct after rendering.")]
        public IEnumerator Float4ColorTest()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.Float4ColorTest;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a matrix float4 parameter can be set and looks correct after rendering.")]
        public IEnumerator Float4MatrixTest()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.Float4MatrixTest;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a CPU based read/write enabled texture parameter can be set and looks correct after rendering.")]
        public IEnumerator TextureCPUTest()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.TextureCPUTest;

            yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a GPU based texture parameter can be set and looks correct after rendering.")]
        public IEnumerator TextureGPUTest()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.TextureGPUTest;

            yield return TestGroupAsyncCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an $outputsize parameter can be set and looks correct after rendering.")]
        public IEnumerator OutputSizeTest()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            Assert.Ignore("Cannot resize output textures as of 3.4.0");
            //SubstanceGraphRuntimeTestGroup testGroup = testAsset.OutputSizeTest;

            //yield return TestGroupCoroutine(testGroup);
        }


        [UnityTest, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an $randomseed parameter can be set and looks correct after rendering.")]
        public IEnumerator RandomSeedTest()
        {
            if (breakoutOfTests)
            {
                Assert.Ignore(breakoutMessage);
                yield break;
            }

            SubstanceGraphRuntimeTestGroup testGroup = testAsset.RandomSeedTest;

            yield return TestGroupCoroutine(testGroup);
        }
    }
}
