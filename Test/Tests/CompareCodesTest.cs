using System.IO;
using System.Reflection;
using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            c.One = a.One;
            c.Two = a.Two;
            c.Three = a.Three;
            c.Four = a.Four;
            Assert.IsTrue(codeTester.AreEqual(a, c));
        }
    }
}