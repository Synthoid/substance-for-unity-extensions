using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Adobe.Substance;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomEditor(typeof(SceneSubstanceGraphData))]
    public class SceneSubstanceGraphDataEditor : Editor
    {
        [System.Flags]
        public enum SceneGraphType
        {
            None    = 0,
            Runtime = 1,
            Static  = 2,
            All     = ~0
        }

        public enum SceneReferenceType
        {
            All         = 0,
            ActiveOnly  = 1
        }

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

        private void OnEnable()
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


        private void DrawGrabMaterialsSettings()
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
                GrabSceneGraphs(materialType);
            }

            EditorGUILayout.EndVertical();
        }


        private void GrabSceneGraphs(SceneGraphType type)
        {
            EditorUtility.DisplayProgressBar("Grabbing Scene Substances...", "Grabbing Renderers...", 0.2f);

            Renderer[] renderers = FindObjectsOfType<Renderer>(includeInactive);
            
            //Cull non-active scene content if desired...
            if(sceneType == SceneReferenceType.ActiveOnly)
            {
                EditorUtility.DisplayProgressBar("Grabbing Scene Substances...", "Culling Renderers from extra scenes...", 0f);

                Scene activeScene = EditorSceneManager.GetActiveScene();
                GameObject[] rootObjects = activeScene.GetRootGameObjects();
                Transform[] rootTransforms = new Transform[rootObjects.Length];

                for(int i=0; i < rootTransforms.Length; i++)
                {
                    rootTransforms[i] = rootObjects[i].transform;
                }

                List<Renderer> newRenderers = new List<Renderer>();

                float rendererDelta = 1f / (float)renderers.Length;

                for(int i=0; i < renderers.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("Grabbing Scene Substances...", "Culling Renderers from extra scenes...", rendererDelta * i);

                    if(rootTransforms.Contains(renderers[i].transform.root))
                    {
                        newRenderers.Add(renderers[i]);
                    }
                }

                if(newRenderers.Count != renderers.Length) renderers = newRenderers.ToArray();
            }

            //Get materials...
            List<Material> materials = new List<Material>(renderers.Length);
            List<Material> sharedMaterials = new List<Material>();
            float delta = 1f / (float)renderers.Length;

            for(int i=0; i < renderers.Length; i++)
            {
                EditorUtility.DisplayProgressBar("Grabbing Scene Substances...", string.Format("Grabbing materials [{0}]...", renderers[i].name), delta * i);

                sharedMaterials.Clear();

                renderers[i].GetSharedMaterials(sharedMaterials);

                sharedMaterials.ForEach((m) =>
                {
                    //Only add materials not already included in the list...
                    if(!materials.Contains(m)) materials.Add(m);
                });
            }

            //Get Substances...
            List<SubstanceGraphSO> substances = new List<SubstanceGraphSO>();
            List<string> runtimeSubstances = new List<string>();
            List<string> staticSubstances = new List<string>();
            string[] searchFolders = new string[1];

            delta = 1f / (float)materials.Count;

            for(int i=0; i < materials.Count; i++)
            {
                EditorUtility.DisplayProgressBar("Grabbing Scene Substances...", string.Format("Checking substance materials [{0}]...", materials[i].name), delta * i);

                string folderPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(materials[i].GetInstanceID()));

                if(string.IsNullOrEmpty(folderPath)) continue; //Skip non-asset materials...
                if(folderPath == "Resources") continue; //Skip internal folders...

                searchFolders[0] = folderPath;

                string[] guids = AssetDatabase.FindAssets(kSubstanceGraphSearchString, searchFolders);

                for(int j=0; j < guids.Length; j++)
                {
                    SubstanceGraphSO graph = AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GUIDToAssetPath(guids[j]));

                    if(graph.OutputMaterial == materials[i])
                    {
                        //Only reference runtime/static substances when desired...
                        if((graph.IsRuntimeOnly && (type & SceneGraphType.Runtime) > 0))
                        {
                            runtimeSubstances.Add(graph.Name);

                            substances.Add(graph);
                        }

                        if((!graph.IsRuntimeOnly && (type & SceneGraphType.Static) > 0))
                        {
                            staticSubstances.Add(graph.Name);

                            substances.Add(graph);
                        }

                        break;
                    }
                }
            }

            //Update asset...
            Undo.RecordObject(target, "Update substance material references");

            ((SceneSubstanceGraphData)target).graphs = substances.ToArray();

            serializedObject.ApplyModifiedProperties();

            EditorUtility.ClearProgressBar();

            //Log referenced substances...
            StringBuilder referenceOutput = new StringBuilder(string.Format("Updated Scene Substance References!\nRuntime: {0}\n", runtimeSubstances.Count));

            runtimeSubstances.Sort();
            staticSubstances.Sort();

            runtimeSubstances.ForEach((rs) =>
            {
                referenceOutput.AppendLine(rs);
            });

            referenceOutput.AppendLine("");
            referenceOutput.AppendLine(string.Format("Static: {0}", staticSubstances.Count));

            staticSubstances.ForEach((rs) =>
            {
                referenceOutput.AppendLine(rs);
            });

            Debug.Log(referenceOutput.ToString());
        }
    }
}