using HearthstoneSdk;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace HearthstoneSdkTests
{
    public class HearthstoneSdkTests
    {
        Hearthstone _sdk = new Hearthstone();

        // !!!put your client id and client secret from Blizzard Developer portal here
        const string _clientId = "PUT_YOUR_CLIENT_ID_HERE";
        const string _clientSecret = "PUT_YOUR_CLIENT_SECRET_HERE";

        string _accessToken;

        [SetUp]
        public async Task Setup()
        {
            _accessToken = await _sdk.GetAccessToken(Region.us, _clientId, _clientSecret);
        }      

        [Test]
        public async Task GetAccessTokenTest()
        {            
            string accessToken = await _sdk.GetAccessToken(Region.us, _clientId, _clientSecret);
            Assert.IsNotNull(accessToken);
        }

        [Test]
        public async Task GetCardsTest()
        {
            var cardsCollection = await _sdk.GetCards(Region.us, Locale.en_US, _accessToken);
            Assert.IsNotNull(cardsCollection);
            Assert.True(cardsCollection.cards.Count > 0);
        }

        [Test]
        public async Task GetCardsSearchTest()
        {
           var cardsCollection = await _sdk.GetCards(region: Region.us, 
                                           locale: Locale.en_US,
                                           accessToken: _accessToken,
                                           set: "rise-of-shadows",
                                           classString: "mage",
                                           manaCost: new List<int> {9},
                                           attack: new List<int> {4},
                                           health: new List<int> {12},
                                           collectible: new List<int> {1},
                                           rarity: "legendary",
                                           typeString: "minion",
                                           minionType: "dragon",
                                           keyword: "battlecry",
                                           textFilter: "kalecgos",
                                           pageSize: 5,
                                           sort: "name",
                                           order: "desc");
            Assert.IsNotNull(cardsCollection);
            Assert.True(cardsCollection.cards.Count == 1);
        }

        [Test]
        public async Task GetCardsBattlegroundSearchTest()
        {
            var cardsCollection = await _sdk.GetBattlegroundCards(region: Region.us,
                                         locale: Locale.en_US,
                                         accessToken: _accessToken,
                                         tier: new List<string>() { "hero", "3" } );
            Assert.IsNotNull(cardsCollection);
            Assert.True(cardsCollection.cards.Count > 0);
        }

        [Test]
        public async Task GetCardByIdTest()
        {
            Card card = await _sdk.GetCardById(Region.us, "52119-arch-villain-rafaam", 
                Locale.en_US, _accessToken);
            Assert.IsNotNull(card);
            Assert.AreEqual(card.slug, "52119-arch-villain-rafaam");
        }

        [Test]
        public async Task GetCardBackByIdTest()
        {
            CardBack cardBack = await _sdk.GetCardBackById(Region.us, "155-pizza-stone", 
                Locale.en_US, _accessToken);
            Assert.IsNotNull(cardBack);
            Assert.AreEqual(cardBack.slug, "155-pizza-stone");
            Assert.IsNotNull(cardBack.image);
        }

        [Test]
        public async Task GetCardBacksTest()
        {
            var cardBacksCollection = await _sdk.GetCardBacks(Region.us, 
                Locale.en_US, _accessToken);
            Assert.IsNotNull(cardBacksCollection);            
            Assert.True(cardBacksCollection.cardbacks.Count > 0);
        }

        [Test]
        public async Task GetCardBacksSearchTest()
        {
            var cardBacksCollection = await _sdk.GetCardBacks(Region.us,
                Locale.en_US, _accessToken, textFilter: "hero");
            Assert.IsNotNull(cardBacksCollection);
            Assert.True(cardBacksCollection.cardbacks.Count > 0);
        }

        [Test]
        public async Task GetDeckByCodeTest()
        {
            // https://hearthstone.blizzard.com/en-us/deckbuilder?class=warrior%2Cneutral&deckFormat=standard&deckcode=AAECAQcG%2Bwyd8AKS%2BAKggAOblAPanQMMS6IE%2Fweb8wLR9QKD%2BwKe%2BwKz%2FAL1gAOXlAOalAOSnwMA%2Fnull%2F&multiClass=warrior&set=standard
            string code = "AAECAQcG+wyd8AKS+AKggAOblAPanQMMS6IE/web8wLR9QKD+wKe+wKz/AL1gAOXlAOalAOSnwMA";
            Deck deck = await _sdk.GetDeckByCode(Region.us, Locale.en_US,
                code, _accessToken);
            Assert.IsNotNull(deck);
            Assert.AreEqual(HttpUtility.UrlDecode(code), deck.deckCode);
        }  

        [Test]
        public async Task GetMetadataTest()
        {
            string metadata = await _sdk.GetMetadata(Region.us, Locale.en_US, _accessToken);
            Assert.IsNotNull(metadata);
        }

        [Test]
        public async Task GetMetadataByTypeTest()
        {
            string metadata = await _sdk.GetMetadataByType(Region.us, Locale.en_US,
                MetadataType.sets, _accessToken);
            Assert.IsNotNull(metadata);
        }
    }
}