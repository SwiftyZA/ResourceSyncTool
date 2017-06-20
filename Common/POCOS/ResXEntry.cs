using Common.Enums;

namespace Common
{
    public class ResXEntry
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public string SourceFile { get; set; }
        public string LocalizedValue { get; set; }
        public string LocalizedComment { get; set; }
        public State State { get; set; }
    }
}
