using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ArrayIndexingExpr : AbstractNode
    {
        string arrayName;
        string expr;

        public override string Marker
        {
            get { return Convert.ToChar(0x51).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ArrayIndexingExpr;
            }
        }

        public override void Decode()
        {
            var array = DecodeRefAndNode();
            var exprInfo = DecodeNode();
            arrayName = array.Item2.PrettyPrint();
            expr = exprInfo.PrettyPrint();
        }

        public override string PrettyPrint()
        {
            return string.Format("{0}[{1}]", arrayName, expr);
        }
    }
}
