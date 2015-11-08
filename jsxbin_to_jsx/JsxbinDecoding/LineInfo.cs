using System;
using System.Collections.Generic;
using System.Linq;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public sealed class LineInfo
    {
        public LineInfo()
        {
            Labels = new List<string>();
        }
        public int LineNumber { get; set; }
        public INode Child { get; set; }
        public List<string> Labels { get; set; }

        public string CreateLabelStmt()
        {
            return string.Join(Environment.NewLine, Labels.Select(s => s + ": "));
        }

        public string CreateBody()
        {
            return Child == null ? string.Empty : Child.PrettyPrint();
        }
    }
}
