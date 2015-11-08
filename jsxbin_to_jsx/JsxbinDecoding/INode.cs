
using System.Collections.Generic;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public interface INode
    {
        NodeType NodeType { get; }
        string Marker { get; }
        void Decode();
        string PrettyPrint();
    }
}
