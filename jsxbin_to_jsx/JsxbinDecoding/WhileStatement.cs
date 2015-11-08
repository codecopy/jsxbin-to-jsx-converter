using System;
using System.Text;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class WhileStatement : AbstractNode
    {
        LineInfo bodyInfo;
        INode condInfo;

        public override string Marker
        {
            get { return Convert.ToChar(0x6C).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.WhileStatement;
            }
        }

        public override void Decode()
        {
            bodyInfo = DecodeBody();
            condInfo = DecodeNode();
        }

        public override string PrettyPrint()
        {
            var cond = condInfo == null ? "true" : condInfo.PrettyPrint();
            string label = bodyInfo.CreateLabelStmt();
            var body = bodyInfo.CreateBody();
            StringBuilder b = new StringBuilder();
            b.AppendLine(string.Format("{0}while ({1}) {{", label, cond));
            b.AppendLine(body);
            b.Append("}");
            return b.ToString();
        }
    }
}
