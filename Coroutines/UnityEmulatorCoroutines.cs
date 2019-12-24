using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace UnityEngine
{
    public class UnityEmulatorCoroutines
    {
        HashSet<Coroutine> coroutines;
        ConcurrentQueue<Coroutine> pendingAdd = new ConcurrentQueue<Coroutine>();
        ConcurrentQueue<Coroutine> pendingRemove = new ConcurrentQueue<Coroutine>();

        HashSet<Coroutine> removeRoutines = new HashSet<Coroutine>();
        MonoBehaviour behaviour;

        public UnityEmulatorCoroutines(MonoBehaviour behaviour)
        {
            coroutines = new HashSet<Coroutine>();
            this.behaviour = behaviour;
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            if (routine == null)
            {
                throw new ArgumentNullException("routine");
            }

            var c = new Coroutine(this.behaviour, routine);
            pendingAdd.Enqueue(c);
            return c;
        }

        public void StopCoroutine(Coroutine c)
        {
            if (c == null)
                return;

            pendingRemove.Enqueue(c);
        }

        public void ProceedCoroutines()
        {
            lock (coroutines)
            {
                removeRoutines.Clear();
                while (!pendingRemove.IsEmpty)
                {
                    if (pendingRemove.TryDequeue(out var cRemove))
                    {
                        removeRoutines.Add(cRemove);
                    }
                }

                while (!pendingAdd.IsEmpty)
                {
                    if (pendingAdd.TryDequeue(out var cAdd))
                    {
                        coroutines.Add(cAdd);
                    }
                }

                foreach (var c in coroutines)
                {
                    if (c == null || c.Rotine == null)
                    {
                        removeRoutines.Add(c);
                    }

                    if (!ProceedRoutine(c.Rotine))
                    {
                        removeRoutines.Add(c);
                    }
                }

                foreach (var r in removeRoutines)
                {
                    coroutines.Remove(r);
                }
            }
        }

        protected bool ProceedRoutine(IEnumerator enumerator)
        {
            try
            {
                var current = enumerator.Current;
                var cEnum = current as IEnumerator;

                /* If we step onto IEnumerator, then wait till it ends */
                if (cEnum != null)
                {
                    if (!ProceedRoutine(cEnum))
                    {
                        return enumerator.MoveNext();
                    }

                    return true;
                }

                return enumerator.MoveNext();
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Coroutine execution exception {0}", e);
                return false;
            }
        }

        public void StopAllCoroutines()
        {
            lock (coroutines)
            {
                foreach (var c in coroutines)
                {
                    StopCoroutine(c);
                }
            }
        }
    }
}