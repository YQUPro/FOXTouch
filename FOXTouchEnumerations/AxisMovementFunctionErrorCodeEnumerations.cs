using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouchEnumerations.ErrorCodes
{
    public static class AxisMovementFunctionErrorCodeEnumerations
    {
        public enum EMoveFunctionErrorCode : int
        {
            Undefined = -1,
            Success = 1,
            AlreadyRunning = 5,
            LimitSwitchActivated = 6,
            NoAxisEnabled = 7,
            Cancelled = 10,
            TimeoutOccurred = 90,
            MaximumRetriesExceeded = 91,
            EmergencyStopOccurred = 95,
            ExceptionOccurred = 99,
        }
    }
}
