using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class IdNode : AbstractNode
    {
        string id;
        bool boolVal;

        public override string Marker
        {
            get { return Convert.ToChar(0x6A).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.IdNode;
            }
        }

        public override void Decode()
        {
            id = DecodeId();
            boolVal = DecodeBool();
        }

        public override string PrettyPrint()
        {
            return id;
        }
    }
}
