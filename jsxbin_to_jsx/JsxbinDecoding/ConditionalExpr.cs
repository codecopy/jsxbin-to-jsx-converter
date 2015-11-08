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
            return string.Format("{0} ? ({1}) : ({2})", condition.PrettyPrint(), ifTrue.PrettyPrint(), ifFalse.PrettyPrint());
        }
    }
}
