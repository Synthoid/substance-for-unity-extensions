using UnityEngine;
using UnityEngine.Events;
using Adobe.Substance;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Handles conveniently initializing and shutting down the runtime substance engine.
    /// </summary>
    public class SubstanceEngineManager : MonoBehaviour
    {
        /// <summary>
        /// How the runtime engine is managed.
        /// </summary>
        public enum EngineType
        {
            /// <summary>
            /// Preserve compatibility with Adobe's SubstanceRuntimeGraph script.
            /// </summary>
            PluginCompatible = 0,
            /// <summary>
            /// Allow for more control at the expense of dropping support for Adobe's SubstanceRuntimeGraph script.
            /// </summary>
            Advanced = 1
        }

        [SerializeField, Tooltip("If true, will initialize the substance engine on Start().")]
        protected bool initializeOnStart = true;
        [SerializeField, Tooltip("How the runtime engine is managed.\n\n[PluginCompatible] - Preserve compatibility with Adobe's SubstanceRuntimeGraph script.\n\n[Advanced] - Here be dragons! Allow for more control at the expense of dropping support for Adobe's SubstanceRuntimeGraph script.")]
        protected EngineType engineType = EngineType.PluginCompatible;
        [Space]
        [SerializeField, Tooltip("Fired after the runtime engine is initialized.")]
        protected UnityEvent m_OnEngineInitialized = new UnityEvent();
        [SerializeField, Tooltip("Fired BEFORE the runtime engine is shutdown.")]
        protected UnityEvent m_OnEnginePreShutdown = new UnityEvent();
        [SerializeField, Tooltip("Fired AFTER the runtime engine is shutdown.")]
        protected UnityEvent m_OnEnginePostShutdown = new UnityEvent();

        private static SubstanceEngineManager instance = null;

        public static SubstanceEngineManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<SubstanceEngineManager>();

                    if(instance == null)
                    {
                        instance = new GameObject("_SubstanceEngineManager").AddComponent<SubstanceEngineManager>();
                    }

                    if(instance.transform.parent == null)
                    {
                        DontDestroyOnLoad(instance.gameObject);
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Returns true if the runtime substance engine has been initialized.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                if(engineType == EngineType.PluginCompatible)
                {
                    return RuntimeEngineInstance != null;
                }

                return Engine.IsInitialized;
            }
        }

        /// <summary>
        /// How the runtime engine is managed.
        /// </summary>
        public EngineType RuntimeEngineType
        {
            get { return engineType; }
            set
            {
                if(IsInitialized)
                {
                    Debug.LogWarning("Cannot set engine type after initializing the engine! Shutdown the engine first, set engine type, then initialize the engine again.");
                    return;
                }

                engineType = value;
            }
        }

        /// <summary>
        /// Instance of a <see cref="SubstanceRuntime"/> object used for runtime engine initialization.
        /// Only used if <see cref="engineType"/> is set to PluginCompatible.
        /// </summary>
        public SubstanceRuntime RuntimeEngineInstance { get; protected set; }

        /// <summary>
        /// Fired after the runtime engine is initialized.
        /// </summary>
        public UnityEvent onEngineInitialized
        {
            get { return m_OnEngineInitialized; }
            set { m_OnEngineInitialized = value; }
        }

        /// <summary>
        /// Fired BEFORE the runtime engine is shutdown.
        /// </summary>
        public UnityEvent onEnginePreShutdown
        {
            get { return m_OnEnginePreShutdown; }
            set { m_OnEnginePreShutdown = value; }
        }

        /// <summary>
        /// Fired AFTER the runtime engine is shutdown.
        /// </summary>
        public UnityEvent onEnginePostShutdown
        {
            get { return m_OnEnginePostShutdown; }
            set { m_OnEnginePostShutdown = value; }
        }

        /// <summary>
        /// Initialize the substance engine. Note: This will do nothing if the engine is already initialized.
        /// </summary>
        public void InitializeEngine()
        {
            if(IsInitialized)
            {
                Debug.LogWarning("Runtime Substance Engine is already initialized!");
                return;
            }

            if(engineType == EngineType.PluginCompatible)
            {
                //Initialize plugin's runtime engine class...
                RuntimeEngineInstance = SubstanceRuntime.Instance;

                //Parent runtime engine class so it doesn't get destroyed when loading scenes (if applicable)...
                if(RuntimeEngineInstance.transform.parent == null)
                {
                    RuntimeEngineInstance.transform.SetParent(Instance.transform);
                }
            }
            else
            {
                //Initilaize the engine directly...
                Engine.Initialize(SubstanceExtensionsRuntimeUtility.GetPluginPath(), SubstanceExtensionsRuntimeUtility.GetEnginePath());
            }

            onEngineInitialized.Invoke();
        }

        /// <summary>
        /// Shutdown the substance engine.  Note: This will do nothing if the engine is not currently initialized.
        /// </summary>
        public void ShutdownEngine()
        {
            if(!IsInitialized) return;

            onEnginePreShutdown.Invoke();

#if UNITY_EDITOR
            //Editor only check to prevent shutting down the engine as the editor engine really doesn't like that...
            if(Application.isPlaying || !Application.isPlaying)
            {
                Debug.LogWarning("Cannot shut down the substance engine in the editor. Shutdown events will still be raised however, and this method will fully work as expected in builds.");
                onEnginePostShutdown.Invoke();
                return;
            }
#endif

            if(engineType == EngineType.PluginCompatible)
            {
                //Cannot shut down the engine when targeting plugin compatible mode as the SubstanceRuntime script doesn't support re-initializing the engine after it has been shut down.
                Debug.LogWarning("Cannot shut down the runtime engine when targeting PluginCompatible mode.");
                return;
            }

            Engine.Shutdown();

            onEnginePostShutdown.Invoke();
        }


        protected virtual void Start()
        {
            if(initializeOnStart) InitializeEngine();
        }
    }
}