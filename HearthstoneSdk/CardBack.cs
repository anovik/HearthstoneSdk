using System.Collections.Generic;

namespace HearthstoneSdk
{
    public class CardBack
    {
        public int id { get; set; }
        public int sortCategory { get; set; }
        public string text { get; set; }
        public string image { get; set; }
        public string slug { get; set; }
    }

    public class CardBacksCollection
    {
        public List<CardBack> cardbacks { get; set; }
    }
}