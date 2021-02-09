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
        //const string _clientId = "PUT_YOUR_CLIENT_ID_HERE";
        //const string _clientSecret = "PUT_YOUR_CLIENT_SECRET_HERE";
        const string _clientId = "PUT_YOUR_CLIENT_ID_HERE";
        const string _clientSecret = "PUT_YOUR_CLIENT_SECRET_HERE";

        string _accessToken;

        [SetUp]
        public async Task Setup()
        {
            _accessToken = await _sdk.GetAccessToken(Region.eu,_clientId, _clientSecret);
        }      

        [Test]
        public async Task GetAccessTokenTest()
        {            
            string accessToken = await _sdk.GetAccessToken(Region.eu, _clientId, _clientSecret);
            Assert.IsNotNull(accessToken);
        }

        [Test]
        public async Task GetCardsTest()
        {
            var cardsCollection = await _sdk.GetCards(Region.eu, Locale.ru_RU, _accessToken);
            Assert.IsNotNull(cardsCollection);
            Assert.True(cardsCollection.cards.Count > 0);
        }

        [Test]
        public async Task GetCardsSearchTest()
        {
           var cardsCollection = await _sdk.GetCards(region: Region.eu, 
                                           locale: Locale.ru_RU,
                                           accessToken: _accessToken,
                                           set: "rise-of-shadows",
                                           classString: "mage",
                                           manaCost: new List<int> {10},
                                           attack: new List<int> {4},
                                           health: new List<int> {10},
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
            var cardsCollection = await _sdk.GetBattlegroundCards(region: Region.eu,
                                         locale: Locale.ru_RU,
                                         accessToken: _accessToken,
                                         tier: new List<string>() { "hero", "3" } );
            Assert.IsNotNull(cardsCollection);
            Assert.True(cardsCollection.cards.Count > 0);
        }

        [Test]
        public async Task GetCardByIdTest()
        {
            Card card = await _sdk.GetCardById(Region.eu, "52119-arch-villain-rafaam", 
                Locale.ru_RU, _accessToken);
            Assert.IsNotNull(card);
            Assert.AreEqual(card.slug, "52119-arch-villain-rafaam");
        }

        [Test]
        public async Task GetCardBackByIdTest()
        {
            CardBack cardBack = await _sdk.GetCardBackById(Region.eu, "155-pizza-stone", 
                Locale.ru_RU, _accessToken);
            Assert.IsNotNull(cardBack);
            Assert.AreEqual(cardBack.slug, "155-pizza-stone");
            Assert.IsNotNull(cardBack.image);
        }

        [Test]
        public async Task GetCardBacksTest()
        {
            var cardBacksCollection = await _sdk.GetCardBacks(Region.eu, 
                Locale.ru_RU,_accessToken);
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
            string code = "AAECAQcG+wyd8AKS+AKggAOblAPanQMMS6IE/web8wLR9QKD+wKe+wKz/AL1gAOXlAOalAOSnwMA";
            Deck deck = await _sdk.GetDeckByCode(Region.eu, Locale.ru_RU,
                code, _accessToken);
            Assert.IsNotNull(deck);
            Assert.AreEqual(HttpUtility.UrlDecode(code), deck.deckCode);
        }

        [Test]
        public async Task GetDeckByCardListTest()
        {
            int heroId = 813;
            List<int> ids = new List<int> { 906, 1099, 1363, 1367, 46706, 48099, 48759, 49184, 50071, 50278, 51714, 52109, 52632, 52715, 53409, 53413, 53756,
                53969, 54148, 54425, 54431, 54874, 54898, 54917, 55166, 55245, 55438, 55441, 55907, 57416 };
            Deck deck = await _sdk.GetDeckByCardList(Region.eu, Locale.ru_RU,
                ids, heroId, _accessToken);
            Assert.IsNotNull(deck);
            Assert.True(deck.cards.Count > 0);
            Assert.AreEqual(deck.hero.id, heroId);            
        }

        [Test]
        public async Task GetMetadataTest()
        {
            string metadata = await _sdk.GetMetadata(Region.eu, Locale.ru_RU, _accessToken);
            Assert.IsNotNull(metadata);
        }

        [Test]
        public async Task GetMetadataByTypeTest()
        {
            string metadata = await _sdk.GetMetadataByType(Region.eu, Locale.ru_RU,
                MetadataType.sets, _accessToken);
            Assert.IsNotNull(metadata);
        }
    }
}