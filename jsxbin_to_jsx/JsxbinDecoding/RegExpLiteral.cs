using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class RegExpLiteral : AbstractNode
    {
        string regex;
        string flags;

        public override string Marker
        {
            get { return Convert.ToChar(0x59).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.RegExpLiteral;
            }
        }

        public override void Decode()
        {
            regex = DecodeString();
            flags = DecodeString();
        }

        public override string PrettyPrint()
        {
            return string.Format("/{0}/{1}", regex, flags);
        }
    }
}
