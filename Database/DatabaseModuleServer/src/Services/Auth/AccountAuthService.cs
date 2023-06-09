namespace Database.Auth.Services;

using HTools;
using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
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
        Printer.LogRequest(request, context.Host, context.Method);

        Account account = CreateAccount(request);

        // Adding new account to the database
        DatabaseCore.accounts.Create(account);

        AdapterAccount response = CreateAdapterAccount(account);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override async Task<AdapterAccount> UnlinkAccount(UnlinkAccountRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);
        
        // Finding account
        Account account = await DatabaseCore.accounts.ReadAsync("provider", request.Provider, "provider_id", request.ProviderAccountId);

        // Removing requested account from database
        DatabaseCore.accounts.Delete("provider", request.Provider, "provider_id", request.ProviderAccountId);

        AdapterAccount response = CreateAdapterAccount(account);

        Console.WriteLine("Response sent: " + response);

        return response;
    }

    private Account CreateAccount(AdapterAccount request)
    {
        return new Account(request.UserId, request.Provider, request.ProviderAccountId, request.Type)
        {
            RefreshToken = request.RefreshToken,
            AccessToken = request.AccessToken,
            ExpiresIn = request.ExpiresIn,
            TokenType = request.TokenType,
            Scope = request.Scope,
            TokenId = request.IdToken,
            SessionState = request.SessionState
        };
    }

    private AdapterAccount CreateAdapterAccount(Account account)
    {
        return new AdapterAccount()
        {
           UserId = account.UserId,
           Type = account.ProviderType,
           Provider = account.Provider,
           ProviderAccountId = account.ProviderAccountId,
           RefreshToken = account.RefreshToken,
           AccessToken = account.AccessToken,
           ExpiresIn = account.ExpiresIn,
           TokenType = account.TokenType,
           Scope = account.Scope,
           IdToken = account.TokenId,
           SessionState = account.SessionState
        };
    }
}