using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class BinaryNode : AbstractNode
    {
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
                return NodeType.BinaryNode;
            }
        }

        public override void Decode()
        {
            DecodeReference();
            DecodeNode();
            DecodeNode();
        }

        public override string PrettyPrint()
        {
            throw new Exception("Not defined.");
        }
    }
}
