using jsxbin_to_jsx.JsxbinDecoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jsxbin_to_jsx.Tests
{
    [TestClass]
    public class JsxbinToJsxTests
    {
        [TestMethod]
        public void TestJsxbinToJsxConversion()
        {
            foreach(var p in new Testdata().ReadTestfiles())
            {
                string actualJsx = AbstractNode.Decode(p.Jsxbin);
                Assert.AreEqual(p.Jsx, actualJsx, string.Format("Decoding JSXBIN does not match expected output in {0}.", p.JsxFilename));
            }
        }
    }
}
