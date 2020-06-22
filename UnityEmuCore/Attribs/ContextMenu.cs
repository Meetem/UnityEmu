using System;

namespace UnityEngine
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class ContextMenuAttribute : Attribute
    {
        public string FunctionName { get; protected set; }
        public ContextMenuAttribute(string functionName) : base()
        {
            this.FunctionName = functionName;
        }
    }
}