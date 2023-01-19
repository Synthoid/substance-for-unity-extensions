using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.GraphView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SOS.SubstanceExtensions;
using Adobe.Substance;
using Adobe.Substance.Input;
using Adobe.SubstanceEditor;
using Adobe.SubstanceEditor.Importer;

using Object = UnityEngine.Object;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Contains utility methods and values used in editor operations.
    /// </summary>
    public static class SubstanceExtensionsEditorUtility
    {
        #region Utility

        private static readonly string kSubstanceGraphSearchString = "t:" + typeof(SubstanceGraphSO).FullName;

        public class Labels
        {
            /// <summary>
            /// Labels used when drawing various inspector controls.
            /// </summary>
            public class Controls
            {
                /// <summary>
                /// Default content used when drawing a linked button with a linked state.
                /// </summary>
                public static readonly GUIContent LinkedLabel = new GUIContent(EditorGUIUtility.IconContent(EditorGUIUtility.isProSkin ? "d_Linked" : "Linked").image);
                /// <summary>
                /// Default content used when drawing a linked button with an unlinked state.
                /// </summary>
                public static readonly GUIContent UnlinkedLabel = new GUIContent(EditorGUIUtility.IconContent(EditorGUIUtility.isProSkin ? "d_Unlinked" : "Linked").image);
            }

            /// <summary>
            /// Labels used when drawing matrix inspectors.
            /// </summary>
            public class Matrix
            {
                public static readonly GUIContent RotationLabel = new GUIContent("Rotation");
                public static readonly GUIContent RotationAngleLabel = new GUIContent("Angle", "Degrees (counterclockwise) to rotate the current matrix value by.");
                public static readonly GUIContent RotationApplyLabel = new GUIContent("Apply Angle", "Apply the given angle (counterclockwise, in degrees) to the current matrix value.");
                public static readonly GUIContent Rotation180Label = new GUIContent("180", "Rotate the current matrix 180 degrees.");
                public static readonly GUIContent Rotaiton90Label_CW = new GUIContent("90 cw", "Rotate the current matrix 90 degrees clockwise.");
                public static readonly GUIContent Rotation90Label_CCW = new GUIContent("90 ccw", "Rotate the current matrix 90 degrees counterclockwise.");
                public static readonly GUIContent StretchLabel = new GUIContent("Stretch");
                public static readonly GUIContent StretchWidthLabel = new GUIContent("Width");
                public static readonly GUIContent StretchHeightLabel = new GUIContent("Height");
                public static readonly GUIContent StretchApplyWidthLabel = new GUIContent("Apply Width", "Apply the given stretch width percentage to the current matrix value.");
                public static readonly GUIContent StretchApplyHeightLabel = new GUIContent("Apply Height", "Apply the given stretch height percentage to the current matrix value.");
                public static readonly GUIContent StretchPercentLabel = new GUIContent("%");
                //Misc
                public static readonly GUIContent MirrorHorizontalLabel = new GUIContent("Hori Mirror", "Mirror the current matrix value horizontally.");
                public static readonly GUIContent MirrorVerticalLabel = new GUIContent("Vert Mirror", "Mirror the current matrix value vertically.");
                public static readonly GUIContent Mul2Label = new GUIContent("x2", "Multiply the current matrix value by 2.");
                public static readonly GUIContent Div2Label = new GUIContent("/2", "Divide the current matrix value by 2.");
                //Swap
                public static readonly GUIContent SwapMatrixLabel = new GUIContent("Edit Matrix Values", "Edit the raw Float4 value of the matrix.");
                public static readonly GUIContent SwapRawLabel = new GUIContent("Back");
                //Raw Matrix
                public static GUIContent RawMatrixX1Label = new GUIContent("X1");
                public static GUIContent RawMatrixX2Label = new GUIContent("X2");
                public static GUIContent RawMatrixY1Label = new GUIContent("Y1");
                public static GUIContent RawMatrixY2Label = new GUIContent("Y2");
            }
        }

        public const float kSpaceHeight = 8f;

        /// <summary>
        /// Format string used in popup search windows of input values.
        /// </summary>
        public const string kInputSearchWindowTitle = "{0} Inputs";
        /// <summary>
        /// Format string used in popup search windows of output values.
        /// </summary>
        public const string kOutputSearchWindowTitle = "{0} Outputs";
        /// <summary>
        /// Default label used when displaying a value representing no substance.
        /// </summary>
        public const string kDefaultSubstanceName = "<No Substance>";

        /// <summary>
        /// Default label used when displaying a value representing nothing.
        /// </summary>
        public static readonly GUIContent kDefaultNoneLabel = new GUIContent("<None>", "");


        public static Tuple<GUIContent[], SubstanceParameterData[]> GetInputData(SubstanceGraphSO substance, SbsInputTypeFilter filter)
        {
            List<GUIContent> newLabels = new List<GUIContent>() { kDefaultNoneLabel };
            List<SubstanceParameterData> parameters = new List<SubstanceParameterData>() { new SubstanceParameterData() };

            if (substance != null)
            {
                List<ISubstanceInput> inputs = substance.Input;

                for (int j = 0; j < inputs.Count; j++)
                {
                    if (!inputs[j].IsValid) continue; //Skip invalid inputs
                    if ((filter & inputs[j].ValueType.ToFilter()) == 0) continue; //Skip inputs not included in the filter.

                    int index = j;

                    GUIContent label = new GUIContent(string.Format("{0}{1} ({2}) [{3}]",
                        string.IsNullOrEmpty(inputs[index].Description.GuiGroup) ? "" : string.Format("{0}/", inputs[index].Description.GuiGroup),
                        inputs[index].Description.Label,
                        inputs[index].Description.Identifier,
                        inputs[index].Description.Type),
                        inputs[index].Description.Identifier);

                    newLabels.Add(label);
                    parameters.Add(new SubstanceParameterData(inputs[index], substance.GUID));
                }
            }
            else
            {
                newLabels[0].text = "None <No Substance>";
            }

            return Tuple.Create(newLabels.ToArray(), parameters.ToArray());
        }

        #endregion

        #region SerializedProperty

        public static Vector4Int GetVector4IntValue(this SerializedProperty vectorProperty)
        {
            return new Vector4Int(vectorProperty.FindPropertyRelative("x").intValue,
                vectorProperty.FindPropertyRelative("y").intValue,
                vectorProperty.FindPropertyRelative("z").intValue,
                vectorProperty.FindPropertyRelative("w").intValue);
        }

        public static void SetVector4IntValue(this SerializedProperty vectorProperty, Vector4Int vector)
        {
            vectorProperty.FindPropertyRelative("x").intValue = vector.x;
            vectorProperty.FindPropertyRelative("y").intValue = vector.y;
            vectorProperty.FindPropertyRelative("z").intValue = vector.z;
            vectorProperty.FindPropertyRelative("w").intValue = vector.w;
        }

        /// <summary>
        /// Get the <see cref="SubstanceGraphSO"/> referenced by a <see cref="SubstanceParameter"/> field.
        /// </summary>
        /// <param name="property"><see cref="SubstanceParameter"/> property to get the target reference for.</param>
        public static SubstanceGraphSO GetGUIDReferenceSubstance(this SerializedProperty property)
        {
            string guid = property.FindPropertyRelative("guid").stringValue;

            if(string.IsNullOrEmpty(guid)) return null;

            return AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GUIDToAssetPath(guid));
        }

        #endregion

        #region Search

        public static void DrawPopupSearchWindow(Rect position, GUIContent label, int index, GUIContent[] labels, System.Action<int> selectionCallback, GUIContent title = default)
        {
            if(label != GUIContent.none)
            {
                EditorGUI.PrefixLabel(position, label);
                position.Set(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, position.height);
            }

            if(EditorGUI.DropdownButton(position, labels[index], FocusType.Keyboard))
            {
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)),
                    ScriptableObject.CreateInstance<LabelSearchProvider>().Initialize(labels, selectionCallback, title));
            }
        }


        public static void DrawPopupSearchWindow(Rect position, int index, GUIContent[] labels, System.Action<int> selectionCallback, GUIContent title=default)
        {
            if(EditorGUI.DropdownButton(position, labels[index], FocusType.Keyboard))
            {
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)),
                    ScriptableObject.CreateInstance<LabelSearchProvider>().Initialize(labels, selectionCallback, title));
            }
        }

        #endregion

        #region Scene Substances

        /// <summary>
        /// Obtain references to <see cref="SubstanceGraphSO"/> assets associated with materials in open scenes.
        /// This is done by finding all <see cref="Renderer"/> components in the scene(s) then evaluating their materials.
        /// </summary>
        /// <param name="graphTypes">Valid graphs to reference. Can be runtime only, static only, or both.</param>
        /// <param name="sceneType">How to handle referencing substances when multiple scenes are open.\n\n[All] - Reference substances in all open scenes.\n[Active Only] - Only reference substances in the currently active scene.</param>
        /// <param name="includeInactive">If true, inactive renderers will be included in the search. If false, only active renderers will be included.</param>
        /// <param name="logSubstances">If true, the console will log a list of found substance graphs.</param>
        /// <returns>Array of substance graph assets associated with scene materials.</returns>
        public static SubstanceGraphSO[] GetSceneGraphs(SceneGraphType graphTypes=SceneGraphType.All, SceneReferenceType sceneType=SceneReferenceType.All, bool includeInactive=true, bool logSubstances=false)
        {
            List<SubstanceGraphSO> substances = new List<SubstanceGraphSO>();

            GetSceneGraphs(substances, graphTypes, sceneType, includeInactive, logSubstances);

            return substances.ToArray();
        }

        /// <summary>
        /// Obtain references to <see cref="SubstanceGraphSO"/> assets associated with materials in open scenes.
        /// This is done by finding all <see cref="Renderer"/> components in the scene(s) then evaluating their materials.
        /// </summary>
        /// <param name="substances">List of substance graph assets to populate.</param>
        /// <param name="graphTypes">Valid graphs to reference. Can be runtime only, static only, or both.</param>
        /// <param name="sceneType">How to handle referencing substances when multiple scenes are open.\n\n[All] - Reference substances in all open scenes.\n[Active Only] - Only reference substances in the currently active scene.</param>
        /// <param name="includeInactive">If true, inactive renderers will be included in the search. If false, only active renderers will be included.</param>
        /// <param name="logSubstances">If true, the console will log a list of found substance graphs.</param>
        /// <returns>Number of valid substances found.</returns>
        public static int GetSceneGraphs(List<SubstanceGraphSO> substances, SceneGraphType graphTypes=SceneGraphType.All, SceneReferenceType sceneType=SceneReferenceType.All, bool includeInactive=true, bool logSubstances=false)
        {
            EditorUtility.DisplayProgressBar("Grabbing Scene Substances...", "Grabbing Renderers...", 0.2f);

            Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>(includeInactive);

            //Cull non-active scene content if desired...
            if(sceneType == SceneReferenceType.ActiveOnly)
            {
                EditorUtility.DisplayProgressBar("Grabbing Scene Substances...", "Culling Renderers from extra scenes...", 0f);

                Scene activeScene = EditorSceneManager.GetActiveScene();
                GameObject[] rootObjects = activeScene.GetRootGameObjects();
                Transform[] rootTransforms = new Transform[rootObjects.Length];

                for(int i = 0; i < rootTransforms.Length; i++)
                {
                    rootTransforms[i] = rootObjects[i].transform;
                }

                List<Renderer> newRenderers = new List<Renderer>();

                float rendererDelta = 1f / (float)renderers.Length;

                for(int i = 0; i < renderers.Length; i++)
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

            for(int i = 0; i < renderers.Length; i++)
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
            List<SubstanceGraphSO> runtimeSubstances = new List<SubstanceGraphSO>();
            List<SubstanceGraphSO> staticSubstances = new List<SubstanceGraphSO>();
            string[] searchFolders = new string[1];

            delta = 1f / (float)materials.Count;

            for(int i = 0; i < materials.Count; i++)
            {
                EditorUtility.DisplayProgressBar("Grabbing Scene Substances...", string.Format("Checking substance materials [{0}]...", materials[i].name), delta * i);

                string folderPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(materials[i].GetInstanceID()));

                if(string.IsNullOrEmpty(folderPath)) continue; //Skip non-asset materials...
                if(folderPath == "Resources") continue; //Skip internal folders...

                searchFolders[0] = folderPath;

                string[] guids = AssetDatabase.FindAssets(kSubstanceGraphSearchString, searchFolders);

                for(int j = 0; j < guids.Length; j++)
                {
                    SubstanceGraphSO graph = AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GUIDToAssetPath(guids[j]));

                    if(graph.OutputMaterial == materials[i])
                    {
                        //Only reference runtime/static substances when desired...
                        if((graph.IsRuntimeOnly && (graphTypes & SceneGraphType.Runtime) > 0))
                        {
                            runtimeSubstances.Add(graph);
                        }

                        if((!graph.IsRuntimeOnly && (graphTypes & SceneGraphType.Static) > 0))
                        {
                            staticSubstances.Add(graph);
                        }

                        break;
                    }
                }
            }

            EditorUtility.ClearProgressBar();

            //Sort substances alphabetically and generate a single list...
            runtimeSubstances.Sort((a, b) => { return a.Name.CompareTo(b.Name); });
            staticSubstances.Sort((a, b) => { return a.Name.CompareTo(b.Name); });

            substances.Clear();

            substances.AddRange(runtimeSubstances);
            substances.AddRange(staticSubstances);

            //Log referenced substances...
            if(!logSubstances) return substances.Count;

            StringBuilder referenceOutput = new StringBuilder(string.Format("Updated Scene Substance References: [{0}]\n<color=blue>Runtime</color>: [{1}]\n", substances.Count, runtimeSubstances.Count));

            runtimeSubstances.ForEach((rs) =>
            {
                referenceOutput.AppendLine(rs.Name);
            });

            referenceOutput.AppendLine("");
            referenceOutput.AppendLine(string.Format("<color=blue>Static</color>: [{0}]", staticSubstances.Count));

            staticSubstances.ForEach((rs) =>
            {
                referenceOutput.AppendLine(rs.Name);
            });

            Debug.Log(referenceOutput.ToString());

            return substances.Count;
        }

        #endregion

        #region Asset Interactions

        /// <summary>
        /// Destroy all loaded editors for <see cref="SubstanceGraphSO"/> and <see cref="SubstanceImporter"/> assets.
        /// </summary>
        /// <returns>True if any valid editors were loaded. False otherwise.</returns>
        public static bool CullSubstanceEditors()
        {
            //Destroy existing SubstanceGraphSOEditor objects...
            SubstanceGraphSOEditor[] graphEditors = Resources.FindObjectsOfTypeAll<SubstanceGraphSOEditor>();

            for(int i = 0; i < graphEditors.Length; i++)
            {
                Object.DestroyImmediate(graphEditors[i]);
            }

            //Destroy existing SubstanceImporterEditor objects...
            Object[] importerEditors = Resources.FindObjectsOfTypeAll(SubstanceReflectionEditorUtility.ImporterEditorType);

            for(int i = 0; i < importerEditors.Length; i++)
            {
                Object.DestroyImmediate(importerEditors[i]);
            }

            return graphEditors.Length > 0 || importerEditors.Length > 0;
        }


        public static bool TryUpdateSubstanceInputs(SubstanceFileSO substanceFile)
        {
            if(substanceFile == null) return false;

            //If RawData is null, this is likely a new susbtance file, so don't continue.
            if(substanceFile.Instances[0].RawData == null)
            {
                return true;
            }

            bool success = true;

            //Update all graph instances associated with the file...
            SubstanceNativeGraph nativeGraph = null;

            for(int i = 0; i < substanceFile.Instances.Count; i++)
            {
                nativeGraph = Engine.OpenFile(substanceFile.Instances[i].RawData.FileContent, substanceFile.Instances[i].Index);

                //Update actual inputs...
                List<ISubstanceInput> inputs = new List<ISubstanceInput>();

                int count = nativeGraph.GetInputs(inputs);

                /*StringBuilder expectingOutput = new StringBuilder($"Expecting: {count}\n");

                for(int j=0; j < count; j++)
                {
                    expectingOutput.AppendLine($"{j}: {inputs[j].Description.Identifier}");
                }

                Debug.Log(expectingOutput.ToString());*/

                SubstanceInputExtensions.UpdateInputList(substanceFile.Instances[i].Input, inputs);

                substanceFile.Instances[i].Input = new List<ISubstanceInput>(inputs);

                //Update preset string...
                substanceFile.Instances[i].RuntimeInitialize(nativeGraph, false);

                substanceFile.Instances[i].CurrentStatePreset = nativeGraph.CreatePresetFromCurrentState();

                EditorUtility.SetDirty(substanceFile.Instances[i]);

                //TODO: Tell the editor engine to render and update assets?
                //Force the editor engine to release the native graph associated with the graph, if it exists...
                SubstanceReflectionEditorUtility.ReleaseInstance(substanceFile.Instances[i]);

                nativeGraph.Dispose();
            }

            return success;
        }

        #endregion
    }
}