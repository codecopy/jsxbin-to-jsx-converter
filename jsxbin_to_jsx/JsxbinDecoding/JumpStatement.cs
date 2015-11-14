using System;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class JumpStatement : AbstractNode, IStatement
    {
        LineInfo labelInfo;
        string jmpLocation;
        bool isBreakStatement;

        public int LineNumber
        {
            get
            {
                return labelInfo.LineNumber;
            }
        }

        public override string Marker
        {
            get { return Convert.ToChar(0x44).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.JumpStatement;
            }
        }

        public override void Decode()
        {
            labelInfo = DecodeLineInfo();
            jmpLocation = DecodeId();
            isBreakStatement = DecodeBool();
        }
        
        public override string PrettyPrint()
        {
            string jmp = labelInfo.CreateLabelStmt();
            if (isBreakStatement)
            {
                jmp += "break " + jmpLocation;
            }
            else
            {
                jmp += "continue " + jmpLocation;
            }
            jmp += ";";
            return jmp;
        }
    }
}
