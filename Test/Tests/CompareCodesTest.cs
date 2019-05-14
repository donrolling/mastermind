using Engine;
using Engine.Factory;
using Engine.Model;
using Engine.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Omu.ValueInjecter;
using System.IO;
using System.Linq;
using Test;

namespace Tests {

	[TestClass]
	public class CompareCodesTest {
		[TestMethod]
		public void CompareCodes() {
			var a = CodeMaker.Create();
			var aCode = CodeFactory.ToString(a);
			File.WriteAllText(TestUtility.GetPath("Output\\CompareCodes_A.txt"), aCode);
			var b = CodeMaker.Create();
			var bCode = CodeFactory.ToString(b);
			File.WriteAllText(TestUtility.GetPath("Output\\CompareCodes_B.txt"), bCode);
			//compare them as strings so that we know they aren't the same
			Assert.AreNotEqual(aCode, bCode);
			//use the tester to assert that they aren't the same
			Assert.IsFalse(CodeTester.AreEqual(a, b));
			//compare two that are the same
			var c = new Code();
			c.InjectFrom(a);
			Assert.IsTrue(CodeTester.AreEqual(a, c));
		}

		[TestMethod]
		public void CompareResponses_AllNonesShouldBeReturned() {
			var answer = CodeFactory.Create(CodeColors.Green, CodeColors.Orange, CodeColors.Purple, CodeColors.Purple);
			var guess = CodeFactory.Create(CodeColors.Red, CodeColors.Red, CodeColors.Red, CodeColors.Red);

			var response = CodeTester.Test(guess, answer);
            var colorList = CodeResponseFactory.ToColorList(response);
            File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_AllNonesShouldBeReturned.txt"), response.ToString());
			Assert.AreEqual(0, colorList.Where(a => a == ResponseColors.Red).Count(), "There should be 0 Red responses.");
			Assert.AreEqual(0, colorList.Where(a => a == ResponseColors.White).Count(), "There should be 0 White responses.");
			Assert.AreEqual(4, colorList.Where(a => a == ResponseColors.None).Count(), "There should be 4 None responses.");
		}

		[TestMethod]
		public void CompareResponses_GivenAnswerWithTwoRedAndTwoEmptyResponses() {
			var answer = CodeFactory.Create(CodeColors.Green, CodeColors.Orange, CodeColors.Purple, CodeColors.Red);
			var guess = CodeFactory.Create(CodeColors.Green, CodeColors.Orange, CodeColors.White, CodeColors.Yellow);

			var response = CodeTester.Test(guess, answer);
            var colorList = CodeResponseFactory.ToColorList(response);
            File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_GivenAnswerWithTwoRedAndTwoWhite.txt"), response.ToString());
			Assert.AreEqual(2, colorList.Where(a => a == ResponseColors.Red).Count(), "There should be 2 Red responses.");
			Assert.AreEqual(2, colorList.Where(a => a == ResponseColors.None).Count(), "There should be 2 None responses.");
		}

		[TestMethod]
		public void CompareResponses_GivenAnswerWithTwoRedAndTwoWhiteResponses() {
			var answer = CodeFactory.Create(CodeColors.Green, CodeColors.Orange, CodeColors.Purple, CodeColors.Red);
			var guess = CodeFactory.Create(CodeColors.Green, CodeColors.Orange, CodeColors.Red, CodeColors.Purple);

			var response = CodeTester.Test(guess, answer);
            var colorList = CodeResponseFactory.ToColorList(response);
            File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_GivenAnswerWithTwoRedAndTwoWhiteResponses.txt"), response.ToString());
			Assert.AreEqual(2, colorList.Where(a => a == ResponseColors.Red).Count(), "There should be 2 Red responses.");
			Assert.AreEqual(2, colorList.Where(a => a == ResponseColors.White).Count(), "There should be 2 White responses.");
		}

		[TestMethod]
		public void CompareResponses_GivenCorrectAnswer() {
			var answer = CodeMaker.Create();
			var guess = new Code();
			guess.InjectFrom(answer);

			var response = CodeTester.Test(guess, answer);
			Assert.AreEqual(ResponseColors.Red, response.One);
			Assert.AreEqual(ResponseColors.Red, response.Two);
			Assert.AreEqual(ResponseColors.Red, response.Three);
			Assert.AreEqual(ResponseColors.Red, response.Four);
		}

		[TestMethod]
		public void CompareResponses_DoesNotReturnMultipleCorrectHitsOnASingularGuess() {
			var answer = CodeFactory.Create(CodeColors.Green, CodeColors.Green, CodeColors.Purple, CodeColors.Red);
			var guess = CodeFactory.Create(CodeColors.Orange, CodeColors.Yellow, CodeColors.Green, CodeColors.White);
			var response = CodeTester.Test(guess, answer);
            var colorList = CodeResponseFactory.ToColorList(response);
            File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_DoesNotReturnMultipleCorrectHitsOnASingularGuess.txt"), response.ToString());
            Assert.AreEqual(1, colorList.Where(a => a == ResponseColors.White).Count(), "There should be 1 White responses.");
			Assert.AreEqual(3, colorList.Where(a => a == ResponseColors.None).Count(), "There should be 3 None responses.");
		}

		[TestMethod]
		public void CompareResponses_ShouldReturnOneRedOneWhiteAndThenTwoNonesInThatOrder() {
			var answer = CodeFactory.Create(CodeColors.Green, CodeColors.Orange, CodeColors.Purple, CodeColors.Red);
			var guess = CodeFactory.Create(CodeColors.Green, CodeColors.Red, CodeColors.Yellow, CodeColors.Yellow);
			var response = CodeTester.Test(guess, answer);
            var colorList = CodeResponseFactory.ToColorList(response);
            File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_ShouldReturnOneRedOneWhiteAndThenTwoNonesInThatOrder.txt"), response.ToString());
			Assert.AreEqual(1, colorList.Where(a => a == ResponseColors.Red).Count(), "There should be 1 Red responses.");
			Assert.AreEqual(1, colorList.Where(a => a == ResponseColors.White).Count(), "There should be 1 White responses.");
			Assert.AreEqual(2, colorList.Where(a => a == ResponseColors.None).Count(), "There should be 2 None responses.");
		}
	}
}