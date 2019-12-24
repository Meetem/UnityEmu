using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine
{
    public class UnityEmulator
    {
        public double Fps { get; set; } = 60.0;
        internal List<GameObject> gameObjects = new List<GameObject>();
        
        private double nextUpdateCall = 0.0;
        private static UnityEmulator _instance;
        
        public static UnityEmulator Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new UnityEmulator();

                return _instance;
            }
        }

        private Stopwatch clock;
        public UnityEmulator(double fps = 60.0)
        {
            Fps = fps;
            _instance = this;
            clock = new Stopwatch();
        }

        public void Start()
        {
            Console.WriteLine("Start unity");
            clock.Start();
        }

        public void Stop()
        {
            Console.WriteLine("Stop unity");
            clock.Stop();
        }

        public void Update()
        {
            var ct = clock.ElapsedTicks / (double) System.Diagnostics.Stopwatch.Frequency;
            if (Fps > 1e-6f)
            {
                if (nextUpdateCall > ct)
                    return;

                nextUpdateCall = ct + 1.0 / Fps;
            }

            var pt = Time.timeDouble;
            var dt = ct - pt;
            
            Time.timeDouble = ct;
            Time.deltaTimeDouble = dt;

            Time.time = (float) ct;
            Time.deltaTime = (float) dt;

            for (int i = 0; i < gameObjects.Count; i++)
            {
                var go = gameObjects[i];
                var components = go.components;

                for (int componentIdx = 0; componentIdx < components.Count; componentIdx++)
                {
                    var component = components[componentIdx];
                    if (component.enabled)
                        component.InternalPreUpdate();
                }
            }
            
            for (int i = 0; i < gameObjects.Count; i++)
            {
                var go = gameObjects[i];
                var components = go.components;

                for (int componentIdx = 0; componentIdx < components.Count; componentIdx++)
                {
                    var component = components[componentIdx];
                    if (component.enabled)
                        component.CallUpdate();
                }
            }
            
            for (int i = 0; i < gameObjects.Count; i++)
            {
                var go = gameObjects[i];
                var components = go.components;

                for (int componentIdx = 0; componentIdx < components.Count; componentIdx++)
                {
                    var component = components[componentIdx];
                    if (component.enabled)
                        component.CallLateUpdate();
                }
            }
        }

        public GameObject NewGameObject()
        {
            var go = new GameObject();
            gameObjects.Add(go);
            return go;
        }

        public T NewGameObject<T>(T original = null)
            where T: MonoBehaviour, new()
        {
            var go = new GameObject();
            gameObjects.Add(go);
            
            var component = go.AddComponent<T>();
            return component;
        }
    }
}