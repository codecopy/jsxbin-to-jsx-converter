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
            var leftExpr = CreateExpr(literalLeft, left);
            var rightExpr = CreateExpr(literalRight, right);
            bool isUpdateExpr = (leftExpr != null && rightExpr == null) || (leftExpr == null && rightExpr != null);
            if (isUpdateExpr)
            {
                string op = leftExpr + rightExpr;
                OpName = opName;
                Op = op;
            }
            else
            {
                Op = string.Format("{0} {1} {2}", leftExpr, opName, rightExpr);
            }
        }

        public override string PrettyPrint()
        {
            return Op;
        }

        string CreateExpr(string literal, INode expr)
        {
            bool requiresParens = false;
            string actualExpr;
            if (expr != null && expr.NodeType == NodeType.BinaryExpr)
            {
                var binaryExpr = (BinaryExpr)expr;
                actualExpr = binaryExpr.Op;
                requiresParens = true;
                bool isAssociativeOp = (binaryExpr.opName == "*" && opName == "*") || (binaryExpr.opName == "+" && opName == "+");
                if (isAssociativeOp)
                {
                    requiresParens = false;
                }
            }
            else
            {
                actualExpr = expr == null ? literal : expr.PrettyPrint();
            }
            return requiresParens ? "(" + actualExpr + ")" : actualExpr;
        }
    }
}
