using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOS.SubstanceExtensions.Tests
{
    /// <summary>
    /// Base class for components managing substance test scenes.
    /// </summary>
    public abstract class UnitTestSceneManager : MonoBehaviour
    {
        private static Dictionary<string, UnitTestSceneManager> SceneManagers = new Dictionary<string, UnitTestSceneManager>();

        public static T GetTestSceneManager<T>() where T : UnitTestSceneManager
        {
            string typeName = typeof(T).FullName;
            bool success = SceneManagers.TryGetValue(typeName, out UnitTestSceneManager manager);

            if(!success)
            {
                manager = FindObjectOfType<T>();

                SceneManagers.Add(typeName, manager);
            }

            return (T)manager;
        }


        protected virtual void OnDestroy()
        {
            SceneManagers.Remove(this.GetType().FullName);
        }


        protected virtual void Awake()
        {
            SceneManagers.TryAdd(this.GetType().FullName, this);
        }
    }
}