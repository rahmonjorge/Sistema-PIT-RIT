namespace Database.Auth.Services;

using Grpc.Core;
using DatabaseModule.Controllers;
using Database.Auth;

public class AccountService : AccountAuthService.AccountAuthServiceBase
{
    private readonly ILogger<AccountService> _logger;
    private AccountController _controller;

    public AccountService(ILogger<AccountService> logger)
    {
        _logger = logger;

        _controller = new AccountController();
    }

    public override Task<AdapterAccount> LinkAccount(AdapterAccount request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        AdapterAccount response = new AdapterAccount()
        {
            UserId = "DUMMY USER ID",
            Type = Auth.ProtoProviderType.Oauth,
            Provider = "DUMMY PROVIDER",
            ProviderAccountId = "DUMMY PROVIDER'S ID"
        };

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<AdapterAccount> UnlinkAccount(UnlinkAccountRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");
        throw new RpcException(new Status(StatusCode.Unimplemented,$"{context.Method} unimplemented by server"));
    }
}