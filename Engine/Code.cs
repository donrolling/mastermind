namespace Engine
{
    public class Code
    {
        public CodeColors One { get; set; }
        public CodeColors Two { get; set; }
        public CodeColors Three { get; set; }
        public CodeColors Four { get; set; }

        public override string ToString(){
            return $"{ One }, { Two }, { Three }, { Four }";
        }
    }
}