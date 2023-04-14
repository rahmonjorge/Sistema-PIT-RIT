namespace Database.Auth.Services;

using HTools;
using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Google.Protobuf.WellKnownTypes;

public class VerificationTokenService : VerificationTokenAuthService.VerificationTokenAuthServiceBase
{
    private readonly ILogger<VerificationTokenService> _logger;

    public VerificationTokenService(ILogger<VerificationTokenService> logger)
    {
        _logger = logger;
    }

    public override Task<VerificationTokenObj> CreateVerificationToken(VerificationTokenObj request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        VerificationToken newToken = new VerificationToken(request.Token)
        {
            Expires = request.Expires.ToDateTime()
        };

        DatabaseCore.tokens.Create(newToken);

        VerificationTokenObj response = CreateVerificationTokenObj(newToken);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<VerificationTokenObj> UseVerificationToken(UseVerificationTokenRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);
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