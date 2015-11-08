using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class UnknownNode1 : AbstractNode
    {
        public override string Marker
        {
            get { return Convert.ToChar(0x71).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.UnknownNode1;
            }
        }

        public override void Decode()
        {
            DecodeReference();
            DecodeNode();
            DecodeNode();
            DecodeNode();
        }

        public override string PrettyPrint()
        {
            throw new Exception("Not defined");
        }
    }
}
