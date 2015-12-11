using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public sealed class ReferenceDecoderVersion1 : IReferenceDecoder
    {
        public double JsxbinVersion
        {
            get
            {
                return 1.0;
            }
        }

        public Tuple<string, bool> Decode(INode node)
        {
            var id = node.DecodeId();
            return Tuple.Create(id, false);
        }
    }
}
