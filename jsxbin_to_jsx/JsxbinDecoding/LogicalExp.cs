using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public sealed class LogicalExp : AbstractNode
    {
        LogicalExpInfo info;

        public override string Marker
        {
            get { return Convert.ToChar(0x55).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.LogicalExp;
            }
        }

        public override void Decode()
        {
            info = DecodeLogicalExp();
        }

        public override string PrettyPrint()
        {
            return string.Format("{0} {1} {2}", info.GetLeftExp(), info.OpName, info.GetRightExp());
        }
    }
}
