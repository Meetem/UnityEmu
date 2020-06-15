using System;

namespace UnityEngine
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class SerializeField : Attribute
    {
        public SerializeField() : base()
        {
            
        }
    }
}