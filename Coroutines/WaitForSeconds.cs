using System.Collections;

namespace UnityEngine
{
    public class WaitForSeconds : IEnumerator
    {
        private float releaseOn = 0.0f;
        private float duration = 0.0f;
        public WaitForSeconds(float seconds)
        {
            duration = seconds;
            releaseOn = Time.time + seconds;
        }

        public object Current{ get { return null; }}
        public bool MoveNext()
        {
            var t = Time.time;
            if (t > releaseOn) {
                return false;
            }

            return true;
        }

        public void Reset()
        {
            releaseOn = Time.time + duration;
        }
    }
}