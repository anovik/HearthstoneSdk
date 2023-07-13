# HearthstoneSdk

Blizzard provides APIs for getting various information about Hearthstone cards, card backs, decks and metadata.

https://develop.battle.net/documentation/hearthstone/game-data-apis

The aim of the current project is to create .NET 6 library that allows to get all this information easily.

## Nuget

You can add the library to your project using NuGet:

https://www.nuget.org/packages/HearthstoneSdk/

# Getting Started

https://develop.battle.net/documentation/guides/getting-started

To start working with Hearthstone API directly or through HearthstoneSdk you need to create a Battle.net account and generate a client id and client secret.

To run HearthstoneSdkTests you need to insert your client id and client secret in the code:

```csharp
const string _clientId = "PUT_YOUR_CLIENT_ID_HERE";
const string _clientSecret = "PUT_YOUR_CLIENT_SECRET_HERE";
```

# Restrictions

API clients are limited to 36,000 requests per hour at a rate of 100 requests per second. Exceeding the hourly quota results in slower service until traffic decreases. Exceeding the per-second limit results in a 429 error for the remainder of the second until the quota refreshes.
