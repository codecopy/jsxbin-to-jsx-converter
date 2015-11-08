namespace jsxbin_to_jsx.JsxbinDecoding
{
    public sealed class LogicalExpInfo
    {
        public string OpName { get; set; }
        public INode LeftExpr { get; set; }
        public string LeftLiteral { get; set; }
        public INode RightExpr { get; set; }
        public string RightLiteral { get; set; }
        

        public string GetLeftExp()
        {
            return GetExpr(LeftExpr, LeftLiteral);
        }

        public string GetRightExp()
        {
            return GetExpr(RightExpr, RightLiteral);
        }

        public string GetExpr(INode expr, string literal)
        {
            return expr == null ? literal : expr.PrettyPrint();
        }
    }
}
