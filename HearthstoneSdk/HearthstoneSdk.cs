using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HearthstoneSdk
{
    public class Hearthstone
    {
        private readonly HttpClient _client = new HttpClient();

        public Hearthstone()
        {
        }

        //curl -u {client_id}:{client_secret} -d grant_type=client_credentials https://us.battle.net/oauth/token
        //{"access_token": "USVb1nGO9kwQlhNRRnI4iWVy2UV5j7M6h7", "token_type": "bearer", "expires_in": 86399, "scope": "example.scope"}
        public async Task<string> GetAccessToken(Region region,
                                                 string clientId, 
                                                 string clientSecret)
        {            
            var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),                    
                });

            _client.DefaultRequestHeaders.Add("Authorization", "Basic " + 
                Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(clientId + ":" + clientSecret)));
            
            HttpResponseMessage response = await _client.PostAsync(
                $"https://{region}.battle.net/oauth/token",
                requestContent);

            string token = null;
            HttpContent responseContent = response.Content;

            try
            {
                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                {
                    string responseBody = await reader.ReadToEndAsync();
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);
                    // TODO: process possible errors here
                    token = dict["access_token"];
                }
            }
            finally
            {
                // anyway clear DefaultRequestHeaders
                _client.DefaultRequestHeaders.Clear();                
            }

            return token;
        }
        
        //https://us.api.blizzard.com/hearthstone/cards?locale=en_US&set=rise-of-shadows&class=mage&manaCost=10&attack=4&health=10&collectible=1&rarity=legendary&type=minion&minionType=dragon&keyword=battlecry&textFilter=kalecgos&gameMode=constructed&page=1&pageSize=5&sort=name&order=desc&access_token=USFIZOMpbbZo4NFI9UC6RiPnQvbYGyyT4F
        public async Task<CardsCollection> GetCards(Region region,
                                               Locale locale,
                                               string accessToken,
                                               string set = "",
                                               string classString = "",
                                               List<int> manaCost = null,
                                               List<int> attack = null,
                                               List<int> health = null,
                                               List<int> collectible = null,
                                               string rarity = "",
                                               string typeString = "",
                                               string minionType = "",
                                               string keyword = "",
                                               string textFilter = "",
                                               GameMode mode = GameMode.constructed,
                                               int page = 1,
                                               int pageSize = 10,
                                               string sort = "",
                                               string order = "")
        {            
            string url = $"https://{region}.api.blizzard.com/hearthstone/cards?locale={locale}";

            if (!string.IsNullOrEmpty(set))
            {
                url += $"&set={set}";
            }
            if (!string.IsNullOrEmpty(classString))
            {
                url += $"&class={classString}";
            }
            if (manaCost != null)
            {
                var manaCostList = HttpUtility.UrlEncode(string.Join(",", manaCost));
                url += $"&manaCost={manaCostList}";
            }
            if (attack != null)
            {
                var attackList = HttpUtility.UrlEncode(string.Join(",", attack));
                url += $"&attack={attackList}";
            }
            if (health != null)
            {
                var healthList = HttpUtility.UrlEncode(string.Join(",", health));
                url += $"&health={healthList}";
            }
            if (collectible != null)
            {
                var collectibleList = HttpUtility.UrlEncode(string.Join(",", collectible));
                url += $"&collectible={collectibleList}";
            }
            if (!string.IsNullOrEmpty(rarity))
            {
                url += $"&rarity={rarity}";
            }
            if (!string.IsNullOrEmpty(typeString))
            {
                url += $"&type={typeString}";
            }
            if (!string.IsNullOrEmpty(minionType))
            {
                url += $"&minionType={minionType}";
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                url += $"&keyword={keyword}";
            }
            if (!string.IsNullOrEmpty(textFilter))
            {
                url += $"&textFilter={textFilter}";
            }
            url += $"&gameMode={mode}";
            url += $"&page={page}";
            url += $"&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(sort))
            {
                url += $"&sort={sort}";
            }
            if (!string.IsNullOrEmpty(order))
            {
                url += $"&order={order}";
            }
            url += $"&access_token={accessToken}";

            string response = await GetResponseBodyByUrl(url);
            CardsCollection cardsCollection = JsonConvert.DeserializeObject<CardsCollection>(response);
            return cardsCollection;
        }

        public async Task<CardsCollection> GetBattlegroundCards(Region region,
                                               Locale locale,
                                               string accessToken,
                                               List<string> tier,
                                               List<int> attack = null,
                                               List<int> health = null,                                              
                                               string minionType = "",
                                               string keyword = "",
                                               string textFilter = "",                                               
                                               int page = 1,
                                               int pageSize = 10,
                                               string sort = "",
                                               string order = "")
        {
            string url = $"https://{region}.api.blizzard.com/hearthstone/cards?locale={locale}";

            url += $"&gameMode=battlegrounds";

            if (tier != null)
            {
                var tierList = HttpUtility.UrlEncode(string.Join(",", tier));
                url += $"&tier={tierList}";
            }
            if (attack != null)
            {
                var attackList = HttpUtility.UrlEncode(string.Join(",", attack));
                url += $"&attack={attackList}";
            }
            if (health != null)
            {
                var healthList = HttpUtility.UrlEncode(string.Join(",", health));
                url += $"&health={healthList}";
            }
            if (!string.IsNullOrEmpty(minionType))
            {
                url += $"&minionType={minionType}";
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                url += $"&keyword={keyword}";
            }
            if (!string.IsNullOrEmpty(textFilter))
            {
                url += $"&textFilter={textFilter}";
            }
            url += $"&page={page}";
            url += $"&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(sort))
            {
                url += $"&sort={sort}";
            }
            if (!string.IsNullOrEmpty(order))
            {
                url += $"&order={order}";
            }
            url += $"&access_token={accessToken}";

            string response = await GetResponseBodyByUrl(url);
            CardsCollection cardsCollection = JsonConvert.DeserializeObject<CardsCollection>(response);
            return cardsCollection;
        }

        //https://us.api.blizzard.com/hearthstone/cards/52119-arch-villain-rafaam?locale=en_US&access_token=USeYa4TlSBPfeS37Ri6z1wKIFyAcZY48Oh
        public async Task<Card> GetCardById(Region region, 
                                            string idorslug, 
                                            Locale locale,                                 
                                            string accessToken)
        {
            string url = $"https://{region}.api.blizzard.com/hearthstone/cards/{idorslug}?locale={locale}&access_token={accessToken}";
            string response = await GetResponseBodyByUrl(url);
            Card card = JsonConvert.DeserializeObject<Card>(response);
            return card;            
        }

        //https://us.api.blizzard.com/hearthstone/cardbacks/155-pizza-stone?locale=en_US&access_token=US4huw9bkzz57N48OyULtJjwmJCBsLG9ZM
        public async Task<CardBack> GetCardBackById(Region region,
                                           string idorslug,
                                           Locale locale,
                                           string accessToken)
        {
            string url = $"https://{region}.api.blizzard.com/hearthstone/cardbacks/{idorslug}?locale={locale}&access_token={accessToken}";
            string response = await GetResponseBodyByUrl(url);
            CardBack cardBack = JsonConvert.DeserializeObject<CardBack>(response);
            return cardBack;
        }

        public async Task<CardBacksCollection> GetCardBacks(Region region,                                          
                                          Locale locale,
                                          string accessToken,
                                          string cardBackCategory = "",
                                          string textFilter = "",
                                          string sort = "",
                                          string order = ""
                                          )
        {
            string url = $"https://{region}.api.blizzard.com/hearthstone/cardbacks?locale={locale}";
            if (!string.IsNullOrEmpty(cardBackCategory))
            {
                url += $"&cardBackCategory={cardBackCategory}";
            }
            if (!string.IsNullOrEmpty(textFilter))
            {
                url += $"&textFilter={textFilter}";
            }
            if (!string.IsNullOrEmpty(sort))
            {
                url += $"&sort={sort}";
            }
            if (!string.IsNullOrEmpty(order))
            {
                url += $"&order={order}";
            }
            url += $"&access_token={accessToken}";
            string response = await GetResponseBodyByUrl(url);
            CardBacksCollection cardBacksCollection = JsonConvert.DeserializeObject<CardBacksCollection>(response);
            return cardBacksCollection;
        }

        //https://us.api.blizzard.com/hearthstone/deck?locale=en_US&code=AAECAQcG%2Bwyd8AKS%2BAKggAOblAPanQMMS6IE%2Fweb8wLR9QKD%2BwKe%2BwKz%2FAL1gAOXlAOalAOSnwMA&access_token=USCXQL0qCdabbgXIAASEZ5LfK8aJT5brYh
        public async Task<Deck> GetDeckByCode(Region region,                                           
                                           Locale locale,
                                           string code,
                                           string accessToken)
        {
            string url = $"https://{region}.api.blizzard.com/hearthstone/deck?locale={locale}&code={code}&access_token={accessToken}";
            string response = await GetResponseBodyByUrl(url);
            Deck card = JsonConvert.DeserializeObject<Deck>(response);
            return card;
        }

        //https://us.api.blizzard.com/hearthstone/deck?locale=en_US&ids=2C55907%2C57416&hero=813&access_token=USK6UsLobuF5dMIxBsr6nFGeBt1iRRlZg3
        public async Task<Deck> GetDeckByCardList(Region region,
                                        Locale locale,
                                        List<int> cardIds,
                                        int heroId,
                                        string accessToken)
        {
            var cardsList = HttpUtility.UrlEncode(string.Join(",", cardIds));
            string url = $"https://{region}.api.blizzard.com/hearthstone/deck?locale={locale}&ids={cardsList}&hero={heroId}&access_token={accessToken}";
            string response = await GetResponseBodyByUrl(url);
            Deck card = JsonConvert.DeserializeObject<Deck>(response);
            return card;
        }

        //https://us.api.blizzard.com/hearthstone/metadata?locale=en_US&access_token=USeYa4TlSBPfeS37Ri6z1wKIFyAcZY48Oh
        public async Task<string> GetMetadata(Region region, 
                                              Locale locale, 
                                              string accessToken)
        {
            string url = $"https://{region}.api.blizzard.com/hearthstone/metadata?locale={locale}&access_token={accessToken}";
            string response = await GetResponseBodyByUrl(url);
            return response;
        }

        //https://us.api.blizzard.com/hearthstone/metadata/sets?locale=en_US&access_token=US0BGa5ZmrooCiI7444MSepywywJAZDCfq
        public async Task<string> GetMetadataByType(Region region,
                                                    Locale locale,
                                                    MetadataType type,
                                                    string accessToken)
        {
            string url = $"https://{region}.api.blizzard.com/hearthstone/metadata/{type}?locale={locale}&access_token={accessToken}";
            string response = await GetResponseBodyByUrl(url);
            return response;
        }

        private async Task<string> GetResponseBodyByUrl(string url)
        {            
            
            HttpResponseMessage response = await _client.GetAsync(url);

            HttpContent responseContent = response.Content;
            
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                string responseBody = await reader.ReadToEndAsync();
                return responseBody;
            }
        }
    }
}
