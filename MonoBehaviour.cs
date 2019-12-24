using System.Collections;
using System.Globalization;
using System.Reflection;

namespace UnityEngine
{
    public class MonoBehaviour : Object
    {
        public GameObject gameObject { get; internal set; }
        private bool _enabled = true;
        private UnityEmulatorCoroutines coroutines;
        
        public bool enabled
        {
            get => _enabled;
            set => SetEnable(value);
        }
        
        protected MonoBehaviour()
        {
            coroutines = new UnityEmulatorCoroutines(this);
        }

        private struct UnityMethod
        {
            internal MethodInfo method;
            internal string methodName;
        }
        
        private static BindingFlags methodFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                         BindingFlags.InvokeMethod;

        private UnityMethod? awakeMethod;
        private UnityMethod? startMethod;
        private UnityMethod? onEnableMethod;
        private UnityMethod? onDisableMethod;
        private UnityMethod? onDestroyMethod;
        private UnityMethod? updateMethod;
        private UnityMethod? lateUpdateMethod;

        private void SetEnable(bool v)
        {
            var call = v != _enabled;
            _enabled = v;

            if (call)
            {
                if(v)
                    CallOnEnable();
                else
                    CallOnDisable();
            }
        }
        
        public Coroutine StartCoroutine(IEnumerator enumerator){
            return coroutines.StartCoroutine(enumerator);
        }

        public void StopCoroutine(Coroutine c){
            coroutines.StopCoroutine(c);
        }

        public void StopAllCoroutines(){
            coroutines.StopAllCoroutines();
        }

        private void CallMethod(ref UnityMethod? method, string methodName)
        {
            if (!method.HasValue)
            {
                var mt = this.GetType().GetMethod(methodName, methodFlags);
                method = new UnityMethod
                {
                    method = mt,
                    methodName = methodName
                };
            }
            
            if (method.Value.method == null)
                return;

            method.Value.method.Invoke(this, methodFlags, null, null, CultureInfo.CurrentCulture);
        }
        
        internal void CallAwake()
        {
            CallMethod(ref awakeMethod, "Awake");
        }

        private void CallCoroutines()
        {
            coroutines.ProceedCoroutines();
        }
        
        internal void InternalPreUpdate()
        {
            CallCoroutines();
        }
        
        internal void CallUpdate()
        {
            CallMethod(ref updateMethod, "Update");
        }
        
        internal void CallLateUpdate()
        {
            CallMethod(ref lateUpdateMethod, "LateUpdate");
        }

        internal void CallStart()
        {
            CallMethod(ref startMethod, "Start");
        }

        internal void CallOnEnable()
        {
            CallMethod(ref onEnableMethod, "OnEnable");
        }

        internal void CallOnDisable()
        {
            CallMethod(ref onDisableMethod, "OnDisable");
        }

        internal void CallOnDestroy()
        {
            coroutines.StopAllCoroutines();
            CallMethod(ref onDestroyMethod, "OnDestroy");
        }
    }
}