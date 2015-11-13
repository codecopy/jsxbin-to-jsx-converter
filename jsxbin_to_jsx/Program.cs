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
            if (args.Length != 2)
            {
                PrintHelp();
                return;
            }
            var parsedArgs = ParseCommandLine(args);
            if (parsedArgs == null)
            {
                PrintHelp();
                return;
            }
            Decode(parsedArgs);
        }

        static void PrintHelp()
        {
            Console.WriteLine("Invalid arguments given.");
            Console.WriteLine("Usage: jsxbin_to_jsx JSXBIN JSX");
            Console.WriteLine("Example: jsxbin_to_jsx encoded.jsxbin decoded.jsx");
        }

        static void Decode(DecodeArgs decoderArgs)
        {
            try {
                Console.WriteLine("Decoding {0}", decoderArgs.JsxbinFilepath);
                string jsxbin = File.ReadAllText(decoderArgs.JsxbinFilepath, Encoding.ASCII);
                string jsx = AbstractNode.Decode(jsxbin);
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
            decoderArgs.JsxbinFilepath = args[0];
            decoderArgs.JsxFilepath = args[1];
            return decoderArgs;
        }

        private class DecodeArgs
        {
            public string JsxFilepath { get; set; }
            public string JsxbinFilepath { get; set; }
        }
    }
}
