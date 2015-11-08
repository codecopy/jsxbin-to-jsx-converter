using System;
using System.Collections.Generic;
namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ScanState
    {
        int _index = 0;
        string _input;
        Nesting nesting;
        SymbolTable table = new SymbolTable();
        public ScanState(string input)
        {
            _input = input;
            nesting = new Nesting(this);
        }

        public ScanState(string input, int index)
        {
            _input = input;
            _index = index;
        }

        public void AddSymbol(string key, string value)
        {
            table.Add(key, value);
        }

        public string GetSymbol(string key)
        {
            return table.Get(key);
        }

        public void Inc()
        {
            _index++;
        }

        public char getCur()
        {
            return _input[_index];
        }

        public bool IsHex(int num)
        {
            return getCur() == Convert.ToChar(num);
        }

        public ScanState Clone()
        {
            return new ScanState(_input, _index);
        }

        public int GetNestingLevels()
        {
            if (nesting.Levels == 0)
            {
                nesting.ParseLevels();
            }
            return nesting.Levels;
        }

        public void DecrementNestingLevels()
        {
            if (nesting.Levels == 0)
            {
                return;
            }
            nesting.DecrementLevel();
        }

        public string Progress
        {
            get
            {
                string m = "****";
                string copy = _input;
                copy = copy.Insert(_index, m);
                copy = copy.Insert(_index + m.Length + 1, m);
                return copy;
            }
        }

        public string GetAndOrAddId(List<string> id)
        {
            string propName = "";
            if (id.Count == 3)
            {
                propName = id[1];
                AddSymbol(id[2], propName);
            }
            else
            {
                propName = GetSymbol(id[0]);
            }
            return propName;
        }
    }
}
