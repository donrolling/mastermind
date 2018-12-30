namespace Test {

	public static class TestUtility {
		private static string _dllPath = System.AppDomain.CurrentDomain.BaseDirectory;

		public static string GetPath(string relativePath) {
			var basePath = _dllPath.Split("\\bin")[0];
			return $"{ basePath }\\{ relativePath.TrimStart('\\') }";
		}
	}
}