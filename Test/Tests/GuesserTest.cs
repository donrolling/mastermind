using Engine;
using Engine.Factory;
using Engine.Model;
using Engine.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {

	[TestClass]
	public class GuesserTest {
		public Code Answer { get; }

		public GameState GameState { get; }
		public CodeGuesser Guesser { get; }

		public GuesserTest() {
			this.Answer = new Code { One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Yellow, Four = CodeColors.Purple };
			this.GameState = new GameState(this.Answer);
			this.Guesser = new CodeGuesser();
		}

		[TestMethod]
		public void MakeGuesses() {
			var hadCorrectGuess = false;
			do {
				var guess = this.Guesser.MakeGuess(this.GameState);
				var response = this.GameState.Guess(guess);
				if (CodeResponseFactory.CorrectGuess(response)) {
					hadCorrectGuess = true;
					Assert.IsTrue(this.GameState.GameOver);
				}
			} while (!this.GameState.GameOver);
			Assert.IsTrue(hadCorrectGuess, "The Guesser should have guessed correctly before the game is over, but has not.");
		}
	}
}