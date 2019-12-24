using System.Threading;

namespace UnityEngine
{
    /// <summary>
    /// Creates and manages thread for unity emulator
    /// </summary>
    public class UnityEmulatorWrap
    {
        public UnityEmulator Emulator { get; protected set; }
        protected Thread t;
        protected bool stop = false;
        
        public UnityEmulatorWrap(double fps = 60.0)
        {
            Emulator =new UnityEmulator(fps);
        }

        public void Start()
        {
            if (t != null)
                Stop();
            
            stop = false;
            t = new Thread(UnityLoop);
            t.Start();
        }

        private void UnityLoop()
        {
            Emulator.Start();
            
            while (!stop)
            {
                Emulator.Update();
            }
            
            Emulator.Stop();
        }

        public void Stop()
        {
            stop = true;
            t.Join();
            t = null;
        }
    }
}