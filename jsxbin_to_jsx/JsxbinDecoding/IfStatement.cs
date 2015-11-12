using System;
using System.Collections.Generic;
using System.Text;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class IfStatement : AbstractNode
    {
        LineInfo body;
        INode test;
        INode tail;

        public override string Marker
        {
            get { return Convert.ToChar(0x4F).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.IfStatement;
            }
        }

        public INode Tail
        {
            get
            {
                return tail;
            }

            set
            {
                tail = value;
            }
        }

        public INode Test
        {
            get
            {
                return test;
            }

            set
            {
                test = value;
            }
        }

        public LineInfo Body
        {
            get
            {
                return body;
            }

            set
            {
                body = value;
            }
        }

        public override void Decode()
        {
            body = DecodeLineInfo();
            test = DecodeNode();
            tail = DecodeNode();
        }

        public override string PrettyPrint()
        {
            StringBuilder output = new StringBuilder();
            BuildIf(output);
            if (!HasElse())
            {
                return output.ToString();
            }
            BuildTail(output);
            return output.ToString();
        }

        void BuildIf(StringBuilder output)
        {
            string label = body.CreateLabelStmt();
            string bodyExpr = body.CreateBody();
            string testExpr = test.PrettyPrint();
            output.AppendLine(string.Format("{0}if ({1}) {{", label, testExpr));
            output.AppendLine(bodyExpr);
            output.Append("}");
        }

        void BuildTail(StringBuilder output)
        {
            INode currentTail = tail;
            while (IsElseIf(currentTail))
            {
                var elseIf = (IfStatement)currentTail;
                BuildElseIf(elseIf, output);
                currentTail = elseIf.Tail;
            }
            BuildElse(currentTail, output);
        }

        void BuildElseIf(IfStatement node, StringBuilder output)
        {
            output.AppendLine(string.Format(" {0}else if ({1}) {{", node.Body.CreateLabelStmt(), node.Test.PrettyPrint()));
            output.AppendLine(node.Body.CreateBody());
            output.Append("}");
        }

        void BuildElse(INode node, StringBuilder output)
        {
            output.AppendLine(" else {");
            output.AppendLine(node.PrettyPrint());
            output.Append("}");
        }

        bool IsElseIf(INode node)
        {
            return node.NodeType == NodeType.IfStatement && ((IfStatement)node).Tail != null;
        }

        bool HasElse()
        {
            return Tail != null;
        }
    }
}
