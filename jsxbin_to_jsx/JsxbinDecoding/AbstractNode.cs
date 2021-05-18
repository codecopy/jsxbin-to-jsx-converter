using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public abstract class AbstractNode : INode
    {
        public const double ALL_VERSIONS = 0;
        private const string alphabet_lower = "ghijklmn";
        private const string alphabet_upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdef";
        private static readonly Dictionary<string, Type> decoders = new Dictionary<string, Type>();
        private static ScanState scanState;
        private static readonly Regex ValidIdentifier = new Regex("^[a-zA-Z_$][0-9a-zA-Z_$]*$", RegexOptions.Compiled);
        private static IReferenceDecoder referenceDecoder;

        public abstract string Marker { get; }
        public abstract NodeType NodeType { get; }
        public abstract void Decode();
        public abstract string PrettyPrint();
        public bool PrintStructure { get; set; }
        public int IndentLevel { get; set; }

        public virtual double JsxbinVersion
        {
            get
            {
                return ALL_VERSIONS;
            }
        }

        protected ScanState ScanState
        {
            get
            {
                return scanState;
            }
        }

        protected bool IsValidIdentifier(string identifier)
        {
            return ValidIdentifier.IsMatch(identifier);
        }

        public static string Decode(string jsxbin, bool printStructure)
        {
            string normalized = jsxbin.Replace("\n", "").Replace("\r", "").Replace("\\", "");
            Match versionMatch = Regex.Match(normalized, "^@JSXBIN@ES@([\\d.]+)@");
            double version = ALL_VERSIONS;
            if (versionMatch.Success)
                version = double.Parse(versionMatch.Groups[1].Value, CultureInfo.InvariantCulture);
            string noheader = Regex.Replace(normalized, "^@JSXBIN@ES@[\\d.]+@", "");
            scanState = new ScanState(noheader);
            InitializeDecoders(scanState, version);
            var root = new RootNode();
            root.PrintStructure = printStructure;
            root.Decode();
            var jsx = root.PrettyPrint();
            return string.Join(Environment.NewLine, jsx);
        }

        public string DecodeNumber()
        {
            var chr = GetCurrentAndAdvanceCore(scanState.Clone());
            string number = "";
            if (IsHex(chr, Constants.MARKER_NUMBER_8_BYTES))
            {
                var val = GetCurrentAndAdvanceCore(scanState);
                number = DecodeNumberCore(8, false);
            }
            else
            {
                number = DecodeLiteral(true, false);
            }
            return number ?? "0";
        }

        public bool DecodeBool()
        {
            var str = GetCurrentAndAdvanceCore(scanState);
            if (str == Constants.BOOL_FALSE)
            {
                return false;
            }
            else if (str == Constants.BOOL_TRUE)
            {
                return true;
            }
            else
            {
                throw new Exception("unexpected bool value " + str);
            }
        }

        private static void InitializeDecoders(ScanState scanState, double version)
        {
            decoders.Clear();
            var type = typeof(INode);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && !p.IsAbstract && p.IsSubclassOf(typeof(AbstractNode)));
            foreach (var t in types)
            {
                INode n = (INode)Activator.CreateInstance(t);
                if (n.JsxbinVersion == ALL_VERSIONS || n.JsxbinVersion == version)
                    decoders.Add(n.Marker, t);
            }
            if (version == 1.0)
                referenceDecoder = new ReferenceDecoderVersion1();
            else
                referenceDecoder = new ReferenceDecoderVersion2();                
        }

        public INode DecodeNode()
        {
            char marker = GetCurrentAndAdvance(scanState);
            if (IsHex(marker, Constants.MARKER_HAS_NO_VARIANT))
            {
                return null;
            }
            else if (decoders.ContainsKey(marker.ToString()))
            {
                var type = decoders[marker.ToString()];
                INode node = (INode)Activator.CreateInstance(type);
                bool ignoreHeaderFunction = this.NodeType == NodeType.Root;
                if (PrintStructure && !ignoreHeaderFunction)
                {
                    Console.WriteLine(new string(' ', 4 * IndentLevel) + node.NodeType.ToString());
                    node.IndentLevel = IndentLevel + 1;
                }
                node.PrintStructure = PrintStructure;
                node.Decode();
                return node;
            }
            else
            {
                throw new Exception("No decoder found for marker " + marker);
            }
        }

        public char GetCurrentAndAdvance(ScanState scanState)
        {
            return GetCurrentAndAdvanceCore(scanState);
        }

        public char GetCurrentAndAdvanceCore(ScanState scanState)
        {
            char cur = scanState.getCur();
            scanState.Inc();
            return cur;
        }

        public bool IsNested()
        {
            return scanState.GetNestingLevels() > 0;
        }

        public void DecrementNesting()
        {
            scanState.DecrementNestingLevels();
        }

        public LogicalExpInfo DecodeLogicalExp()
        {
            var info = new LogicalExpInfo();
            info.OpName = DecodeId();
            info.LeftExpr = DecodeNode();
            info.RightExpr = DecodeNode();
            info.LeftLiteral = DecodeVariant();
            info.RightLiteral = DecodeVariant();
            return info;
        }

        public string DecodeVariant()
        {
            var chr = GetCurrentAndAdvance(scanState.Clone());
            if (IsHex(chr, Constants.MARKER_HAS_NO_VARIANT))
            {
                GetCurrentAndAdvance(scanState);
                return null;
            }
            else
            {
                return DecodeVariantCore();
            }
        }

        public bool IsHex(char src, int num)
        {
            return src == Convert.ToChar(num);
        }

        public string DecodeId()
        {
            char marker = GetCurrentAndAdvanceCore(scanState.Clone());
            if (!IsHex(marker, Constants.MARKER_ID_REFERENCE))
            {
                var id = DecodeLength().ToString();
                return scanState.GetSymbol(id);
            }
            else
            {
                var type = GetCurrentAndAdvanceCore(scanState);
                var name = DecodeString();
                var id = DecodeLength().ToString();
                scanState.AddSymbol(id, name);
                return name;
            }
        }

        public string DecodeVariantCore()
        {
            char str = GetCurrentAndAdvanceCore(scanState);
            int typ = str - 0x61;
            string val = "";
            switch (typ)
            {
                case 2:
                    val = DecodeBool().ToString();
                    val = val.ToLower();
                    break;
                case 3:
                    val = DecodeNumber();
                    break;
                case 4:
                    val = DecodeString();
                    val = val.Replace("\\", "\\\\");
                    val = val.Replace("\"", "\\\"");
                    val = "\"" + val + "\"";                    
                    val = val.Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r");                    
                    break;
                case 0:
                case 1:
                    val = "null";
                    break;
                default:
                    throw new Exception("Unexpected Variant type: " + typ.ToString());
            }
            return val.ToString();
        }

        public string DecodeString()
        {
            string str = DecodeLiteral(true, false);
            int length = str == null ? 0 : Int32.Parse(str);
            if (length == 0)
            {
                return "";
            }
            string res = "";
            int i = length;
            while (i > 0)
            {
                res += DecodeLiteral(false, true);
                i--;
            }
            return res;
        }

        public string DecodeNumberCore(int numberLength, bool twosComplement)
        {
            List<byte> bytes = new List<byte>();
            int i = numberLength;
            while (i > 0)
            {
                byte decr = DecodeByte();
                bytes.Add(decr);
                i--;
            }
            string decryptedNumber = "";
            if (numberLength == 4)
            {
                uint number = BitConverter.ToUInt32(bytes.ToArray(), 0);
                decryptedNumber = number.ToString();
            }
            else if (numberLength == 2)
            {
                ushort number = BitConverter.ToUInt16(bytes.ToArray(), 0);
                decryptedNumber = number.ToString();
            }
            else if (numberLength == 8)
            {
                double number = BitConverter.ToDouble(bytes.ToArray(), 0);
                decryptedNumber = number.ToString();
            }
            else
            {
                throw new Exception("Unknown number length found: " + numberLength);
            }
            if (twosComplement)
            {
                decryptedNumber = "-" + decryptedNumber;
                return decryptedNumber;
            }
            else
            {
                return decryptedNumber;
            }
        }

        public string ConvertToUnicodeString(string codePoint)
        {
            int code = int.Parse(codePoint);
            return char.ConvertFromUtf32(code).ToString();
        }

        public string DecodeLiteral(bool isNumber, bool isUnicodeString)
        {
            if (IsNested())
            {
                DecrementNesting();
                return null;
            }
            bool twosComplement = false;
            if (scanState.IsHex(Constants.MARKER_NEGATIVE_NUMBER))
            {
                twosComplement = true;
                GetCurrentAndAdvanceCore(scanState);
            }
            if (scanState.IsHex(Constants.MARKER_NUMBER_4_BYTES))
            {
                GetCurrentAndAdvanceCore(scanState);
                string nr = DecodeNumberCore(4, twosComplement);
                if (isUnicodeString)
                {
                    nr = ConvertToUnicodeString(nr);
                }
                return nr;
            }
            else if (scanState.IsHex(Constants.MARKER_NUMBER_2_BYTES))
            {
                GetCurrentAndAdvanceCore(scanState);
                string nr = DecodeNumberCore(2, twosComplement);
                if (isUnicodeString)
                {
                    nr = ConvertToUnicodeString(nr);
                }
                return nr;
            }
            else
            {
                byte bz = DecodeByte();
                string str = "";
                if (twosComplement)
                {
                    int nr = -1 * (int)bz;
                    str = nr.ToString();
                }
                else
                {
                    if (isNumber)
                    {
                        str = bz.ToString();
                    }
                    else
                    {
                        str = Encoding.GetEncoding("iso-8859-1").GetString(new byte[] { bz });
                    }
                }
                return str;
            }
        }

        public byte DecodeByte()
        {
            if (IsNested())
            {
                DecrementNesting();
                return 0;
            }
            int[] res = new int[2];
            var first = GetCurrentAndAdvanceCore(scanState);
            byte decrypt = 0;
            int minus41 = first - 0x41;
            if (minus41 <= 0x19)
            {
                decrypt = (byte)minus41;
            }
            else
            {
                int n = alphabet_lower.IndexOf(first);
                char second = GetCurrentAndAdvanceCore(scanState);
                int rest = alphabet_upper.IndexOf(second);
                decrypt = (byte)(n * 32 + rest);
            }
            return decrypt;
        }

        public ConstDeclarationInfo DecodeConstDeclaration()
        {
            ConstDeclarationInfo info = new ConstDeclarationInfo();
            List<string> res = new List<string>();
            info.Name = DecodeId();
            info.Length = DecodeLength();
            info.Expr = DecodeNode();
            info.Literal = DecodeVariant();
            info.BoolVal1 = DecodeBool();
            info.BoolVal2 = DecodeBool();
            return info;
        }

        public int DecodeLength()
        {
            var length = DecodeLiteral(true, false);
            return length == null ? 0 : Math.Abs(Int32.Parse(length));
        }

        public LineInfo DecodeLineInfo()
        {
            var info = new LineInfo();
            List<string> res = new List<string>();
            var lineNumber = DecodeLength();
            var child = DecodeNode();
            int length = DecodeLength();
            info.LineNumber = lineNumber;
            info.Child = child;
            int i = length;
            while (i > 0)
            {
                info.Labels.Add(DecodeId());
                i--;
            }
            return info;
        }

        public Tuple<string, bool> DecodeReference()
        {
            return referenceDecoder.Decode(this);
        }

        public LineInfo DecodeBody()
        {
            return DecodeLineInfo();
        }
        
        public List<INode> DecodeChildren()
        {
            int length = DecodeLength();
            if (length == 0)
            {
                return new List<INode>();
            }
            int i = length;
            List<INode> res = new List<INode>();
            while (i > 0)
            {
                var child = DecodeNode();
                if (child != null)
                {
                    res.Add(child);
                }
                i--;
            }
            return res;
        }

        public void ForEachChild(Action action)
        {
            int length = DecodeLength();
            if (length == 0)
            {
                return;
            }
            int i = length;
            while (i > 0)
            {
                action();
                i--;
            }
        }

        public FunctionSignature DecodeSignature()
        {
            var sig = new FunctionSignature();
            int length = DecodeLength();
            if (length > 0)
            {
                int i = length;
                while (i > 0)
                {
                    var paramName = DecodeId();
                    // does not contain anything worthwile, only nesting
                    var paramLength = DecodeLength();
                    sig.Parameter.Add(Tuple.Create(paramName, paramLength));
                    i--;
                }
            }
            sig.Header1 = DecodeLength();
            sig.Type = DecodeLength();
            sig.Header3 = DecodeLength();
            sig.Name = DecodeId();
            sig.Header5 = DecodeLiteralNumber2();
            return sig;
        }

        public int DecodeLiteralNumber2()
        {
            string number = DecodeLiteral(true, false);
            return number == null ? 0 : Int32.Parse(number);
        }

        public Tuple<Tuple<string, bool>, INode> DecodeRefAndNode()
        {
            List<string> res = new List<string>();
            var refa = DecodeReference();
            var node = DecodeNode();
            return Tuple.Create(refa, node);
        }

        public Tuple<string, INode> DecodeIdAndNode()
        {
            List<string> res = new List<string>();
            string id = DecodeId();
            var node = DecodeNode();
            return Tuple.Create(id, node);
        }

        public override string ToString()
        {
            throw new Exception("Call PrettyPrint() instead.");
        }
    }
}
