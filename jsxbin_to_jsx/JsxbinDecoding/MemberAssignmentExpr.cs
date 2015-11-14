using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class MemberAssignmentExpr : AbstractNode
    {
        INode varNode;
        INode expr;
        string literal;
        bool isShorthand;

        public override string Marker
        {
            get { return Convert.ToChar(0x42).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.MemberAssignmentExpr;
            }
        }

        public override void Decode()
        {
            varNode = DecodeNode();
            expr = DecodeNode();
            literal = DecodeVariant();
            isShorthand = DecodeBool();
        }

        public override string PrettyPrint()
        {
            if (isShorthand)
            {
                BinaryExpr binaryExpr = (BinaryExpr)expr;
                string assignExpr = literal == null ? binaryExpr.Op : literal;
                string opName = binaryExpr.OpName;
                return string.Format("{0} {1}= {2}", varNode.PrettyPrint(), opName, assignExpr);
            }
            else
            {
                string assignExpr = literal == null ? expr.PrettyPrint() : literal;
                return string.Format("{0} = {1}", varNode.PrettyPrint(), assignExpr);
            }
        }
    }
}
