using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class TernaryNode : AbstractNode
    {
        public override string Marker
        {
            get { return Convert.ToChar(0x70).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.TernaryNode;
            }
        }

        public override void Decode()
        {
            DecodeNode();
            DecodeNode();
            DecodeNode();
            DecodeId();
        }

        public override string PrettyPrint()
        {
            throw new Exception("Not defined.");
        }
    }
}
