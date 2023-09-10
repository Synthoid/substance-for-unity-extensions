using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using Adobe.Substance;
using Adobe.Substance.Input;
using Adobe.Substance.Input.Description;
using Adobe.SubstanceEditor;
using SOS.SubstanceExtensions;
using SOS.SubstanceExtensionsEditor;

public class SubstanceTestWindow : EditorWindow
{
    private const string kTargetParameter = "rough";
    private const string kParameterPath = "Input.Array.data[{0}]";

    private static readonly GUIContent kSubstanceLabel = new GUIContent("Substance");

    [SerializeField]
    private SubstanceGraphSO substance = null;

    private SerializedObject serializedObject = null;
    private SerializedProperty targetParameter = null;
    private SerializedProperty targetDataParameter = null;
    private SubstanceInputFloat testInput = null;
    private SubstanceGraphSO previousSubstance = null;
    private SubstanceNativeGraph nativeGraph = null;

    [MenuItem("Window/SOS/Tests/Substance Parameter Window")]
    private static void OpenWindow()
    {
        GetWindow<SubstanceTestWindow>(false, "Test Params", true).Show();
    }


    private void OnDestroy()
    {
        ReleaseSubstanceData();
    }


    private void OnGUI()
    {
        if(!Engine.IsInitialized)
        {
            EditorGUILayout.HelpBox("Waiting for engine to initialize...", MessageType.Warning);
            return;
        }

        //Can't do this in OnEnable() since the engine will not have initialized yet.
        if(substance != null && targetParameter == null) RefreshSubstanceData();

        using(EditorGUI.ChangeCheckScope scope = new EditorGUI.ChangeCheckScope())
        {
            substance = (SubstanceGraphSO)EditorGUILayout.ObjectField(kSubstanceLabel, substance, typeof(SubstanceGraphSO), false);

            if(scope.changed)
            {
                if(substance == null)
                {
                    ReleaseSubstanceData();
                }
                else if(substance != previousSubstance)
                {
                    Debug.Log("New sbs is different!");

                    ReleaseSubstanceData();
                    RefreshSubstanceData();
                }
            }
        }

        if(substance == null || targetParameter == null) return;

        serializedObject.Update();

        using(EditorGUI.ChangeCheckScope scope = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.PropertyField(targetDataParameter, true);

            if(scope.changed)
            {
                SubstanceReflectionEditorUtility.SubmitAsyncRenderWork(nativeGraph, substance, true);
            }
        }

        serializedObject.ApplyModifiedProperties();

        if(nativeGraph == null) return;

        using(new EditorGUI.DisabledGroupScope(true))
        {
            EditorGUILayout.Toggle(new GUIContent("Rendering"), nativeGraph.InRenderWork);
        }
    }


    private void RefreshSubstanceData()
    {
        ReleaseSubstanceData();

        previousSubstance = substance;

        serializedObject = new SerializedObject(substance);

        int index = substance.GetInputIndex(kTargetParameter);

        targetParameter = serializedObject.FindProperty(string.Format(kParameterPath, index));
        targetDataParameter = targetParameter.FindPropertyRelative("Data");

        testInput = substance.GetInput<SubstanceInputFloat>(kTargetParameter);

        bool success = SubstanceReflectionEditorUtility.TryGetHandlerFromInstance(substance, out nativeGraph);

        if(!success)
        {
            SubstanceReflectionEditorUtility.InitializeInstance(substance, substance.AssetPath, out _);
            //InitializeInstanceInfo.Invoke(EditorEngineInstance, new object[] { substance, substance.AssetPath });

            //success = (bool)TryGetHandlerFromInstanceInfo.Invoke(EditorEngineInstance, parameters);
            success = SubstanceReflectionEditorUtility.TryGetHandlerFromInstance(substance, out nativeGraph);
        }
    }


    private void ReleaseSubstanceData()
    {
        if(nativeGraph != null)
        {
            SubstanceReflectionEditorUtility.ReleaseInstance(previousSubstance);
            //ReleaseInstanceInfo.Invoke(EditorEngineInstance, new object[] { previousSubstance });

            nativeGraph = null;
        }

        if(serializedObject != null)
        {
            serializedObject.Dispose();
            serializedObject = null;
        }

        testInput = null;
        previousSubstance = null;
    }


    private void Temp()
    {
        //nativeGraph.inpu
    }
}