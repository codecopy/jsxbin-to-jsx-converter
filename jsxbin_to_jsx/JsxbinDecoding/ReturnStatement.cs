using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ReturnStatement : AbstractNode, IStatement
    {
        LineInfo lineInfo;
        INode exprInfo;

        public int LineNumber
        {
            get
            {
                return lineInfo.LineNumber;
            }
        }

        public override string Marker
        {
            get { return Convert.ToChar(0x5A).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ReturnStatement;
            }
        }

        public override void Decode()
        {
            lineInfo = DecodeLineInfo();
            exprInfo = DecodeNode();
        }

        public override string PrettyPrint()
        {
            string returnExpr = exprInfo == null ? "" : " " + exprInfo.PrettyPrint();
            string label = lineInfo.CreateLabelStmt();
            string stmt = label + "return" + returnExpr;
            stmt += ";";
            return stmt;
        }
    }
}
