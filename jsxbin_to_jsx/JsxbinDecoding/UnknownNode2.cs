using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class UnknownNode2 : AbstractNode
    {
        Tuple<string, INode> idAndNode;

        public override string Marker
        {
            get { return Convert.ToChar(0x73).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.UnknownNode2;
            }
        }

        public override void Decode()
        {
            idAndNode = DecodeIdAndNode();
        }

        public override string PrettyPrint()
        {
            throw new Exception("Not defined");
        }
    }
}
