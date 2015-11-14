using System;
using System.Text;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class WithStatement : AbstractNode, IStatement
    {
        LineInfo bodyInfo;
        string objName;

        public int LineNumber
        {
            get
            {
                return bodyInfo.LineNumber;
            }
        }

        public override string Marker
        {
            get { return Convert.ToChar(0x6D).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.WithStatement;
            }
        }

        public override void Decode()
        {
            bodyInfo = DecodeLineInfo();
            objName = DecodeNode().PrettyPrint();
        }

        public override string PrettyPrint()
        {
            string label = bodyInfo.CreateLabelStmt();
            string body = bodyInfo.CreateBody();
            StringBuilder b = new StringBuilder();
            b.AppendLine(string.Format("{0}with ({1}) {{", label, objName));
            b.AppendLine(body);
            b.Append("}");
            return b.ToString();
        }
    }
}
