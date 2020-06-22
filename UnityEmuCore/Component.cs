namespace UnityEngine
{
    public abstract class Component : Object
    {
        public GameObject gameObject { get; internal set; }
        
        public T GetComponent<T>() where T: MonoBehaviour => gameObject.GetComponent<T>();
        public T[] GetComponents<T>() where T: MonoBehaviour => gameObject.GetComponents<T>();
    }
}