using System;
using System.Diagnostics;


namespace Keramatian.Common
{
    public static class SystemTime
    {
        private static Func<DateTime> _now = () => DateTime.UtcNow;

        public static Func<DateTime> Now
        {
            [DebuggerStepThrough]
            get { return _now; }

            [DebuggerStepThrough]
            set { _now = value; }
        }
    }
}