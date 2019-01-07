using System.Collections.Generic;

namespace Engine {

	public class Code {
		public CodeColors Four { get; set; }
		public CodeColors One { get; set; }
		public CodeColors Three { get; set; }
		public CodeColors Two { get; set; }

		public Code() {

		}

		public Code(List<CodeColors> colors) {
			this.One = colors[0];
			this.Two = colors[1];
			this.Three = colors[2];
			this.Four = colors[3];
		}

		public List<CodeColors> ToColorList() {
			var colors = new List<CodeColors>{
				this.One,
				this.Two,
				this.Three,
				this.Four
			};
			return colors;
		}

		public override string ToString() {
			return $"{ One }, { Two }, { Three }, { Four }";
		}
	}
}