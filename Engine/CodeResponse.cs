using System;
using System.Collections.Generic;

namespace Engine {

	public class CodeResponse {
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
	}
}