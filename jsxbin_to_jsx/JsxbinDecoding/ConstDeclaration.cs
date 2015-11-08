using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public sealed class ConstDeclaration : AbstractNode
    {
        ConstDeclarationInfo info;

        public override string Marker
        {
            get
            {
                return Convert.ToChar(0x47).ToString();
            }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ConstDeclaration;
            }
        }

        public override void Decode()
        {
            info = DecodeConstDeclaration();
        }
        
        public override string PrettyPrint()
        {
            string name = info.Name;
            string value = info.GetValue();
            return string.Format("const {0} = {1}", name, value);
        }
    }
}
