namespace Engine
{
    public class CodeTester
    {
        public bool AreEqual(Code a, Code b){
            return a.One == b.One
                && a.Two == b.Two
                && a.Three == b.Three
                && a.Four == b.Four;
        }
    }
}