namespace jsxbin_to_jsx.JsxbinDecoding
{
    public sealed class ConstDeclarationInfo
    {

        public string Name { get; set; }
        public int Length { get; set; }
        public INode Expr { get; set; }
        public string Literal { get; set; }
        public bool BoolVal1 { get; set; }
        public bool BoolVal2 { get; set; }

        public string GetValue()
        {
            return Expr == null ? Literal : Expr.PrettyPrint();
        }
    }
}
