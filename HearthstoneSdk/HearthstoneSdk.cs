using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                string.Format("https://{0}.battle.net/oauth/token", region),
                requestContent);

            string token = null;
            HttpContent responseContent = response.Content;

            try
            {
                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                {
                    string responseBody = await reader.ReadToEndAsync();
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);
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

        //https://us.api.blizzard.com/hearthstone/cards?locale=en_US&access_token=USeYa4TlSBPfeS37Ri6z1wKIFyAcZY48Oh
        public async Task<List<Card>> GetCards(Region region,
                                               Locale locale,
                                               string accessToken)
        {
            string url = string.Format("https://{0}.api.blizzard.com/hearthstone/cards?locale={1}&access_token={2}",
                            region, locale, accessToken);
            string response = await GetResponseBodyByUrl(url);
            CardsCollection cardsCollection = JsonConvert.DeserializeObject<CardsCollection>(response);
            if (cardsCollection != null)
            {
                return cardsCollection.cards;
            }
            else
            {
                return new List<Card>();
            }
        }

        //https://us.api.blizzard.com/hearthstone/cards/52119-arch-villain-rafaam?locale=en_US&access_token=USeYa4TlSBPfeS37Ri6z1wKIFyAcZY48Oh
        public async Task<Card> GetCardById(Region region, 
                                            string idorslug, 
                                            Locale locale,                                 
                                            string accessToken)
        {
            string url = string.Format("https://{0}.api.blizzard.com/hearthstone/cards/{1}?locale={2}&access_token={3}",
                              region, idorslug, locale, accessToken);
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
            string url = string.Format("https://{0}.api.blizzard.com/hearthstone/cardbacks/{1}?locale={2}&access_token={3}",
                              region, idorslug, locale, accessToken);
            string response = await GetResponseBodyByUrl(url);
            CardBack cardBack = JsonConvert.DeserializeObject<CardBack>(response);
            return cardBack;
        }

        public async Task<List<CardBack>> GetCardBacks(Region region,                                          
                                          Locale locale,
                                          string accessToken,
                                          string cardBackCategory = "",
                                          string textFilter = "",
                                          string sort = "",
                                          string order = ""
                                          )
        {
            // TODO: implement filtering
            string url = string.Format("https://{0}.api.blizzard.com/hearthstone/cardbacks?locale={1}&access_token={2}",
                              region, locale, accessToken);
            string response = await GetResponseBodyByUrl(url);
            CardBacksCollection cardBacksCollection = JsonConvert.DeserializeObject<CardBacksCollection>(response);
            if (cardBacksCollection != null)
            {
                return cardBacksCollection.cardbacks;
            }
            else
            {
                return new List<CardBack>();
            }           
        }

        //https://us.api.blizzard.com/hearthstone/deck?locale=en_US&code=AAECAQcG%2Bwyd8AKS%2BAKggAOblAPanQMMS6IE%2Fweb8wLR9QKD%2BwKe%2BwKz%2FAL1gAOXlAOalAOSnwMA&access_token=USCXQL0qCdabbgXIAASEZ5LfK8aJT5brYh
        public async Task<Deck> GetDeckByCode(Region region,                                           
                                           Locale locale,
                                           string code,
                                           string accessToken)
        {
            string url = string.Format("https://{0}.api.blizzard.com/hearthstone/deck?locale={1}&code={2}&access_token={3}",
                              region, locale, code, accessToken);
            string response = await GetResponseBodyByUrl(url);
            Deck card = JsonConvert.DeserializeObject<Deck>(response);
            return card;
        }

        //https://us.api.blizzard.com/hearthstone/metadata?locale=en_US&access_token=USeYa4TlSBPfeS37Ri6z1wKIFyAcZY48Oh
        public async Task<string> GetMetadata(Region region, 
                                              Locale locale, 
                                              string accessToken)
        {
            string url = string.Format("https://{0}.api.blizzard.com/hearthstone/metadata?locale={1}&access_token={2}", 
                            region, locale, accessToken);
            string response = await GetResponseBodyByUrl(url);
            return response;
        }

        //https://us.api.blizzard.com/hearthstone/metadata/sets?locale=en_US&access_token=US0BGa5ZmrooCiI7444MSepywywJAZDCfq
        public async Task<string> GetMetadataByType(Region region,
                                                    Locale locale,
                                                    MetadataType type,
                                                    string accessToken)
        {
            string url = string.Format("https://{0}.api.blizzard.com/hearthstone/metadata/{1}?locale={2}&access_token={3}",
                            region, type, locale, accessToken);
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
