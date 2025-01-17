using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouchEnumerations.MeasurementResults
{
    public static class MeasurementResultsEnumerations
    {
        public enum EMeasurementResult : int
        {
            NotToleranced = 0,
            InTolerances = 1,
            OutOfTolerances = 2,
            NotMeasured = 3
        }
    }
}
