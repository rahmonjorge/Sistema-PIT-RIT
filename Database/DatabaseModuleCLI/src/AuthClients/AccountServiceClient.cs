using Database.Auth;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;

using static Database.Auth.AccountAuthService;

namespace DatabaseModuleClient;

class AccountServiceClient
{
    private AccountAuthServiceClient _accountClient;

    public AccountServiceClient(GrpcChannel channel)
    {
        _accountClient = new AccountAuthServiceClient(channel);
    }

    public void LinkAccount()
    {
        var request = new AdapterAccount
        {
            UserId = "",
            Type = ProtoProviderType.Oauth,
            Provider = "my provider",
            ProviderAccountId = "my provider account id",
            RefreshToken = "my refresh token",
            AccessToken = "my access token",
            ExpiresIn = 9999,
            TokenType = "my token type",
            Scope = "my scope",
            IdToken = "my id token",
            SessionState = "my session state"
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _accountClient.LinkAccount(request);

        Console.WriteLine("Reply received: " + reply);
    }

    public void UnlinkAccount(string provider, string providerAccountId)
    {
        var request = new UnlinkAccountRequest
        {
            Provider = provider,
            ProviderAccountId = providerAccountId
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _accountClient.UnlinkAccount(request);

        Console.WriteLine("Reply received: " + reply);
    }
}
