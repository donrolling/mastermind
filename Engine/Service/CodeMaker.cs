using Engine.Model;
using System;
using System.Linq;

namespace Engine.Service
{
    public static class CodeMaker
    {
        private static int Max = Enum.GetValues(typeof(CodeColors)).Cast<int>().Max();

        public static Code Create()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var code = new Code();
            code.One = getRandomColor(random);
            code.Two = getRandomColor(random);
            code.Three = getRandomColor(random);
            code.Four = getRandomColor(random);
            return code;
        }

        private static CodeColors getRandomColor(Random random)
        {
            var rand = random.Next(1, Max);
            return (CodeColors)rand;
        }
    }
}