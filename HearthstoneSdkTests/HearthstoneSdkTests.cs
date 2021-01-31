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
            List<Card> cards = await _sdk.GetCards(Region.eu, Locale.ru_RU, _accessToken);
            Assert.IsNotNull(cards);
            Assert.True(cards.Count > 0);
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
            List<CardBack> cardBacks = await _sdk.GetCardBacks(Region.eu, 
                Locale.ru_RU,_accessToken);
            Assert.IsNotNull(cardBacks);            
            Assert.True(cardBacks.Count > 0);
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