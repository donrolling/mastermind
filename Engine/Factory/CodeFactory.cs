using Engine.Model;
using System.Collections.Generic;

namespace Engine.Factory
{
    public static class CodeFactory
    {
        public static Code Create(CodeColors one, CodeColors two, CodeColors three, CodeColors four)
        {
            var code = new Code();
            code.One = one;
            code.Two = two;
            code.Three = three;
            code.Four = four;
            return code;
        }

        public static Code Create(List<CodeColors> colors)
        {
            var code = new Code();
            code.One = colors[0];
            code.Two = colors[1];
            code.Three = colors[2];
            code.Four = colors[3];
            return code;
        }

        public static List<CodeColors> ToColorList(Code code)
        {
            var colors = new List<CodeColors>{
                code.One,
                code.Two,
                code.Three,
                code.Four
            };
            return colors;
        }

        public static string ToString(Code code)
        {
            return $"{ code.One }, { code.Two }, { code.Three }, { code.Four }";
        }
    }
}