namespace Database.Auth;

using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Google.Protobuf.WellKnownTypes;

public class VerificationTokenService : VerificationTokenAuthService.VerificationTokenAuthServiceBase
{
    private readonly ILogger<VerificationTokenService> _logger;
    private VerificationTokenController _controller;

    public VerificationTokenService(ILogger<VerificationTokenService> logger)
    {
        _logger = logger;

        _controller = new VerificationTokenController();
    }

    public override Task<VerificationTokenObj> CreateVerificationToken(VerificationTokenObj request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");
        throw new RpcException(new Status(StatusCode.Unimplemented,$"{context.Method} unimplemented by server"));
    }

    public override Task<VerificationTokenObj> UseVerificationToken(UseVerificationTokenRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");
        throw new RpcException(new Status(StatusCode.Unimplemented,$"{context.Method} unimplemented by server"));
    }
}