using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Test;

namespace Tests {

	[TestClass]
	public class CreateCodeTest {

		[TestMethod]
		public void CreateNewCode() {
			var codeMaker = new CodeMaker();
			var code = codeMaker.Create();
			Assert.IsNotNull(code);
			//write it to a file, so I can read it
			var relativePath = "Output\\CreateNewCode.txt";
			var path = TestUtility.GetPath(relativePath);
			File.WriteAllText(path, code.ToString());
		}
	}
}