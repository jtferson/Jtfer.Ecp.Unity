using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Jtfer.Ecp.Unity
{
    public class UnitySingleton<T> : UnityScript
        where T : UnityScript
    {
        // Check to see if we're about to be destroyed.
        private static bool shuttingDown = false;
        private static object lockObject = new object();
        private static T _instance;

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (shuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                        "' already destroyed. Returning null.");
                    return null;
                }

                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        // Search for existing instance.
                        _instance = (T)FindObjectOfType(typeof(T));

                        // Create new instance if one doesn't already exist.
                        if (_instance == null)
                        {
                            // Need to create a new GameObject to attach the singleton to.
                            var singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";

                            // Make instance persistent.
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return _instance;
                }
            }
        }


        private void OnApplicationQuit()
        {
            shuttingDown = true;
        }

        private void OnDestroy()
        {
            shuttingDown = true;
        }
    }
}
