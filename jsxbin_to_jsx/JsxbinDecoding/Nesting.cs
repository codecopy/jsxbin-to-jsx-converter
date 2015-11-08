namespace jsxbin_to_jsx.JsxbinDecoding
{
    public sealed class Nesting
    {
        readonly ScanState _scanState;
        public Nesting(ScanState scanState)
        {
            Levels = 0;
            _scanState = scanState;
        }

        public int Levels { get; set; }

        public void DecrementLevel()
        {
            Levels--;
        }

        public void ParseLevels()
        {
            Levels = ParseLevelsCore();
        }

        private int ParseLevelsCore()
        {
            if (_scanState.IsHex(0x41))
            {
                _scanState.Inc();
                return 1;
            }
            if (_scanState.IsHex(0x30))
            {
                _scanState.Inc();
                int lvlsCrypted = _scanState.getCur();
                int lvls = lvlsCrypted - 0x3F;
                _scanState.Inc();
                if (lvls > 0x1B)
                {
                    return lvls + ParseLevelsCore();
                }
                else
                {
                    return lvls;
                }
            }
            return 0;
        }
    }
}
