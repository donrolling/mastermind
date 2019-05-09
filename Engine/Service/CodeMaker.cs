using Engine.Model;
using System;
using System.Linq;

namespace Engine.Service
{
    public static class CodeMaker
    {
        private static Random _random = new Random(Guid.NewGuid().GetHashCode());
        private static int Max = Enum.GetValues(typeof(CodeColors)).Cast<int>().Max();

        public static Code Create()
        {
            var code = new Code();
            code.One = getRandomColor();
            code.Two = getRandomColor();
            code.Three = getRandomColor();
            code.Four = getRandomColor();
            return code;
        }

        private static CodeColors getRandomColor()
        {
            var rand = _random.Next(1, Max);
            return (CodeColors)rand;
        }
    }
}