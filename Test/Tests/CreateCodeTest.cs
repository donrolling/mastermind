using Engine;
using Engine.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Test;

namespace Tests {

	[TestClass]
	public class CreateCodeTest {

		[TestMethod]
		public void CreateNewCode() {
			var code = CodeMaker.Create();
			Assert.IsNotNull(code);
			//write it to a file, so I can read it
			var relativePath = "Output\\CreateNewCode.txt";
			var path = TestUtility.GetPath(relativePath);
			File.WriteAllText(path, code.ToString());
		}
	}
}