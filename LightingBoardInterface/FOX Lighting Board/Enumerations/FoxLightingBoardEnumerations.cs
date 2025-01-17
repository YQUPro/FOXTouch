using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouchLightingBoards
{
    public static class FoxLightingBoardEnumerations
    {
        public enum EUpdateLinesFunctionErrorCode : int
        {
            ImpossibleToProcess = -1,
            OK = 1,

            Reply_MismatchWithExpectedValues = 5,
            Protocol_AddressError = 10,
            Protocol_ChecksumError = 11,
            Protocol_ParamError = 12,
            Protocol_FormatError = 13,
        }
    }
}
