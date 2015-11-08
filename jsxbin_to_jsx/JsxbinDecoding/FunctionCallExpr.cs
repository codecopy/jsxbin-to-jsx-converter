using System;
using System.Collections.Generic;
using System.Linq;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class FunctionCallExpr : AbstractNode
    {
        INode functionName;
        INode argsInfo;
        bool isConstructorCall;

        public override string Marker
        {
            get { return Convert.ToChar(0x45).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.FunctionCallExpr;
            }
        }

        public override void Decode()
        {
            functionName = DecodeNode();
            argsInfo = DecodeNode();
            isConstructorCall = DecodeBool();
        }

        public override string PrettyPrint()
        {
            FunctionArguments args = (FunctionArguments)argsInfo;
            string ctor = isConstructorCall ? "new " : "";
            return string.Format("{0}{1}({2})", ctor, functionName.PrettyPrint(), args == null ? "" : string.Join(", ", args.Arguments.Select(a => a.PrettyPrint())));
        }
    }
}
