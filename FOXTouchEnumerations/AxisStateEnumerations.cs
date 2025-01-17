using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouchEnumerations.AxisState
{
    public static class AxisStateEnumerations
    {
        public enum EAxisState : int
        {
            Undefined = 0,
            Iddle,
            Moving,
            Settling
        }
    }
}
