using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class DebuggerStatement : AbstractNode, IStatement
    {
        LineInfo lineInfo;

        public int LineNumber
        {
            get
            {
                return lineInfo.LineNumber;
            }
        }

        public override string Marker
        {
            get
            {
                return Convert.ToChar(0x48).ToString();
            }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.DebuggerStatement;
            }
        }

        public override void Decode()
        {
            lineInfo = DecodeLineInfo();
        }

        public override string PrettyPrint()
        {
            string label = lineInfo.CreateLabelStmt();
            string name = "debugger";
            return label + name;
        }
    }
}
