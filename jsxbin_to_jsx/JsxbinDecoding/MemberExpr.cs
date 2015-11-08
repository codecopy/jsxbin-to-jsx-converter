using System;
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class MemberExpr : AbstractNode
    {
        Tuple<string, bool> memberInfo;
        INode objInfo;

        public override string Marker
        {
            get { return Convert.ToChar(0x58).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.MemberExpr;
            }
        }

        public override void Decode()
        {
            memberInfo = DecodeReference();
            objInfo = DecodeNode();
        }

        public override string PrettyPrint()
        {
            string obj = objInfo == null ? "" : objInfo.PrettyPrint();
            string member = "";
            if (IsValidIdentifier(memberInfo.Item1))
            {
                member = "." + memberInfo.Item1;
            }
            else
            {
                // Prettier format for array indexing. ["1"] becomes [1].
                // http://stackoverflow.com/questions/27537677/is-javascript-array-index-a-string-or-integer
                int numericalIndex;
                if (Int32.TryParse(memberInfo.Item1, out numericalIndex))
                {
                    member = "[" + memberInfo.Item1 + "]";
                }
                else
                {
                    member = "[\"" + memberInfo.Item1 + "\"]";
                }
            }
            return obj + member;
        }
    }
}
