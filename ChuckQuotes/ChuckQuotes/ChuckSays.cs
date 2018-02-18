using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ChuckQuotes
{
    public class ChuckSays
    {
        public string type { get; set; }
        public Joke value { get; set; }

        public class Joke
        {
            public int id { get; set; }
            public string joke { get; set; }
            public string[] categories { get; set; }
        }
    }
}