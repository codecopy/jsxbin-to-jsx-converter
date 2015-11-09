using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class XMLAccessorExpr : AbstractNode
    {
        Tuple<string, bool> reference;
        INode obj;
        INode member;

        public override string Marker
        {
            get
            {
                return Convert.ToChar(0x72).ToString();
            }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.XMLAccessorExpr;
            }
        }

        public override void Decode()
        {
            reference = DecodeReference();
            obj = DecodeNode();
            member = DecodeNode();
        }

        public override string PrettyPrint()
        {
            return obj.PrettyPrint() + "." + member.PrettyPrint();
        }
    }
}
