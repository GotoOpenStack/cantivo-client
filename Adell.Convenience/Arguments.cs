using System;

namespace Adell.Convenience
{
    public static class Arguments
    {
        public static string AddArgument(string argSwitch, object argValue)
        {
            return String.Format(" {0} \"{1}\"", argSwitch, argValue.ToString());
        }

        public static string AddSwitch(string argSwitch)
        {
            return  String.Format(" {0}", argSwitch);
        }
    }
}
