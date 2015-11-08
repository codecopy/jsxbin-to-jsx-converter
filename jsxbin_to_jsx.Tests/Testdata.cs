using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace jsxbin_to_jsx.Tests
{
    public class Testdata
    {

        public IEnumerable<JsxJsxbinPair> ReadTestfiles()
        {
            string basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string TestfilesDirectory = Path.Combine(basePath, @"testfiles");
            // *.jsx matches .jsxbin files as well, which is not what we want.
            // http://www.codeproject.com/Questions/152289/Directory-Get-Files-search-pattern-problem
            var jsxfiles = Directory.EnumerateFiles(TestfilesDirectory, "*.jsx").Where(f => f.EndsWith(".jsx")).Select(f => new FileInfo(f));
            var jsxJsxbinFilepaths = jsxfiles.Select(f => Tuple.Create(f.FullName, f.Name, CreateJsxbinFilepath(f)));
            var jsxJsxbinPairs = jsxJsxbinFilepaths.Select(p => new JsxJsxbinPair()
            {
                Jsx = ReadFile(p.Item1),
                JsxFilename = p.Item2,
                Jsxbin = ReadFile(p.Item3)
            });
            return jsxJsxbinPairs.ToList();
        }

        private string CreateJsxbinFilepath(FileInfo jsxFilepath)
        {
            return Path.Combine(jsxFilepath.DirectoryName, Path.GetFileNameWithoutExtension(jsxFilepath.FullName) + ".jsxbin");
        }

        private string ReadFile(string filepath)
        {
            return File.ReadAllText(filepath, System.Text.Encoding.UTF8);
        }
    }
}
