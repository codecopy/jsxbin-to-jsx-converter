using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class UnknownNode4 : AbstractNode
    {
        public override string Marker
        {
            get { return Convert.ToChar(0x6B).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.UnknownNode4;
            }
        }

        public override void Decode()
        {
            DecodeNode();
        }

        public override string PrettyPrint()
        {
            throw new Exception("Not defined");
        }
    }
}
