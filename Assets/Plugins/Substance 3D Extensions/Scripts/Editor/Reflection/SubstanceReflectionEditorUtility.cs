using UnityEngine;
using System;
using System.Reflection;
using Adobe.Substance;
using Adobe.SubstanceEditor;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Provides convenient access to internal substance editor functionality.
    /// </summary>
    public static class SubstanceReflectionEditorUtility
    {
        private static Assembly substanceEditorAssembly = null;
        //Types
        private static Type scriptableSingletonType = null;
        private static Type editorEngineType = null;
        private static Type importerEditorType = null;
        //Properties
        private static PropertyInfo instanceInfo = null;
        //Methods
        private static MethodInfo submitAsyncRenderWorkInfo = null;
        private static MethodInfo initializeInstanceInfo = null;
        private static MethodInfo releaseInstanceInfo = null;
        private static MethodInfo tryGetHandlerFromInstanceInfo = null;
        //Values
        private static object editorEngineInstance = null;

        public static Assembly EditorAssembly
        {
            get
            {
                if(substanceEditorAssembly == null)
                {
                    substanceEditorAssembly = typeof(NamingExtensions).Assembly;
                }

                return substanceEditorAssembly;
            }
        }

        public static Type ScriptableSingletonType
        {
            get
            {
                if(scriptableSingletonType == null)
                {
                    scriptableSingletonType = EditorEngineType.BaseType;
                }

                return scriptableSingletonType;
            }
        }

        public static Type EditorEngineType
        {
            get
            {
                if(editorEngineType == null)
                {
                    editorEngineType = EditorAssembly.GetType("Adobe.SubstanceEditor.SubstanceEditorEngine");
                }

                return editorEngineType;
            }
        }

        public static Type ImporterEditorType
        {
            get
            {
                if(importerEditorType == null)
                {
                    importerEditorType = EditorAssembly.GetType("Adobe.SubstanceEditor.Importer.SubstanceImporterEditor");
                }

                return importerEditorType;
            }
        }

        public static PropertyInfo InstanceInfo
        {
            get
            {
                if(instanceInfo == null)
                {
                    instanceInfo = ScriptableSingletonType.GetProperty("instance", BindingFlags.Public | BindingFlags.Static);
                }

                return instanceInfo;
            }
        }

        public static MethodInfo SubmitAsyncRenderWorkInfo
        {
            get
            {
                if(submitAsyncRenderWorkInfo == null)
                {
                    submitAsyncRenderWorkInfo = EditorEngineType.GetMethod("SubmitAsyncRenderWork", BindingFlags.Public | BindingFlags.Instance);
                }

                return submitAsyncRenderWorkInfo;
            }
        }

        public static MethodInfo InitializeInstanceInfo
        {
            get
            {
                if(initializeInstanceInfo == null)
                {
                    initializeInstanceInfo = EditorEngineType.GetMethod("InitializeInstance", BindingFlags.Public | BindingFlags.Instance);
                }

                return initializeInstanceInfo;
            }
        }

        public static MethodInfo ReleaseInstanceInfo
        {
            get
            {
                if(releaseInstanceInfo == null)
                {
                    releaseInstanceInfo = EditorEngineType.GetMethod("ReleaseInstance", BindingFlags.Public | BindingFlags.Instance);
                }

                return releaseInstanceInfo;
            }
        }

        public static MethodInfo TryGetHandlerFromInstanceInfo
        {
            get
            {
                if(tryGetHandlerFromInstanceInfo == null)
                {
                    tryGetHandlerFromInstanceInfo = EditorEngineType.GetMethod("TryGetHandlerFromInstance", BindingFlags.Public | BindingFlags.Instance);
                }

                return tryGetHandlerFromInstanceInfo;
            }
        }

        public static object EditorEngineInstance
        {
            get
            {
                if(editorEngineInstance == null)
                {
                    editorEngineInstance = InstanceInfo.GetValue(null);
                }

                return editorEngineInstance;
            }
        }

        /// <summary>
        /// Submit the given graph to be rendered by the editor engine.
        /// </summary>
        /// <param name="substanceArchive">Native graph to render.</param>
        /// <param name="instanceKey">Graph asset assoicated with the native graph.</param>
        /// <param name="forceRebuild">If true, will force rebuild the texture assets generated by the render. If false, will update existing textures.</param>
        public static void SubmitAsyncRenderWork(SubstanceNativeGraph substanceArchive, SubstanceGraphSO instanceKey, bool forceRebuild=false)
        {
            SubmitAsyncRenderWorkInfo.Invoke(EditorEngineInstance, new object[] { substanceArchive, instanceKey, forceRebuild });
        }

        /// <summary>
        /// Attempt to get a cached native graph for the given graph asset.
        /// </summary>
        /// <param name="substanceInstance">Graph to get a cached native graph for.</param>
        /// <param name="substanceHandler">Cached native graph associated with the given graph asset.</param>
        /// <returns>True if the given graph's cached native graph was found, or false otherwise.</returns>
        public static bool TryGetHandlerFromInstance(SubstanceGraphSO substanceInstance, out SubstanceNativeGraph substanceHandler)
        {
            object[] parameters = new object[] { substanceInstance, null };

            bool success = (bool)TryGetHandlerFromInstanceInfo.Invoke(EditorEngineInstance, parameters);

            substanceHandler = (SubstanceNativeGraph)parameters[1];

            return success;
        }

        /// <summary>
        /// Load a substance graph into the editor engine. The graph's native graph will be stored internally by the engine.
        /// </summary>
        /// <param name="substanceInstance">Graph to load a native graph for.</param>
        /// <param name="instancePath">Unique path for the asset.</param>
        public static void InitializeInstance(SubstanceGraphSO substanceInstance, string instancePath)
        {
            InitializeInstanceInfo.Invoke(EditorEngineInstance, new object[] { substanceInstance, instancePath });
        }

        /// <summary>
        /// Release the cached native graph associated with the given graph asset. If no cached graph exists, this does nothing.
        /// </summary>
        /// <param name="substanceInstance">Graph associated with the released native graph.</param>
        public static void ReleaseInstance(SubstanceGraphSO substanceInstance)
        {
            ReleaseInstanceInfo.Invoke(EditorEngineInstance, new object[] { substanceInstance });
        }
    }
}