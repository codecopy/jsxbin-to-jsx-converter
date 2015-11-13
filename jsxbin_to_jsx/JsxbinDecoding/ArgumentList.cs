using System;
using System.Collections.Generic;
using System.Linq;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ArgumentList : AbstractNode
    {
        bool boolVal;

        public override string Marker
        {
            get { return Convert.ToChar(0x52).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ArgumentList;
            }
        }

        public List<INode> Arguments { get; set; }

        public override void Decode()
        {
            Arguments = DecodeChildren();
            boolVal = DecodeBool();
        }

        public override string PrettyPrint()
        {
            return string.Join(", ", Arguments.Select(a => a.PrettyPrint()));
        }
    }
}
