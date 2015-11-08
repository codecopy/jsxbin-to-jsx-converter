using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class DeleteExpr : AbstractNode
    {
        Tuple<string, INode> idAndNode;

        public override string Marker
        {
            get { return Convert.ToChar(0x69).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.DeleteExpr;
            }
        }

        public override void Decode()
        {
            idAndNode = DecodeIdAndNode();
        }

        public override string PrettyPrint()
        {
            string name = idAndNode.Item1;
            string arg = idAndNode.Item2.PrettyPrint();
            return name + " " + arg;
        }
    }
}
