using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class IdNodeVersion2 : AbstractNode
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

        public override double JsxbinVersion
        {
            get
            {
                return 2.0;
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
