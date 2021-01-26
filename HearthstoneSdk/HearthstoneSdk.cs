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
        public Hearthstone()
        {
        }

        //curl -u {client_id}:{client_secret} -d grant_type=client_credentials https://us.battle.net/oauth/token
        //{"access_token": "USVb1nGO9kwQlhNRRnI4iWVy2UV5j7M6h7", "token_type": "bearer", "expires_in": 86399, "scope": "example.scope"}
        public async Task<string> GetAccessToken(Region region,
                                                 string clientId, 
                                                 string clientSecret)
        {
            var client = new HttpClient();

            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),                    
                });

            client.DefaultRequestHeaders.Add("Authorization", "Basic " + 
                Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(clientId + ":" + clientSecret)));

            // Get the response.
            HttpResponseMessage response = await client.PostAsync(
                string.Format("https://{0}.battle.net/oauth/token", region),
                requestContent);

            string token = null;
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                string responseBody = await reader.ReadToEndAsync();
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);
                token = dict["access_token"];
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
            CardBack card = JsonConvert.DeserializeObject<CardBack>(response);
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

        private async Task<string> GetResponseBodyByUrl(string url)
        {
            var client = new HttpClient();
            
            HttpResponseMessage response = await client.GetAsync(url);

            HttpContent responseContent = response.Content;
            
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                string responseBody = await reader.ReadToEndAsync();
                return responseBody;
            }
        }
    }
}
