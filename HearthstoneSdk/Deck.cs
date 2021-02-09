using Newtonsoft.Json;
using System.Collections.Generic;

namespace HearthstoneSdk
{
    public class Duels
    {
        public bool relevant { get; set; }
        public bool constructed { get; set; }
    }

    public class DeckClass
    {
        public string slug { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }


    public class Hero
    {
        public int id { get; set; }
        public int collectible { get; set; }
        public string slug { get; set; }
        public int classId { get; set; }
        public List<int> multiClassIds { get; set; }
        public int cardTypeId { get; set; }
        public int cardSetId { get; set; }
        public int rarityId { get; set; }
        public string artistName { get; set; }
        public int health { get; set; }
        public int manaCost { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public string image { get; set; }
        public string imageGold { get; set; }
        public string flavorText { get; set; }
        public string cropImage { get; set; }
        public List<int> keywordIds { get; set; }
        public List<int> childIds { get; set; }
        public Duels duels { get; set; }
    }

    public class HeroPower
    {
        public int id { get; set; }
        public int collectible { get; set; }
        public string slug { get; set; }
        public int classId { get; set; }
        public List<int> multiClassIds { get; set; }
        public int cardTypeId { get; set; }
        public int cardSetId { get; set; }
        public int rarityId { get; set; }
        public string artistName { get; set; }    
        public int manaCost { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public string image { get; set; }
        public string imageGold { get; set; }
        public string flavorText { get; set; }
        public string cropImage { get; set; }
        public int parentId { get; set; }
    }

    public class Deck
    {
        public string deckCode { get; set; }
        public int version { get; set; }

        public string format { get; set; }

        public Hero hero { get; set; }

        public HeroPower heroPower { get; set; }

        [JsonProperty("class")]
        public DeckClass deckClass { get; set;}

        public List<Card> cards { get; set; }
    }
}
