using System;
using System.Text;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class XMLAssignmentExpr : AbstractNode
    {
        string expr;

        public override string Marker
        {
            get { return Convert.ToChar(0x6F).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.XMLAssignmentExpr;
            }
        }

        public override void Decode()
        {
            const int TYPE_NORMAL = 0;
            const int TYPE_ELEMENTPLACEHOLDER = 1;
            const int TYPE_ATTR_PLACEHOLDER = 2;
            const int TYPE_VALUE_PLACEHOLDER = 3;

            StringBuilder res = new StringBuilder();
            ForEachChild(() =>
            {
                var child = DecodeNode();
                var type = DecodeLength();
                if (type == TYPE_NORMAL)
                {
                    res.Append(child.PrettyPrint());
                }
                else
                {
                    res.Append(" + " + child.PrettyPrint() + " + ");
                }
            });
            expr = res.ToString();
        }

        public override string PrettyPrint()
        {
            return expr;
        }
    }
}
