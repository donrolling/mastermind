using System.IO;
using System.Reflection;
using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test;

namespace Tests
{
    [TestClass]
    public class CreateCodeTest
    {
        [TestMethod]
        public void CreateNewCode(){
            var codeMaker = new CodeMaker();
            var code = codeMaker.Create();
            Assert.IsNotNull(code);
            //write it to a file, so I can read it
            var relativePath = "Output\\CreateNewCode.txt";
            var path = TestUtility.GetPath(relativePath);
            File.WriteAllText(path, code.ToString());
            //ensure no duplicates
            Assert.AreNotEqual(code.One, code.Two);
            Assert.AreNotEqual(code.One, code.Three);
            Assert.AreNotEqual(code.One, code.Four);
            Assert.AreNotEqual(code.Two, code.Three);
            Assert.AreNotEqual(code.Two, code.Four);
            Assert.AreNotEqual(code.Three, code.Four);
        }
    }
}