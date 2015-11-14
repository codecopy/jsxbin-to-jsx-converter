using System;
using System.Collections.Generic;
using System.Linq;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class StatementList : AbstractNode
    {
        LineInfo lineInfo;
        int length;
        List<INode> functionDeclarations;
        List<INode> statements;

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
            functionDeclarations = new List<INode>();
            statements = new List<INode>();
            int i = length;
            while (i > 0)
            {
                functionDeclarations.Add(DecodeNode());
                i--;
            }
            statements.AddRange(DecodeChildren());
        }

        public override string PrettyPrint()
        {
            string labels = lineInfo.CreateLabelStmt();
            var functionDeclarationsPretty = functionDeclarations.Select(f => f.PrettyPrint()).ToList();
            var statementsPretty = statements.Select(f => {
                bool requiresSemicolon = f.NodeType == NodeType.ExprNode;
                string expr = f.PrettyPrint();
                if (requiresSemicolon)
                {
                    expr = expr + ";";
                }
                return expr;
            }).ToList();
            string block = string.Join(
                Environment.NewLine, functionDeclarationsPretty
                .Concat(statementsPretty));
            return labels + block;
        }
    }
}
