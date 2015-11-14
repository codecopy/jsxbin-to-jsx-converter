using System;
using System.Linq;
using System.Text;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class FunctionDeclaration : AbstractNode, IStatement
    {
        LineInfo bodyInfo;
        FunctionSignature signature;
        int type;

        public int LineNumber
        {
            get
            {
                return bodyInfo.LineNumber;
            }
        }

        public override string Marker
        {
            get { return Convert.ToChar(0x4D).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.FunctionDeclaration;
            }
        }

        public override void Decode()
        {
            bodyInfo = DecodeLineInfo();
            signature = DecodeSignature();
            type = DecodeLength();
        }
        
        public override string PrettyPrint()
        {
            bool isHeader = signature.Header5 == 1;
            string body = bodyInfo.CreateBody();
            // Header seems to imply an automatically added "wrapper" function which is not really needed.
            if (isHeader)
            {
                return body;
            }
            // Filter out parameters that are actually local variables which for some reason count as parameters.
            var paramList = signature.Parameter.Where(p => p.Item2 > 536870000 && p.Item2 < 540000000).Select(p => p.Item1).ToList();
            var paramNames = string.Join(", ", paramList);
            StringBuilder b = new StringBuilder();
            b.AppendLine(string.Format("function {0}({1}) {{", signature.Name, paramNames));
            b.AppendLine(body);
            b.Append("}");
            return b.ToString();
        }
    }
}
