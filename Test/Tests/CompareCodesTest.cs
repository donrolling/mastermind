using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Omu.ValueInjecter;
using System.IO;
using System.Linq;
using Test;

namespace Tests {

	[TestClass]
	public class CompareCodesTest {
		public CodeMaker CodeMaker { get; private set; }
		public CodeTester CodeTester { get; private set; }

		public CompareCodesTest() {
			this.CodeMaker = new CodeMaker();
			this.CodeTester = new CodeTester();
		}

		[TestMethod]
		public void CompareCodes() {
			var a = this.CodeMaker.Create();
			var aCode = a.ToString();
			File.WriteAllText(TestUtility.GetPath("Output\\CompareCodes_A.txt"), aCode);
			var b = this.CodeMaker.Create();
			var bCode = b.ToString();
			File.WriteAllText(TestUtility.GetPath("Output\\CompareCodes_B.txt"), bCode);
			//compare them as strings so that we know they aren't the same
			Assert.AreNotEqual(aCode, bCode);
			//use the tester to assert that they aren't the same
			Assert.IsFalse(this.CodeTester.AreEqual(a, b));
			//compare two that are the same
			var c = new Code();
			c.InjectFrom(a);
			Assert.IsTrue(this.CodeTester.AreEqual(a, c));
		}

		[TestMethod]
		public void CompareResponses_AllNonesShouldBeReturned() {
			var answer = new Code { One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Purple };
			var guess = new Code { One = CodeColors.Red, Two = CodeColors.Red, Three = CodeColors.Red, Four = CodeColors.Red };

			var response = this.CodeTester.Test(guess, answer);
			File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_AllNonesShouldBeReturned.txt"), response.ToString());
			Assert.AreEqual(0, response.ResponseColorList.Where(a => a == ResponseColors.Red).Count(), "There should be 0 Red responses.");
			Assert.AreEqual(0, response.ResponseColorList.Where(a => a == ResponseColors.White).Count(), "There should be 0 White responses.");
			Assert.AreEqual(4, response.ResponseColorList.Where(a => a == ResponseColors.None).Count(), "There should be 4 None responses.");
		}

		[TestMethod]
		public void CompareResponses_GivenAnswerWithTwoRedAndTwoEmptyResponses() {
			var answer = new Code { One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Red };
			var guess = new Code { One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.White, Four = CodeColors.Yellow };

			var response = this.CodeTester.Test(guess, answer);
			File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_GivenAnswerWithTwoRedAndTwoWhite.txt"), response.ToString());
			Assert.AreEqual(2, response.ResponseColorList.Where(a => a == ResponseColors.Red).Count(), "There should be 2 Red responses.");
			Assert.AreEqual(2, response.ResponseColorList.Where(a => a == ResponseColors.None).Count(), "There should be 2 None responses.");
		}

		[TestMethod]
		public void CompareResponses_GivenAnswerWithTwoRedAndTwoWhiteResponses() {
			var answer = new Code { One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Red };
			var guess = new Code { One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Red, Four = CodeColors.Purple };

			var response = this.CodeTester.Test(guess, answer);
			File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_GivenAnswerWithTwoRedAndTwoWhiteResponses.txt"), response.ToString());
			Assert.AreEqual(2, response.ResponseColorList.Where(a => a == ResponseColors.Red).Count(), "There should be 2 Red responses.");
			Assert.AreEqual(2, response.ResponseColorList.Where(a => a == ResponseColors.White).Count(), "There should be 2 White responses.");
		}

		[TestMethod]
		public void CompareResponses_GivenCorrectAnswer() {
			var answer = this.CodeMaker.Create();
			var guess = new Code();
			guess.InjectFrom(answer);

			var response = this.CodeTester.Test(guess, answer);
			Assert.AreEqual(ResponseColors.Red, response.One);
			Assert.AreEqual(ResponseColors.Red, response.Two);
			Assert.AreEqual(ResponseColors.Red, response.Three);
			Assert.AreEqual(ResponseColors.Red, response.Four);
		}

		[TestMethod]
		public void CompareResponses_RedsShouldBeReturnedFirstThenWhitesThenNones() {
			var answer = new Code { One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Red };
			var guess = new Code { One = CodeColors.Green, Two = CodeColors.Red, Three = CodeColors.Yellow, Four = CodeColors.Yellow };
			var response = this.CodeTester.Test(guess, answer);
			File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_RedsShouldBeReturnedFirstThenWhitesThenNones.txt"), response.ToString());
			Assert.AreEqual(1, response.ResponseColorList.Where(a => a == ResponseColors.Red).Count(), "There should be 1 Red responses.");
			Assert.AreEqual(1, response.ResponseColorList.Where(a => a == ResponseColors.White).Count(), "There should be 1 White responses.");
			Assert.AreEqual(2, response.ResponseColorList.Where(a => a == ResponseColors.None).Count(), "There should be 2 None responses.");
		}
	}
}