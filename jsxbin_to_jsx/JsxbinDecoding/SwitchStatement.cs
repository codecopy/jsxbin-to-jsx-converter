using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class SwitchStatement : AbstractNode
    {
        LineInfo labelInfo;
        INode test;
        List<INode> cases;
        List<INode> bodies;

        public override string Marker
        {
            get { return Convert.ToChar(0x63).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.SwitchStatement;
            }
        }

        public override void Decode()
        {
            labelInfo = DecodeBody();
            test = DecodeNode();
            cases = new List<INode>();
            ForEachChild(() =>
            {
                var r = DecodeNode();
                if (r != null)
                {
                    cases.Add(r);
                }
            });
            bodies = new List<INode>();
            ForEachChild(() =>
            {
                var q = DecodeNode();
                if (q != null)
                {
                    bodies.Add(q);
                }
            });
        }

        public override string PrettyPrint()
        {
            StringBuilder b = new StringBuilder();
            b.AppendLine(string.Format("switch({0}) {{", test.PrettyPrint()));
            for (int i = 0; i < cases.Count; i++)
            {
                var caseArgs = ((FunctionArguments)cases[i]).Arguments;
                string caseStmt = "";
                if (caseArgs.Count > 0)
                {
                    caseStmt = string.Join(Environment.NewLine, caseArgs.Select(c => string.Format("case {0}:", c.PrettyPrint())));
                }
                else
                {
                    caseStmt = "default:";
                }
                b.AppendLine(caseStmt);
                string bodyStmt = "";
                if (i < bodies.Count)
                {
                    bodyStmt = bodies[i].PrettyPrint();
                }
                b.AppendLine(bodyStmt);
            }
            b.Append("}");
            return b.ToString();
        }
    }
}
