using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ValueNode : AbstractNode
    {
        string value;

        public override string Marker
        {
            get { return Convert.ToChar(0x46).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ValueNode;
            }
        }

        public override void Decode()
        {
            value = DecodeVariantCore();
        }

        public override string PrettyPrint()
        {
            return value;
        }
    }
}
