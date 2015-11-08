using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class IdRefExpr : AbstractNode
    {
        Tuple<string, bool> val;
        int type;

        public override string Marker
        {
            get { return Convert.ToChar(0x56).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.IdRefExpr;
            }
        }

        public override void Decode()
        {
            val = DecodeReference();
            type = DecodeLength();
        }

        public override string PrettyPrint()
        {
            string idName = val.Item1;
            return idName;
        }
    }
}
