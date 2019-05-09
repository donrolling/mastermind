using Engine.Model;
using System.Collections.Generic;

namespace Engine {

	public class GameState {
		public bool CodeBroken { get; private set; }
		public bool GameOver { get; private set; }

		public string GameOverMessage {
			get {
				return getGameOverMessage();
			}
		}

		public List<Turn> Turns;
		
		private Code _code;
		private CodeMaker _codeMaker;
		private CodeTester _codeTester;
		private int _numberOfTurns = 12;//can be 12, 10 or 8

		public GameState() {
			this._codeMaker = new CodeMaker();
			this._codeTester = new CodeTester();
			this.Turns = new List<Turn>();
			//this._gamesToPlay = 10;//must be even - players decide this
			this._code = this._codeMaker.Create();
		}

		public GameState(Code code) : this() {
			this._code = code;
		}

		public CodeResponse Guess(Code guess) {
			if (this.Turns.Count >= this._numberOfTurns) {
				throw new System.Exception(getGameOverMessage());
			}
			var response = this._codeTester.Test(guess, this._code);
			this.Turns.Add(new Turn { Code = guess, CodeResponse = response });
			if (response.CorrectGuess) {
				this.CodeBroken = true;
				this.GameOver = true;
			}
			if (this.Turns.Count >= this._numberOfTurns) {
				this.CodeBroken = false;
				this.GameOver = true;
			}
			return response;
		}

		private string getGameOverMessage() {
			return $"Game over. \r\nCodeBroken: { this.CodeBroken }\r\nCode: { this._code.ToString() }";
		}
	}
}