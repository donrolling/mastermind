using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Factory
{
    public static class CodeResponseFactory
    {
        public static string ToString(CodeResponse codeResponse)
        {
            var delimiter = ", ";
            return String.Join(delimiter, ToColorList(codeResponse));
        }

        public static List<ResponseColors> ToColorList(CodeResponse codeResponse)
        {
            var colors = new List<ResponseColors>{
                codeResponse.One,
                codeResponse.Two,
                codeResponse.Three,
                codeResponse.Four
            };
            return colors;
        }

        public static bool CorrectGuess(CodeResponse codeResponse)
        {
            return ToColorList(codeResponse).Where(a => a == ResponseColors.Red).Count() == 4;
        }
    }
}