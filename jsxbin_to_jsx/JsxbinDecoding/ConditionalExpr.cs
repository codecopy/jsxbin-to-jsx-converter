using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ConditionalExpr : AbstractNode
    {
        INode condition;
        INode ifTrue;
        INode ifFalse;

        public override string Marker
        {
            get { return Convert.ToChar(0x64).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ConditionalExpr;
            }
        }

        public override void Decode()
        {
            condition = DecodeNode();
            ifTrue = DecodeNode();
            ifFalse = DecodeNode();
        }

        public override string PrettyPrint()
        {
            string printed = string.Format("{0} ? {1} : {2}", condition.PrettyPrint(),
                RequiresParens(ifTrue) ? "(" + ifTrue.PrettyPrint() + ")" : ifTrue.PrettyPrint(),
                RequiresParens(ifFalse) ? "(" + ifFalse.PrettyPrint() + ")" : ifFalse.PrettyPrint());
            return printed;
        }

        bool RequiresParens(INode node)
        {
            return node.NodeType == NodeType.ConditionalExpr || node.NodeType == NodeType.ArgumentList;
        }
    }
}
