using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ThrowStatement : AbstractNode
    {
        LineInfo part1;
        INode exprInfo;

        public override string Marker
        {
            get { return Convert.ToChar(0x66).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ThrowStatement;
            }
        }

        public override void Decode()
        {
            part1 = DecodeLineInfo();
            exprInfo = DecodeNode();
        }        

        public override string PrettyPrint()
        {
            var body = exprInfo.PrettyPrint();
            var expr = "throw " + body;
            return expr;
        }
    }
}
