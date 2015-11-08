using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class BinaryExpr : AbstractNode
    {
        string opName;
        INode left;
        INode right;
        string literalLeft;
        string literalRight;

        public override string Marker
        {
            get { return Convert.ToChar(0x43).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.BinaryExpr;
            }
        }

        public string OpName { get; set; }
        public string Op { get; set; }

        public override void Decode()
        {
            opName = DecodeId();
            left = DecodeNode();
            right = DecodeNode();
            literalLeft = DecodeVariant();
            literalRight = DecodeVariant();

            string leftExpr;
            if (left != null && left.NodeType == NodeType.BinaryExpr)
            {
                leftExpr = ((BinaryExpr)left).Op;
            }
            else
            {
                leftExpr = left == null ? literalLeft : left.PrettyPrint();
            }
            string rightExpr;
            if (right != null && right.NodeType == NodeType.BinaryExpr)
            {
                rightExpr = ((BinaryExpr)right).Op;
            }
            else
            {
                rightExpr = right == null ? literalRight : right.PrettyPrint();
            }
            bool isUpdateExpr = (leftExpr != null && rightExpr == null) || (leftExpr == null && rightExpr != null);
            if (isUpdateExpr)
            {
                string op = leftExpr + rightExpr;
                OpName = opName;
                Op = op;
            }
            else
            {
                Op = string.Format("({0} {1} {2})", leftExpr, opName, rightExpr);
            }
        }

        public override string PrettyPrint()
        {
            return Op;
        }
    }
}
