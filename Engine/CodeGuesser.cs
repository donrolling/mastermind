using System;

namespace Engine {

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
				One = CodeColors.Green,
				Two = CodeColors.Green,
				Three = CodeColors.Green,
				Four = CodeColors.Green
			};
		}

		private Code makeGuess(GameState gameState) {
			throw new NotImplementedException();
		}
	}
}