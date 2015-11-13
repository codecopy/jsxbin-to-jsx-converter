using Jsbeautifier;
using jsxbin_to_jsx.JsxbinDecoding;
using System;
using System.IO;
using System.Text;

namespace jsxbin_to_jsx
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                PrintHelp();
                return;
            }
            DecodeArgs parsedArgs = null;
            try
            {
                parsedArgs = ParseCommandLine(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                PrintHelp();
                return;
            }
            Decode(parsedArgs);
        }

        static void PrintHelp()
        {
            Console.WriteLine("Usage: [-v] jsxbin_to_jsx JSXBIN JSX");
            Console.WriteLine("Example: -v jsxbin_to_jsx encoded.jsxbin decoded.jsx");
            Console.WriteLine("Flags:");
            Console.WriteLine("-v print tree structure to stdout");
        }

        static void Decode(DecodeArgs decoderArgs)
        {
            try
            {
                Console.WriteLine("Decoding {0}", decoderArgs.JsxbinFilepath);
                string jsxbin = File.ReadAllText(decoderArgs.JsxbinFilepath, Encoding.ASCII);
                string jsx = AbstractNode.Decode(jsxbin, decoderArgs.PrintStructure);
                jsx = new Beautifier().Beautify(jsx);
                File.WriteAllText(decoderArgs.JsxFilepath, jsx, Encoding.UTF8);
                Console.WriteLine("Jsxbin successfully decoded to {0}", decoderArgs.JsxFilepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decoding failed. If this problem persists, please raise an issue on github. Error message: {0}. Stacktrace: {1}.", ex.Message, ex.StackTrace);
            }
        }

        static DecodeArgs ParseCommandLine(string[] args)
        {
            var decoderArgs = new DecodeArgs();
            int flagOffset = 0;
            if (args.Length > 2)
            {
                if (args[0] == "-v")
                {
                    flagOffset++;
                    decoderArgs.PrintStructure = true;
                }
                else
                {
                    throw new Exception(string.Format("Flag {0} is not valid.", args[0]));
                }
            }
            decoderArgs.JsxbinFilepath = args[flagOffset];
            decoderArgs.JsxFilepath = args[flagOffset + 1];
            return decoderArgs;
        }

        private class DecodeArgs
        {
            public string JsxFilepath { get; set; }
            public string JsxbinFilepath { get; set; }
            public bool PrintStructure { get; set; }
        }
    }
}
