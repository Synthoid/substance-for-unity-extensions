using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Adobe.Substance;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomEditor(typeof(SceneSubstanceGraphData), true)]
    public class SceneSubstanceGraphDataEditor : Editor
    {
        private const string kKeyMaterial = "sos-material-type";
        private const string kKeyScene = "sos-scene-type";
        private const string kKeyInactive = "sos-include-inactive";

        private static readonly string kSubstanceGraphSearchString = "t:" + typeof(SubstanceGraphSO).FullName;

        private static readonly GUIContent kSettingsLabel = new GUIContent("Grab Material Settings", "Controls for grabbing substance related materials in the scene.");
        private static readonly GUIContent kAllLabel = new GUIContent("All", "Populate graphs with all substances associated with materials in the scene.");
        private static readonly GUIContent kRuntimeLabel = new GUIContent("Runtime", "Populate graphs with all runtime only substances associated with materials in the scene.");
        private static readonly GUIContent kStaticLabel = new GUIContent("Static", "Populate graphs with all NON-runtime only substances associated with materials in the scene.");
        private static readonly GUIContent kSceneTypeLabel = new GUIContent("Scenes", "How substance references should be handled if multiple scenes are open.\n\n[All] - Reference substances in all open scenes.\n[Active Only] - Only reference substances in the currently active scene.");
        private static readonly GUIContent kIncludeInactiveLabel = new GUIContent("Include Inactive", "[True] - Reference materials on all scene objects, including disabled game objects.\n[False] - Only reference materials on enabled game objects in the scene.");
        private static readonly GUIContent kGrabMaterialsLabel = new GUIContent("Grab Material Susbtances", "Get references to substance graph assets associated with materials in the currently open scene(s) using the above settings.");

        private SerializedProperty m_Script = null;
        private SerializedProperty m_Graphs = null;

        private Vector2 scroll = Vector2.zero;
        private GUIContent[] MaterialTypeToolbarLabels = new GUIContent[0];

        private int materialTypeIndex = 0;
        private SceneGraphType materialType = SceneGraphType.All;
        private SceneReferenceType sceneType = SceneReferenceType.All;
        private bool includeInactive = true;
        private List<SubstanceGraphSO> cachedGraphs = new List<SubstanceGraphSO>();

        protected virtual void OnEnable()
        {
            m_Script = serializedObject.FindProperty("m_Script");
            m_Graphs = serializedObject.FindProperty("graphs");

            materialType = (SceneGraphType)PlayerPrefs.GetInt(kKeyMaterial, (int)SceneGraphType.All);
            sceneType = (SceneReferenceType)PlayerPrefs.GetInt(kKeyScene, (int)SceneReferenceType.All);
            includeInactive = PlayerPrefs.GetInt(kKeyInactive, 1) != 0;

            switch(materialType)
            {
                case SceneGraphType.All:
                    materialTypeIndex = 0;
                    break;
                case SceneGraphType.Runtime:
                    materialTypeIndex = 1;
                    break;
                case SceneGraphType.Static:
                    materialTypeIndex = 2;
                    break;
            }

            MaterialTypeToolbarLabels = new GUIContent[3]
            {
                kAllLabel,
                kRuntimeLabel,
                kStaticLabel
            };
        }


        public override void OnInspectorGUI()
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);

            using(new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUILayout.PropertyField(m_Script);
            }

            DrawGrabMaterialsSettings();

            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Graphs, true);

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.EndScrollView();
        }


        protected void DrawGrabMaterialsSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField(kSettingsLabel, EditorStyles.boldLabel);

            Rect buttonPos = EditorGUILayout.GetControlRect();

            using(EditorGUI.ChangeCheckScope scope = new EditorGUI.ChangeCheckScope())
            {
                materialTypeIndex = GUI.Toolbar(buttonPos, materialTypeIndex, MaterialTypeToolbarLabels);

                sceneType = (SceneReferenceType)EditorGUILayout.EnumPopup(kSceneTypeLabel, sceneType);

                includeInactive = EditorGUILayout.Toggle(kIncludeInactiveLabel, includeInactive);

                if(scope.changed)
                {
                    switch(materialTypeIndex)
                    {
                        case 0:
                            materialType = SceneGraphType.All;
                            break;
                        case 1:
                            materialType = SceneGraphType.Runtime;
                            break;
                        case 2:
                            materialType = SceneGraphType.Static;
                            break;
                    }

                    PlayerPrefs.SetInt(kKeyMaterial, (int)materialType);
                    PlayerPrefs.SetInt(kKeyScene, (int)sceneType);
                    PlayerPrefs.SetInt(kKeyInactive, includeInactive ? 1 : 0);
                }
            }

            EditorGUILayout.Space();

            if(GUILayout.Button(kGrabMaterialsLabel))
            {
                GrabSceneGraphs();
            }

            EditorGUILayout.EndVertical();
        }


        protected virtual void GrabSceneGraphs()
        {
            //Get scene graphs...
            SubstanceExtensionsEditorUtility.GetSceneGraphs(cachedGraphs, materialType, sceneType, includeInactive, true);

            //Update asset...
            Undo.RecordObject(target, "Update substance material references");

            ((SceneSubstanceGraphData)target).graphs = cachedGraphs.ToArray();

            serializedObject.ApplyModifiedProperties();

            cachedGraphs.Clear();
        }
    }
}