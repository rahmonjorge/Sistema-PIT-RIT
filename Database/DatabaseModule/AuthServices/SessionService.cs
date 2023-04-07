namespace Database.Auth.Services;

using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Google.Protobuf.WellKnownTypes;

public class SessionService : SessionAuthService.SessionAuthServiceBase
{
    private readonly ILogger<SessionService> _logger;
    private SessionController _controller;


    public SessionService(ILogger<SessionService> logger)
    {
        _logger = logger;

        _controller = new SessionController();
    }

    public override Task<SessionObj> CreateSession(SessionObj request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        Session newSession = new Session(request.SessionToken, request.UserId, request.Expires.ToDateTime());

        _controller.Create(newSession);

        SessionObj response = CreateSessionObj(newSession);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<GetSessionAndUserResponse> GetSessionAndUser(GetSessionAndUserRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        GetSessionAndUserResponse response = new GetSessionAndUserResponse()
        {

        };

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<SessionObj> UpdateSession(UpdateSessionRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find Session
        Session sessionFound = _controller.Read("token", request.SessionToken);
        if (sessionFound == null) throw new RpcException(new Status(StatusCode.NotFound, "Session with token: '" + request.SessionToken + "' not found."));
        
        Session replacementSession = new Session(request.SessionToken, request.UserId, request.Expires.ToDateTime());

        // Replace Session
        var result = _controller.Update("token", request.SessionToken, replacementSession);
        if (!result) throw new RpcException(new Status(StatusCode.Aborted, "Not acknowledged"));

        SessionObj response = CreateSessionObj(replacementSession);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<SessionObj> DeleteSession(DeleteSessionRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find session
        Session sessionFound = _controller.Read("token", request.SessionToken);
        if (sessionFound == null) throw new RpcException(new Status(StatusCode.NotFound, "Session with token: '" + request.SessionToken + "' not found."));
        
        // Delete session
        var result = _controller.Delete("token", request.SessionToken);
        if (!result) throw new RpcException(new Status(StatusCode.Aborted, "Deletion not acknowledged"));

        // Send response
        SessionObj response = CreateSessionObj(sessionFound);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    private SessionObj CreateSessionObj(Session session)
    {
        return new SessionObj
        {
            SessionToken = session.Token,
            UserId = session.UserId.ToString(),
            Expires = Timestamp.FromDateTime(session.Expires)
        };
    }
}