using Engine.Factory;
using Engine.Model;
using Engine.Service;
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
		private int _numberOfTurns = 12;//can be 12, 10 or 8

		public GameState() {
			this.Turns = new List<Turn>();
			//this._gamesToPlay = 10;//must be even - players decide this
			this._code = CodeMaker.Create();
		}

		public GameState(Code code) : this() {
			this._code = code;
		}

		public CodeResponse Guess(Code guess) {
			if (this.Turns.Count >= this._numberOfTurns) {
				throw new System.Exception(getGameOverMessage());
			}
			var response = CodeTester.Test(guess, this._code);
			this.Turns.Add(new Turn { Code = guess, CodeResponse = response });
			if (CodeResponseFactory.CorrectGuess(response)) {
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