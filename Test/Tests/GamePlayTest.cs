using Engine;
using Engine.Factory;
using Engine.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {

	[TestClass]
	public class GamePlayTest {

		public GamePlayTest() {
		}

		[TestMethod]
		public void PlayGameUntilEnd() {
			var answer = new Code { One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Purple };
			var gameState = new GameState(answer);
			do {
				makeGuess(gameState, new Code { One = CodeColors.Red, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Purple });
				Assert.IsFalse(gameState.CodeBroken, "Code should not have been broken.");
			} while (!gameState.GameOver);
			Assert.IsFalse(gameState.CodeBroken, "Code should not have been broken.");
			Assert.AreEqual(12, gameState.Turns.Count, "Turn count should be 12.");
		}

		[TestMethod]
		public void BeginNewGame_GuessCodeIn5Steps_RecognizeEndGame() {
			var answer = new Code { One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Purple };
			var gameState = new GameState(answer);

			makeGuess(gameState, new Code { One = CodeColors.Red, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Purple });

			makeGuess(gameState, new Code { One = CodeColors.Yellow, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Purple });

			makeGuess(gameState, new Code { One = CodeColors.Purple, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Purple });

			makeGuess(gameState, new Code { One = CodeColors.White, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Purple });

			var correctGuess = new Code { One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Purple };
			var response = gameState.Guess(correctGuess);
			Assert.IsTrue(gameState.CodeBroken, "This should have been a correct guess.");
			Assert.IsTrue(CodeResponseFactory.CorrectGuess(response), "This should have been a correct guess.");
		}

		private static void makeGuess(GameState gameState, Code guess) {
			var response = gameState.Guess(guess);
			Assert.IsFalse(gameState.CodeBroken, "This should not have been a correct guess.");
			Assert.IsFalse(CodeResponseFactory.CorrectGuess(response), "This should not have been a correct guess.");
		}
	}
}