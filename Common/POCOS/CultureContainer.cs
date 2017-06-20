using System;
using System.Globalization;

namespace Common.POCOS
{
    public class CultureContainer
    {
        public CultureInfo Culture { get; set; }
        public bool Existing { get; set; }

        public String Name => Culture.DisplayName;

        public String Value => Culture.Name;
    }
}
