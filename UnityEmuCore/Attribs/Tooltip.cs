using System;

namespace UnityEngine
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class TooltipAttribute : Attribute
    {
        public string Message { get; protected set; }
        public TooltipAttribute(string message) : base()
        {
            this.Message = message;
        }
    }
}