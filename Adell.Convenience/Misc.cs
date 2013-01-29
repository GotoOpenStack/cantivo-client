using System;
namespace Adell.Convenience
{
    public static class Misc
    {


        public static bool IsWindowsOS
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                if ((p == 4) || (p == 6) || (p == 128))
                    return false;
                return true;
            }
        }
        public static bool IsInDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }
}


