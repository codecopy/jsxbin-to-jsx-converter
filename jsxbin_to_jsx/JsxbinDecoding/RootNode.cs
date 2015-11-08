using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class RootNode : AbstractNode
    {
        INode child;
        public const string ROOT_MARKER = "ROOT";

        public override string Marker
        {
            get { return ROOT_MARKER; }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.Root;
            }
        }

        public override void Decode()
        {
            child = DecodeNode();
        }

        public override string PrettyPrint()
        {
            return child.PrettyPrint();
        }
    }
}
