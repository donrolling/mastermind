using System;
using System.Collections.Generic;
using System.Text;

namespace Engine {
	public static class ResponseExtensions {
		public static List<ResponseColors> ToColorList(this CodeResponse codeResponse) {
			var responseColors = new List<ResponseColors>{
				codeResponse.One,
				codeResponse.Two,
				codeResponse.Three,
				codeResponse.Four
			};
			return responseColors;
		}
	}
}
