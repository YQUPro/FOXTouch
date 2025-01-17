using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FOXTouchLightingBoards
{
    public static class Helpers
    {
        public readonly static Regex expectedReplyPattern = new Regex(@"^&([0-9]|[A-F]){2}(\;([0-9]|[A-F]){3}){8}\#([0-9]|[A-F]){2}\r$", RegexOptions.Compiled | RegexOptions.IgnoreCase); //Alternative pattern to test : ^&[0-9a-fA-F]{2}(\;[0-9a-fA-F]{3}){8}\#[0-9a-fA-F]{2}
        public readonly static Regex AddressErrorReplyPattern = new Regex(@"^ADDRESS_ERROR$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public readonly static Regex ChecksumErrorReplyPattern = new Regex(@"^CHEKSUM_ERROR$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public readonly static Regex ParamErrorReplyPattern = new Regex(@"^PARAM_ERROR$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public readonly static Regex FormatErrorReplyPattern = new Regex(@"^FORMAT_ERROR$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}
