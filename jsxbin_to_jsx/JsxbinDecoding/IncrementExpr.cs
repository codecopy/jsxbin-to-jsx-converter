using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class IncrementExpr : AbstractNode
    {
        string id;
        int p2;
        string addOrSubtract;
        bool isPostFix;

        public override string Marker
        {
            get { return Convert.ToChar(0x54).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.IncrementExpr;
            }
        }

        public override void Decode()
        {
            id = DecodeId();
            p2 = DecodeLength();
            addOrSubtract = DecodeNumber();
            isPostFix = DecodeBool();
        }

        public override string PrettyPrint()
        {
            string op = "";
            if (addOrSubtract == "1")
            {
                op = "++";
            }
            else
            {
                op = "--";
            }
            if (isPostFix)
            {
                return id + op;
            }
            else
            {
                return op + id;
            }
        }
    }
}
