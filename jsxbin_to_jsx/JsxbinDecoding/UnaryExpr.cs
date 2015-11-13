using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class UnaryExpr : AbstractNode
    {
        string op;
        INode expr;

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
            expr = DecodeNode();
        }

        public override string PrettyPrint()
        {
            bool requiresParens = expr.NodeType != NodeType.IdNode
                && expr.NodeType != NodeType.IdRefExpr
                && expr.NodeType != NodeType.FunctionCallExpr
                && expr.NodeType != NodeType.MemberExpr
                && expr.NodeType != NodeType.ArrayIndexingExpr;
            string exprParens = requiresParens ? "(" + expr.PrettyPrint() + ")" : expr.PrettyPrint();
            var unaryExpr = op + exprParens;
            return unaryExpr;
        }
    }
}
