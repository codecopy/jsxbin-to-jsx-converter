using System;
using System.Collections.Generic;
using System.Text;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ForStatement : AbstractNode
    {
        LineInfo bodyInfo;
        INode loopVarInfo;
        string initExpr;
        INode upperBoundInfo;
        string stepSize;
        int part6;
        string compOp;

        public override string Marker
        {
            get { return Convert.ToChar(0x61).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ForStatement;
            }
        }

        public override void Decode()
        {
            bodyInfo = DecodeBody();
            loopVarInfo = DecodeNode();
            initExpr = DecodeNumber();
            upperBoundInfo = DecodeNode();
            stepSize = DecodeNumber();
            part6 = DecodeLength();
            compOp = DecodeId();
        }

        public override string PrettyPrint()
        {
            string label = bodyInfo.CreateLabelStmt();
            string body = bodyInfo.CreateBody();
            string loopVarName = loopVarInfo == null ? null : loopVarInfo.PrettyPrint();
            string upperBound = upperBoundInfo == null ? null : upperBoundInfo.PrettyPrint();
            StringBuilder b = new StringBuilder();
            b.AppendLine(string.Format("{0}for (var {1} = {2};{1} {3} {4}; {1} += {5}) {{", label, loopVarName, initExpr, compOp, upperBound, stepSize));
            b.AppendLine(body);
            b.Append("}");
            return b.ToString();
        }
    }
}
