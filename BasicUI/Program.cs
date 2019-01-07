using Engine;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BasicUI {

	public class Program {

		private static void Main(string[] args) {
			Console.WriteLine("Mastermind");
			play();
		}

		private static Code parseGuess(string guess) {
			var xs = guess.ToLower().Split(' ');
			var colors = new List<CodeColors>();
			foreach (var x in xs) {
				switch (x) {
					case "y":
						colors.Add(CodeColors.Yellow);
						break;
					case "p":
						colors.Add(CodeColors.Purple);
						break;
					case "o":
						colors.Add(CodeColors.Orange);
						break;
					case "r":
						colors.Add(CodeColors.Red);
						break;
					case "g":
						colors.Add(CodeColors.Green);
						break;
					case "w":
						colors.Add(CodeColors.White);
						break;
					default:
						Console.WriteLine("Your code contained an invalid color.");
						return null;
				}
			}
			return new Code(colors);
		}

		private static void play() {
			var colors = Enum.GetNames(typeof(CodeColors));
			Console.WriteLine($"Guess four of these colors: { String.Join(", ", colors) }");
			var gameState = new GameState();
			do {
				var code = getGuess();
				var guessResult = gameState.Guess(code);
				Console.WriteLine($"My Response: { guessResult.One } { guessResult.Two } { guessResult.Three } { guessResult.Four }");
			} while (!gameState.GameOver);

			Console.WriteLine(gameState.GameOverMessage);
			playAgain();
		}

		private static Code getGuess() {
			var input = dealWithInput();
			var code = parseGuess(input);
			if (code == null) { return getGuess(); }
			return code;
		}

		private static string dealWithInput() {
			Console.WriteLine("Try to guess my code. Example: R W G Y");
			var guess = Console.ReadLine();
			var xs = guess.Split(' ');
			var invalid = false;
			if (xs.Length > 4) {
				Console.WriteLine("Too many colors.");
				invalid = true;
			}
			if (xs.Length < 4) {
				Console.WriteLine("Not enought colors.");
				invalid = true; 
			}
			if (invalid) {
				return dealWithInput();
			}
			return guess;
		}

		private static void playAgain() {
			Console.WriteLine("Play again? Y | N");
			var doPlayAgain = Console.ReadLine();
			if (doPlayAgain.ToLower() == "y") {
				play();
			} else if (doPlayAgain.ToLower() != "n") {
				Console.WriteLine("Huh?");
				playAgain();
			}
			Console.WriteLine("Goodbye");
			Thread.Sleep(3000);
		}
	}
}