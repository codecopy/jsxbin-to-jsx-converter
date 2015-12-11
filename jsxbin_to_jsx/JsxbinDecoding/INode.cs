namespace jsxbin_to_jsx.JsxbinDecoding
{
    public interface INode
    {
        NodeType NodeType { get; }
        string Marker { get; }
        void Decode();
        string PrettyPrint();
        bool PrintStructure { get; set; }
        int IndentLevel { get; set; }
        double JsxbinVersion { get; }
        string DecodeId();
        bool DecodeBool();
    }
}
