using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public static class TypeConstants
    {
        public const string MEMBER_METHOD_CALL = "5";
        public const string MEMBER_INDEX_BY_NUMBER = "7";
        public const string MEMBER_INDEX_BY_NAME = "9";
        public const string MEMBER_PROPERTY_ASSIGNMENT = "999";

        public const string OP_ADD = "2";
        public const string OP_MUL = "3";
        public const string OP_DIV = "4";
        public const string OP_SUB = "7";
        public const string OP_MODULO = "8";

        public const string UPDATE_OP = "2";

        public const string LABEL_NO_LABEL = "45";

        public const int MARKER_HAS_NO_VARIANT = 0x6E;
    }
}
