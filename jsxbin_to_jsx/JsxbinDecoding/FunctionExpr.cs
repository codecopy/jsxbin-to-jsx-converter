using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class FunctionExpr : AbstractNode, IStatement
    {
        LineInfo lineInfo;
        INode expr;

        public int LineNumber
        {
            get
            {
                return lineInfo.LineNumber;
            }
        }

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
            string resultingExpr = expr.PrettyPrint();
            return resultingExpr;
        }
    }
}
