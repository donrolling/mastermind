using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine {

	public class CodeResponse {
		private bool _correctGuessIsSet = false;
		private bool _correctGuess = false;
		public bool CorrectGuess {
			get {
				if (_correctGuessIsSet) {
					return _correctGuess;
				}
				this._correctGuessIsSet = true;
				this._correctGuess = this.ResponseColorList.Where(a => a == ResponseColors.Red).Count() == 4;
				return this._correctGuess;
			}
		}

		public List<ResponseColors> ResponseColorList {
			get {
				if (_responseColors != null) {
					return _responseColors;
				}
				_responseColors = this.ToColorList();
				return _responseColors;
			}
		}

		public ResponseColors Four = ResponseColors.None;
		public ResponseColors One = ResponseColors.None;
		public ResponseColors Three = ResponseColors.None;
		public ResponseColors Two = ResponseColors.None;

		private List<ResponseColors> _responseColors;

		public override string ToString() {
			var delimiter = ", ";
			return String.Join(delimiter, this.ResponseColorList);
		}

		public List<ResponseColors> ToColorList() {
			var colors = new List<ResponseColors>{
				this.One,
				this.Two,
				this.Three,
				this.Four
			};
			return colors;
		}
	}
}