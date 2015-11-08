using System;
using System.Text;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class IfStatement : AbstractNode
    {
        LineInfo body;
        INode test;
        INode tail;

        public override string Marker
        {
            get { return Convert.ToChar(0x4F).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.IfStatement;
            }
        }

        public override void Decode()
        {
            body = DecodeLineInfo();
            test = DecodeNode();
            tail = DecodeNode();
        }

        public override string PrettyPrint()
        {
            string label = body.CreateLabelStmt();
            string bodyExpr = body.CreateBody();
            string testExpr = test.PrettyPrint();
            string elseStmt = tail == null ? null : tail.PrettyPrint();

            StringBuilder b = new StringBuilder();
            b.AppendLine(string.Format("{0}if ({1}) {{", label, testExpr));
            b.AppendLine(bodyExpr);
            b.Append("}");
            if (elseStmt != null)
            {
                b.AppendLine(" else {");
                b.AppendLine(elseStmt);
                b.Append("}");
            }
            return b.ToString();
        }
    }
}
