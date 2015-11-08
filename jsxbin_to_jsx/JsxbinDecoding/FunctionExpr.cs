using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class FunctionExpr : AbstractNode
    {
        LineInfo lineInfo;
        INode expr;

        public override string Marker
        {
            get { return Convert.ToChar(0x4E).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.FunctionExpr;
            }
        }

        public override void Decode()
        {
            lineInfo = DecodeLineInfo();
            expr = DecodeNode();
        }

        public override string PrettyPrint()
        {
            return expr.PrettyPrint();
        }
    }
}
