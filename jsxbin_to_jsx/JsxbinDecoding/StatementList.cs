using System;
using System.Collections.Generic;
using System.Linq;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class StatementList : AbstractNode, IStatement
    {
        LineInfo lineInfo;
        int length;
        List<INode> statements;

        public int LineNumber
        {
            get
            {
                return lineInfo.LineNumber;
            }
        }

        public override string Marker
        {
            get
            {
                return Convert.ToChar(0x62).ToString();
            }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.StatementList;
            }
        }

        public override void Decode()
        {
            lineInfo = DecodeBody();
            length = DecodeLength();
            statements = new List<INode>();
            int i = length;
            while (i > 0)
            {
                statements.Add(DecodeNode());
                i--;
            }
            statements.AddRange(DecodeChildren());
        }

        public override string PrettyPrint()
        {
            string labels = lineInfo.CreateLabelStmt();
            var statementsOrdered = statements.OrderBy(s => ((IStatement)s).LineNumber);
            var statementsPretty = statementsOrdered.Select(f =>
            {
                bool requiresSemicolon = f.NodeType == NodeType.ExprNode;
                string expr = f.PrettyPrint();
                if (requiresSemicolon)
                {
                    expr = expr + ";";
                }
                return expr;
            }).ToList();
            string block = string.Join(Environment.NewLine, statementsPretty);
            return labels + block;
        }
    }
}
