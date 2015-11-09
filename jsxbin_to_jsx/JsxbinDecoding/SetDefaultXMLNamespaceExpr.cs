using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class SetDefaultXMLNamespaceExpr : AbstractNode
    {
        INode setDefaultNamespaceFunctionCall;

        public override string Marker
        {
            get { return Convert.ToChar(0x6B).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.SetDefaultXMLNamespaceExpr;
            }
        }

        public override void Decode()
        {
            setDefaultNamespaceFunctionCall = DecodeNode();
        }

        public override string PrettyPrint()
        {
            return setDefaultNamespaceFunctionCall.PrettyPrint();
        }
    }
}
