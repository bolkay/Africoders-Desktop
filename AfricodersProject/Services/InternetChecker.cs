using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace AfricodersProject.Services
{
    
    class InternetChecker
    {
        [DllImport("wininet.dll")]
        public static extern bool InternetGetConnectedState(out int desp, int rValue);
        
        public bool InternetAvailable()
        {
            int desp;
            return InternetGetConnectedState(out desp, 0);
        }
    }
}
