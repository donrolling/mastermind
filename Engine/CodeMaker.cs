using Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine {

	public class CodeMaker {
		private static List<string> _colors;
		private static Random _random = new Random(Guid.NewGuid().GetHashCode());
		private static int Max = Enum.GetValues(typeof(CodeColors)).Cast<int>().Max();

		public CodeMaker() {
			if (_colors == null) {
				_colors = Enum.GetNames(typeof(CodeColors)).ToList();
			}
		}

		public Code Create() {
			var code = new Code();
			code.One = getRandomColor();
			code.Two = getRandomColor();
			code.Three = getRandomColor();
			code.Four = getRandomColor();
			return code;
		}

		private CodeColors getRandomColor() {
			var rand = _random.Next(1, Max);
			return (CodeColors)rand;
		}
	}
}