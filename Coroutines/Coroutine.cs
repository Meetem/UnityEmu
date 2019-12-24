using System.Collections;

namespace UnityEngine
{
    public class Coroutine
    {
        public MonoBehaviour Behaviour { get; protected set; }
        public IEnumerator Rotine { get; protected set; }

        public Coroutine(MonoBehaviour behaviour, IEnumerator routine)
        {
            Behaviour = behaviour;
            Rotine = routine;
        }
    }
}