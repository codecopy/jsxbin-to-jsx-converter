using System;
using System.Text;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class TryStatement : AbstractNode
    {
        string expr;

        public override string Marker
        {
            get { return Convert.ToChar(0x67).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.TryStatement;
            }
        }

        public override void Decode()
        {
            var tryBlock = DecodeLineInfo();
            int length = DecodeLength();
            var finallyBlock = DecodeNode();
            StringBuilder b = new StringBuilder();
            string label = tryBlock.CreateLabelStmt();
            string body = tryBlock.CreateBody();
            b.AppendLine(label + "try {");
            b.AppendLine(body);
            int i = length;
            while (i > 0)
            {
                var arg = DecodeId();
                var exceptionFilter = DecodeNode();
                var catchBlock = DecodeNode();
                b.Append("} catch (" + arg);
                if (exceptionFilter != null)
                {
                    b.Append(" if " + exceptionFilter.PrettyPrint());
                }
                b.AppendLine(") {");
                b.Append(catchBlock == null ? string.Empty : catchBlock.PrettyPrint());
                b.AppendLine("");
                i--;
            }
            if (finallyBlock != null)
            {
                b.AppendLine("} finally {");
                b.Append(finallyBlock.PrettyPrint());
                b.AppendLine("");
            }
            b.Append("}");
            expr = b.ToString();
        }

        public override string PrettyPrint()
        {
            return expr;
        }
    }
}
