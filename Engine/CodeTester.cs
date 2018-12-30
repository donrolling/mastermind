using System.Collections.Generic;
using System.Linq;

namespace Engine {

	public class CodeTester {

		public bool AreEqual(Code a, Code b) {
			return a.One == b.One
				&& a.Two == b.Two
				&& a.Three == b.Three
				&& a.Four == b.Four;
		}

		public CodeResponse Test(Code guess, Code answer) {
			var list = generateListOfResponseColors(guess, answer);
			var response = generateResponse(list);
			return response;
		}

		private static List<ResponseColors> generateListOfResponseColors(Code guess, Code answer) {
			var list = new List<ResponseColors>();
			var answerColors = new List<CodeColors>{
				answer.One,
				answer.Two,
				answer.Three,
				answer.Four
			};
			var guessColors = new List<CodeColors>{
				guess.One,
				guess.Two,
				guess.Three,
				guess.Four
			};

			if (guess.One == answer.One) {
				list.Add(ResponseColors.Red);
			} else {
				//is maybe white?
				if (answerColors.Contains(guess.One)) {
					list.Add(ResponseColors.White);
				}
			}
			if (guess.Two == answer.Two) {
				list.Add(ResponseColors.Red);
			} else {
				//is maybe white?
				if (answerColors.Contains(guess.Two)) {
					list.Add(ResponseColors.White);
				}
			}
			if (guess.Three == answer.Three) {
				list.Add(ResponseColors.Red);
			} else {
				//is maybe white?
				if (answerColors.Contains(guess.Three)) {
					list.Add(ResponseColors.White);
				}
			}
			if (guess.Four == answer.Four) {
				list.Add(ResponseColors.Red);
			} else {
				//is maybe white?
				if (answerColors.Contains(guess.Four)) {
					list.Add(ResponseColors.White);
				}
			}
			while (list.Count() < 4) {
				list.Add(ResponseColors.None);
			}
			return list;
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