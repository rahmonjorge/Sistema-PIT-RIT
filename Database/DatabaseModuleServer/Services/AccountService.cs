namespace Database.Auth.Services;

using Grpc.Core;
using DatabaseModule.Entities;
using Database.Auth;

public class AccountService : AccountAuthService.AccountAuthServiceBase
{
    private readonly ILogger<AccountService> _logger;

    public AccountService(ILogger<AccountService> logger)
    {
        _logger = logger;
    }

    public override Task<AdapterAccount> LinkAccount(AdapterAccount request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        Account account = new Account(request.Provider, request.ProviderAccountId);

        // Adding new account to the database
        DatabaseModuleMain.accounts.Create(account);

        AdapterAccount response = CreateAdapterAccount(account);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<AdapterAccount> UnlinkAccount(UnlinkAccountRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");
        
        // Removing requested account from database
        DatabaseModuleMain.accounts.Delete("provider", request.Provider, "provider_id", request.ProviderAccountId);

        Account account = new Account(request.Provider, request.ProviderAccountId);

        AdapterAccount response = CreateAdapterAccount(account);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    private AdapterAccount CreateAdapterAccount(Account account)
    {
        return new AdapterAccount()
        {
           UserId = "which user id? i don't know",
           Type = Auth.ProtoProviderType.Oauth,
           Provider = account.Provider,
           ProviderAccountId = account.ProviderAccountId,
           RefreshToken = "should i make one up?",
           AccessToken = "no seriously should i make these up?",
           ExpiresIn = 69420,
           TokenType = "the token type is blue. because yes.",
           Scope = "rifle M4A1",
           IdToken = "which token?",
           SessionState = "the state of this session is: absolutely not working."
        };
    }
}