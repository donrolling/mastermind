using Engine.Model;
using System;

namespace Engine.Service {

	public class CodeGuesser {

		public Code MakeGuess(GameState gameState) {
			var isFirstGuess = gameState.Turns.Count == 0;
			if (isFirstGuess) {
				return this.makeFirstGuess();
			}
			return this.makeGuess(gameState);
		}

		private Code makeFirstGuess() {
			return new Code {
				One = CodeColors.Red,
				Two = CodeColors.Yellow,
				Three = CodeColors.Green,
				Four = CodeColors.White
			};
		}

		private Code makeGuess(GameState gameState) {
			throw new NotImplementedException();
		}
	}
}