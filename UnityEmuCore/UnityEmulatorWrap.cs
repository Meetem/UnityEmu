using System;
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
            try
            {
                Emulator.Start();

                while (!stop)
                {
                    Emulator.Update();
                }

                Emulator.Stop();
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Unhandled exception {0}", e);
                stop = true;
            }
        }

        public void Stop()
        {
            Console.WriteLine("Stop unity thread");
            stop = true;
            t.Join();
            t = null;
        }
    }
}