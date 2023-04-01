namespace Database.Auth;

using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Google.Protobuf.WellKnownTypes;

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
        throw new RpcException(new Status(StatusCode.Unimplemented,$"{context.Method} unimplemented by server"));
    }

    public override Task<AdapterAccount> UnlinkAccount(UnlinkAccountRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");
        throw new RpcException(new Status(StatusCode.Unimplemented,$"{context.Method} unimplemented by server"));
    }
}