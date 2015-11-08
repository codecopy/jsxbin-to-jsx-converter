using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class UnaryExpr : AbstractNode
    {
        string op;
        string expr;

        public override string Marker
        {
            get { return Convert.ToChar(0x68).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.UnaryExpr;
            }
        }

        public override void Decode()
        {
            op = DecodeId();
            expr = DecodeNode().PrettyPrint();
        }

        public override string PrettyPrint()
        {
            return op + " (" + expr + ")";
        }
    }
}
