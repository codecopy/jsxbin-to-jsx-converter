using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ExprNode : AbstractNode, IStatement
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
            get { return Convert.ToChar(0x4A).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ExprNode;
            }
        }

        public INode Expr
        {
            get
            {
                return expr;
            }
        }

        public override void Decode()
        {
            lineInfo = DecodeLineInfo();
            expr = DecodeNode();
        }

        public override string PrettyPrint()
        {
            string labels = lineInfo.CreateLabelStmt();
            return labels + expr.PrettyPrint();
        }
    }
}
