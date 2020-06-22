using System;

namespace UnityEngine
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class SerializeFieldAttribute : Attribute
    {
        public SerializeFieldAttribute() : base()
        {
            
        }
    }
}