# HearthstoneSdk

Blizzard provides APIs for getting various information about Hearthstone cards, card backs, decks and metadata.

https://develop.battle.net/documentation/hearthstone/game-data-apis

The aim of current project is creating .NET 5 library allowing to get all this information easily.


# Getting Started

https://develop.battle.net/documentation/guides/getting-started

To start working with Hearthstone API directly or through HearthstoneSdk you need to create Battle.net account and generate client id and client secret.

To run HearthstoneSdkTests you need to insert your client id and client secret in the code:

```csharp
const string _clientId = "PUT_YOUR_CLIENT_ID_HERE";
const string _clientSecret = "PUT_YOUR_CLIENT_SECRET_HERE";
```
