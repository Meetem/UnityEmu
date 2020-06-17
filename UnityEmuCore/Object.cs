namespace UnityEngine
{
    public abstract class Object
    {
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