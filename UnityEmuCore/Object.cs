namespace UnityEngine
{
    public abstract class Object
    {
        // ReSharper disable once InconsistentNaming
        public string name { get; set; }
        
        internal abstract void DestroyObject();
        public static void Destroy(Object obj)
        {
            if (obj == null)
                return;

            if (obj is MonoBehaviour component)
            {
                component.gameObject.RemoveComponent(component);
            }
            else
            {
                obj.DestroyObject();
            }
        }
    }
}