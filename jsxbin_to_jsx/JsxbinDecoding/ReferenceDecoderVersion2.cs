using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public sealed class ReferenceDecoderVersion2 : IReferenceDecoder
    {
        public double JsxbinVersion
        {
            get
            {
                return 2.0;
            }
        }

        public Tuple<string, bool> Decode(INode node)
        {
            var id = node.DecodeId();
            var flag = node.DecodeBool();
            return Tuple.Create(id, flag);
        }
    }
}
