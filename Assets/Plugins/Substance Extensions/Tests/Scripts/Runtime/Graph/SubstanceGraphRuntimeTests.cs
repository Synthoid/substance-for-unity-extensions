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
    public class SubstanceGraphRuntimeTests
    {
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
            confirmationView.ShowResults(testGroup.expectedVisual, testGraph.GetOutputMap(testGroup.resultOutput.Name), testGroup.notes, (status) =>
            {
                validationStatus = status;
                wait = false;
            });

            while(wait) yield return null;

            AssertValidationStatus(validationStatus);
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
    }
}
