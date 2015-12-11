using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public interface IReferenceDecoder
    {
        double JsxbinVersion { get; }
        Tuple<string, bool> Decode(INode node);
    }
}
