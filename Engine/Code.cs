using System.Collections.Generic;

namespace Engine {

	public class Code {
		public CodeColors Four { get; set; }
		public CodeColors One { get; set; }
		public CodeColors Three { get; set; }
		public CodeColors Two { get; set; }

		public override string ToString() {
			return $"{ One }, { Two }, { Three }, { Four }";
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
	}
}