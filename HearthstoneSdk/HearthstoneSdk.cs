using System;

namespace HearthstoneSdk
{
    public class HearthstoneSdk
    {
        public HearthstoneSdk()
        {
        }

        //curl -u {client_id}:{client_secret} -d grant_type=client_credentials https://us.battle.net/oauth/token
        //{"access_token": "USVb1nGO9kwQlhNRRnI4iWVy2UV5j7M6h7", "token_type": "bearer", "expires_in": 86399, "scope": "example.scope"}
        public string GetAccessToken(string client_id, string client_secret)
        {
            throw new NotImplementedException();
        }

        public void GetAllMetadata()
        {
            throw new NotImplementedException();
        }
    }
}
