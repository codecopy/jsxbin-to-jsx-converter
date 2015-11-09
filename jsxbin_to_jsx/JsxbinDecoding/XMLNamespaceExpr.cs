using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class XMLNamespaceExpr : AbstractNode
    {
        Tuple<string, bool> namespaceObj;
        INode obj;
        string xmlId;

        public override string Marker
        {
            get { return Convert.ToChar(0x70).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.XMLNamespaceExpr;
            }
        }

        public override void Decode()
        {
            namespaceObj = DecodeReference();
            obj = DecodeNode();
            DecodeNode();
            DecodeNode();
            xmlId = DecodeId();
        }

        public override string PrettyPrint()
        {
            var isQualifiedOrSomething = namespaceObj.Item2;
            string ns = isQualifiedOrSomething ? "@" + namespaceObj.Item1 : namespaceObj.Item1;
            return obj.PrettyPrint() + "." + ns + "::" + xmlId;
        }
    }
}
