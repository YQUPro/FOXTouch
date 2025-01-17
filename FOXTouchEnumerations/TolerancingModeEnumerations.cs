using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouchEnumerations.Tolerancing
{
    public  static class TolerancingModeEnumerations
    {
        public enum ETolerancingMode : int
        {
            NoTolerancing = 0,
            DoubleTolerancing = 1,
            CenteredTolerancing = 2,
            SimpleUpperTolerancing = 3,
            SimpleLowerTolerancing = 4
        }
    }
}
