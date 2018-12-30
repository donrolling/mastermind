using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Omu.ValueInjecter;
using Test;

namespace Tests
{
    [TestClass]
    public class CompareCodesTest
    {
        [TestMethod]
        public void CompareCodes(){
            var codeMaker = new CodeMaker();
            var codeTester = new CodeTester();
            var a = codeMaker.Create();
            var aCode = a.ToString();
            File.WriteAllText(TestUtility.GetPath("Output\\CompareCodes_A.txt"), aCode);
            var b = codeMaker.Create();
            var bCode = b.ToString();
            File.WriteAllText(TestUtility.GetPath("Output\\CompareCodes_B.txt"), bCode);
            //compare them as strings so that we know they aren't the same
            Assert.AreNotEqual(aCode, bCode);
            //use the tester to assert that they aren't the same
            Assert.IsFalse(codeTester.AreEqual(a, b));
            //compare two that are the same
            var c = new Code();
            c.InjectFrom(a);
            Assert.IsTrue(codeTester.AreEqual(a, c));
        }

        [TestMethod]
        public void CompareResponses_GivenCorrectAnswer(){
            var codeMaker = new CodeMaker();
            var answer = codeMaker.Create();
            var guess = new Code();
            guess.InjectFrom(answer);
            var codeTester = new CodeTester();
            var response = codeTester.Test(guess, answer);
            Assert.AreEqual(ResponseColors.Red, response.One);
            Assert.AreEqual(ResponseColors.Red, response.Two);
            Assert.AreEqual(ResponseColors.Red, response.Three);
            Assert.AreEqual(ResponseColors.Red, response.Four);
        }

        [TestMethod]
        public void CompareResponses_GivenAnswerWithTwoRedAndTwoEmptyResponses(){
            var answer = new Code{ One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Red };
            var guess = new Code{ One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.White, Four = CodeColors.Yellow };
            var codeTester = new CodeTester();
            var response = codeTester.Test(guess, answer);
            var responseColors = new List<ResponseColors>{
                response.One,
                response.Two,
                response.Three,
                response.Four
            };
            var delimiter = ", ";
            var sb = String.Join(delimiter, responseColors);
            File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_GivenAnswerWithTwoRedAndTwoWhite.txt"), sb);
            Assert.AreEqual(2, responseColors.Where(a => a == ResponseColors.Red).Count(), "There should be 2 Red responses.");
            Assert.AreEqual(2, responseColors.Where(a => a == ResponseColors.None).Count(), "There should be 2 None responses.");
        }

        [TestMethod]
        public void CompareResponses_GivenAnswerWithTwoRedAndTwoWhiteResponses(){
            var answer = new Code{ One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Red };
            var guess = new Code{ One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Red, Four = CodeColors.Purple };
            var codeTester = new CodeTester();
            var response = codeTester.Test(guess, answer);
            var responseColors = new List<ResponseColors>{
                response.One,
                response.Two,
                response.Three,
                response.Four
            };
            var delimiter = ", ";
            var sb = String.Join(delimiter, responseColors);
            File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_GivenAnswerWithTwoRedAndTwoWhiteResponses.txt"), sb);
            Assert.AreEqual(2, responseColors.Where(a => a == ResponseColors.Red).Count(), "There should be 2 Red responses.");
            Assert.AreEqual(2, responseColors.Where(a => a == ResponseColors.White).Count(), "There should be 2 White responses.");
        }


        [TestMethod]
        public void CompareResponses_RedsShouldBeReturnedFirstThenWhitesThenNones(){
            var answer = new Code{ One = CodeColors.Green, Two = CodeColors.Orange, Three = CodeColors.Purple, Four = CodeColors.Red };
            var guess = new Code{ One = CodeColors.Green, Two = CodeColors.Red, Three = CodeColors.Yellow, Four = CodeColors.Yellow };
            var codeTester = new CodeTester();
            var response = codeTester.Test(guess, answer);
            var responseColors = new List<ResponseColors>{
                response.One,
                response.Two,
                response.Three,
                response.Four
            };
            var delimiter = ", ";
            var sb = String.Join(delimiter, responseColors);
            File.WriteAllText(TestUtility.GetPath("Output\\CompareResponses_RedsShouldBeReturnedFirstThenWhitesThenNones.txt"), sb);
            Assert.AreEqual(1, responseColors.Where(a => a == ResponseColors.Red).Count(), "There should be 1 Red responses.");
            Assert.AreEqual(1, responseColors.Where(a => a == ResponseColors.White).Count(), "There should be 1 White responses.");
            Assert.AreEqual(2, responseColors.Where(a => a == ResponseColors.None).Count(), "There should be 2 None responses.");
        }
    }
}