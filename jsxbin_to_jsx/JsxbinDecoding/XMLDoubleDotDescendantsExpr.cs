using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class XMLDoubleDotDescendantsExpr : AbstractNode
    {
        Tuple<string, bool> descendants;
        INode obj;

        public override string Marker
        {
            get { return Convert.ToChar(0x71).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.XMLDoubleDotDescendantsExpr;
            }
        }

        public override void Decode()
        {
            descendants = DecodeReference();
            obj = DecodeNode();
            DecodeNode();
            DecodeNode();
        }

        public override string PrettyPrint()
        {
            return obj.PrettyPrint() + ".." + descendants.Item1;
        }
    }
}
