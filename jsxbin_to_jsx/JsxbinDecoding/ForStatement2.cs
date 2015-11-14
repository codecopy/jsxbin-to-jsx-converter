using System;
using System.Text;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ForStatement2 : AbstractNode, IStatement
    {
        LineInfo bodyInfo;
        INode initInfo;
        INode testInfo;
        INode updateInfo;

        public int LineNumber
        {
            get
            {
                return bodyInfo.LineNumber;
            }
        }

        public override string Marker
        {
            get { return Convert.ToChar(0x4B).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ForStatement2;
            }
        }

        public override void Decode()
        {
            bodyInfo = DecodeBody();
            initInfo = DecodeNode();
            testInfo = DecodeNode();
            updateInfo = DecodeNode();
        }

        public override string PrettyPrint()
        {
            var label = bodyInfo.CreateLabelStmt();
            string body = bodyInfo.CreateBody();
            StringBuilder b = new StringBuilder();
            b.AppendLine(string.Format("{0}for ({1}; {2}; {3}) {{",
                label, 
                initInfo == null ? "" : initInfo.PrettyPrint(),
                testInfo == null ? "" : testInfo.PrettyPrint(),
                updateInfo == null ? "" : updateInfo.PrettyPrint()));
            b.AppendLine(body);
            b.Append("}");
            return b.ToString();
        }
    }
}
