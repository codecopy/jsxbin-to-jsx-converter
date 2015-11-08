using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ThisExpr : AbstractNode
    {
        Tuple<string, bool> info;

        public override string Marker
        {
            get { return Convert.ToChar(0x65).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ThisExpr;
            }
        }

        public override void Decode()
        {
            info = DecodeReference();
        }

        public override string PrettyPrint()
        {
            return info.Item1;
        }
    }
}
