using System.Collections.Generic;
using System.Linq;

namespace Engine {

	public class CodeTester {

		public bool AreEqual(Code guess, Code answer) {
			return guess.One == answer.One
				&& guess.Two == answer.Two
				&& guess.Three == answer.Three
				&& guess.Four == answer.Four;
		}

		public CodeResponse Test(Code guess, Code answer) {
			var list = generateListOfResponseColors(guess, answer);
			var response = generateResponse(list);
			return response;
		}

		private static List<ResponseColors> generateListOfResponseColors(Code guess, Code answer) {
			var list = new List<ResponseColors>();
			var answerColors = answer.ToColorList();
			var guessColors = guess.ToColorList();

			getResponse(guess.One, answer.One, list, answerColors);
			getResponse(guess.Two, answer.Two, list, answerColors);
			getResponse(guess.Three, answer.Three, list, answerColors);
			getResponse(guess.Four, answer.Four, list, answerColors);

			while (list.Count() < 4) {
				list.Add(ResponseColors.None);
			}
			return list;
		}

		private static void getResponse(CodeColors guessColor, CodeColors answerColor, List<ResponseColors> list, List<CodeColors> answerColors) {
			if (guessColor == answerColor) {
				list.Add(ResponseColors.Red);
			} else {
				//is maybe white?
				if (answerColors.Contains(guessColor)) {
					list.Add(ResponseColors.White);
				} else {
					list.Add(ResponseColors.None);
				}
			}
		}

		private static CodeResponse generateResponse(List<ResponseColors> list) {
			list = list
					.OrderBy(x => (int)(x))
					.ToList();
			var response = new CodeResponse();
			response.One = list[0];
			response.Two = list[1];
			response.Three = list[2];
			response.Four = list[3];

			return response;
		}
	}
}