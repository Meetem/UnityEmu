using System.Collections.Generic;
using BitMadness.Reflection;

namespace UnityEngine
{
    public class ComponentInitialize<T>
        where T: MonoBehaviour
    {
        public T Object { get; protected set; }
        public GetSetContainer<T> Container { get; protected set; }
        
        public ComponentInitialize(T obj)
        {
            Object = obj;
            Container = Object.GetFieldsContainer(true);
        }

        public void Set<TValue>(string propertyName, TValue value)
        {
            Container.GetField(propertyName).SetValue(value);
        }
    }
}