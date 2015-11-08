using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class UnknownNode3 : AbstractNode
    {
        public override string Marker
        {
            get { return Convert.ToChar(0x6F).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.UnknownNode3;
            }
        }

        public override void Decode()
        {
            ForEachChild(() =>
            {
                DecodeNode();
                DecodeNode();
            });
        }

        public override string PrettyPrint()
        {
            throw new Exception("Not defined");
        }
    }
}
