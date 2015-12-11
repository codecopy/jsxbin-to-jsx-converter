using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class IdNodeVersion1 : AbstractNode
    {
        string id;

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
                return 1.0;
            }
        }

        public override void Decode()
        {
            id = DecodeId();
            // Version 1.0 doesn't seem to have this field.
            // boolVal = DecodeBool();
        }

        public override string PrettyPrint()
        {
            return id;
        }
    }
}
