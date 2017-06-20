using System;
using Common;

namespace Chronos.Models
{
    public class ChronosEvents : EventArgs
    {
        public ResXEntry JobItem { get; set; }
    }
}
