using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
    public class GameObject : Object
    {
        internal List<MonoBehaviour> components = new List<MonoBehaviour>();
        private Dictionary<string, List<MonoBehaviour>> componentMapping = new Dictionary<string, List<MonoBehaviour>>();

        internal GameObject()
        {
            
        }
        
        public static GameObject Instantiate(UnityEmulator onEmulator = null)
        {
            var emu = onEmulator ?? UnityEmulator.Instance;
            return emu.NewGameObject();
        }

        public static T Instantiate<T>(T original, UnityEmulator onEmulator = null)
            where T: MonoBehaviour, new()
        {
            var emu = onEmulator ?? UnityEmulator.Instance;
            return emu.NewGameObject<T>(original);
        }
        
        public T AddComponent<T>()
            where T : MonoBehaviour, new()
        {
            var newComponent = new T {gameObject = this};
            newComponent.CallAwake();
            newComponent.CallOnEnable();
            newComponent.CallStart();
            
            components.Add(newComponent);

            var typeName = typeof(T).FullName;
            if (!componentMapping.TryGetValue(typeName, out var listComponents))
            {
                listComponents = new List<MonoBehaviour>();
                componentMapping[typeName] = listComponents;
            }
            
            listComponents.Add(newComponent);
            return newComponent;
        }
        
        public T GetComponent<T>()
            where T : MonoBehaviour
        {
            var tName = typeof(T).FullName;
            if (!componentMapping.TryGetValue(tName, out var objects))
            {
                return null;
            }

            return objects[0] as T;
        }
        
        public T[] GetComponents<T>()
            where T : MonoBehaviour
        {
            var tName = typeof(T).FullName;
            if (!componentMapping.TryGetValue(tName, out var objects))
            {
                return null;
            }

            return objects.Select((x) => x as T).ToArray();
        }
    }
}