using System;
using System.Collections.Generic;
using System.Linq;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class BlockStatement : AbstractNode
    {
        LineInfo lineInfo;
        int length;
        List<INode> functionDeclarations;
        List<INode> variableDeclarations;
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
                return NodeType.BlockStatement;
            }
        }

        public override void Decode()
        {
            lineInfo = DecodeBody();
            length = DecodeLength();
            functionDeclarations = new List<INode>();
            variableDeclarations = new List<INode>();
            statements = new List<INode>();
            if (length > 0)
            {
                int i = length;
                while (i > 0)
                {
                    functionDeclarations.Add(DecodeNode());
                    i--;
                }
                variableDeclarations.AddRange(DecodeChildren());
            }
            else
            {
                statements.AddRange(DecodeChildren());
            }
        }

        public override string PrettyPrint()
        {
            string labels = lineInfo.CreateLabelStmt();
            var functionDeclarationsPretty = functionDeclarations.Select(f => f.PrettyPrint()).ToList();
            var variableDeclarationsPretty = variableDeclarations.Select(g => g.PrettyPrint()).ToList();
            var statementsPretty = statements.Select(f => f.PrettyPrint()).ToList();
            string block = string.Join(
                Environment.NewLine, functionDeclarationsPretty
                .Concat(variableDeclarationsPretty)
                .Concat(statementsPretty));
            return labels + block;
        }
    }
}
