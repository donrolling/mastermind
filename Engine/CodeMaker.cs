using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine {

	public class CodeMaker {
		private static List<string> _colors;
		private static Random _random = new Random(Guid.NewGuid().GetHashCode());

		public CodeMaker() {
			if (_colors == null) {
				_colors = Enum.GetNames(typeof(CodeColors)).ToList();
			}
		}

		public Code Create() {
			var code = new Code();
			var usedColors = new List<int>();
			code.One = getRandomUnusedColor(usedColors);
			code.Two = getRandomUnusedColor(usedColors);
			code.Three = getRandomUnusedColor(usedColors);
			code.Four = getRandomUnusedColor(usedColors);
			return code;
		}

		private CodeColors getRandomUnusedColor(List<int> usedColors) {
			//var max = Enum.GetValues(typeof(Colors)).Cast<int>().Max();
			//var max = _colors.Count() - 1;
			var max = 5;
			var rand = _random.Next(0, max);
			//recurse until we get an unused color
			// if(usedColors.Contains(rand)){
			//     return getRandomUnusedColor(usedColors);
			// }
			usedColors.Add(rand);
			return (CodeColors)rand;
		}
	}
}