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
        [SerializeField, Tooltip("If true, will initialize the substance engine on Start().")]
        protected bool initializeOnStart = false;
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
        /// Instance of a <see cref="SubstanceRuntime"/> object used for runtime engine initialization.
        /// </summary>
        public SubstanceRuntime RuntimeEngineInstance { get; protected set; }

        /// <summary>
        /// Returns true if the runtime substance engine has been initialized.
        /// </summary>
        public bool IsInitialized { get { return RuntimeEngineInstance != null; } }

        public UnityEvent onEngineInitialized
        {
            get { return m_OnEngineInitialized; }
            set { m_OnEngineInitialized = value; }
        }

        public UnityEvent onEnginePreShutdown
        {
            get { return m_OnEnginePreShutdown; }
            set { m_OnEnginePreShutdown = value; }
        }

        public UnityEvent onEnginePostShutdown
        {
            get { return m_OnEnginePostShutdown; }
            set { m_OnEnginePostShutdown = value; }
        }

        public void InitializeEngine()
        {
            if(IsInitialized)
            {
                Debug.LogWarning("Runtime Substance Engine is already initialized!");
                return;
            }

            //Initialize plugin's runtime engine class...
            RuntimeEngineInstance = SubstanceRuntime.Instance;

            //Parent runtime engine class so it doesn't get destroyed when loading scenes (if applicable)...
            if(RuntimeEngineInstance.transform.parent == null)
            {
                RuntimeEngineInstance.transform.SetParent(Instance.transform);
            }

            onEngineInitialized.Invoke();
        }


        public void ShutdownEngine()
        {
            if(!IsInitialized) return;

            onEnginePreShutdown.Invoke();

            //Destroy the runtime instance game object...
            if(RuntimeEngineInstance.transform.parent == Instance.transform)
            {
                Destroy(RuntimeEngineInstance.gameObject);
                RuntimeEngineInstance = null;
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