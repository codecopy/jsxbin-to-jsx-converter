using jsxbin_to_jsx.JsxbinDecoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jsxbin_to_jsx.Tests
{
    [TestClass]
    public class JsxbinToJsxTests
    {
        [TestMethod]
        public void TestVersion1()
        {
            ExecuteTests("v1.0");
        }

        [TestMethod]
        public void TestVersion2()
        {
            ExecuteTests("v2.0");
        }

        private void ExecuteTests(string version)
        {
            foreach (var p in new Testdata().ReadTestfiles(version))
            {
                string actualJsx = AbstractNode.Decode(p.Jsxbin, false);
                Assert.AreEqual(p.Jsx, actualJsx, string.Format("Decoding JSXBIN {0} does not match expected output in {1}.", version, p.JsxFilename));
            }
        }
    }
}
