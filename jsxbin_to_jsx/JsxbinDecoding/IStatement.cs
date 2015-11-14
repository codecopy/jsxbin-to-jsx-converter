using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public interface IStatement
    {
        int LineNumber { get; }
    }
}
