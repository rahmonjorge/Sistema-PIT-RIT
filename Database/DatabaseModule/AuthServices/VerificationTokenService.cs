namespace Database.Auth.Services;

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

        VerificationToken newToken = new VerificationToken(request.Token)
        {
            Expires = request.Expires.ToDateTime()
        };

        _controller.Create(newToken);

        VerificationTokenObj response = CreateVerificationTokenObj(newToken);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<VerificationTokenObj> UseVerificationToken(UseVerificationTokenRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");
        throw new RpcException(new Status(StatusCode.Unimplemented,$"{context.Method} unimplemented by server"));
    }

    private VerificationTokenObj CreateVerificationTokenObj(VerificationToken token)
    {
        return new VerificationTokenObj()
        {
            Identifier = token.Id.ToString(),
            Expires = Timestamp.FromDateTime(token.Expires),
            Token = token.Token
        };
    }
}