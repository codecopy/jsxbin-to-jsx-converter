using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class AssignmentExpr : AbstractNode
    {
        string varName;
        bool isDeclaration;
        bool isShorthand;
        INode expr;
        string literal;

        public override string Marker
        {
            get { return Convert.ToChar(0x53).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.AssignmentExpr;
            }
        }

        public override void Decode()
        {
            varName = DecodeId();
            var type = DecodeLength();
            expr = DecodeNode();
            literal = DecodeVariant();
            isShorthand = DecodeBool();
            isDeclaration = DecodeBool();
        }

        public override string PrettyPrint()
        {
            string varKeyword = isDeclaration ? "var " : "";
            string result = "";
            if (isShorthand)
            {
                BinaryExpr binaryExpr = (BinaryExpr)expr;
                string assignExpr = literal == null ? binaryExpr.Op : literal;
                string opName = binaryExpr.OpName;
                result = string.Format("{0}{1} {2}= {3}", varKeyword, varName, opName, assignExpr);
            }
            else
            {
                string assignExpr = literal == null ? expr.PrettyPrint() : literal;
                result = string.Format("{0}{1} = {2}", varKeyword, varName, assignExpr);
            }
            return result;
        }
    }
}
