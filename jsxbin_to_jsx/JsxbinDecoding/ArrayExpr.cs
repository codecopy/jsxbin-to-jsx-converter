using System;
using System.Linq;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ArrayExpr : AbstractNode
    {
        INode arguments;

        public override string Marker
        {
            get { return Convert.ToChar(0x41).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ArrayExpr;
            }
        }

        public override void Decode()
        {
            arguments = DecodeNode();
        }

        public override string PrettyPrint()
        {
            FunctionArguments args = (FunctionArguments)arguments;
            if (args == null)
            {
                return "[]";
            }
            return "[" + string.Join(", ", args.Arguments.Select(a => a.PrettyPrint())) + "]";
        }
    }
}
