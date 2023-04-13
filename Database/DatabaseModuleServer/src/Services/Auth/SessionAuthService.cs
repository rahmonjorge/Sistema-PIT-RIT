namespace Database.Auth.Services;

using HTools;
using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Google.Protobuf.WellKnownTypes;

public class SessionService : SessionAuthService.SessionAuthServiceBase
{
    private readonly ILogger<SessionService> _logger;

    public SessionService(ILogger<SessionService> logger)
    {
        _logger = logger;
    }

    public override Task<SessionObj> CreateSession(SessionObj request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        Session newSession = new Session(request.SessionToken, request.UserId, request.Expires.ToDateTime());

        DatabaseCore.sessions.Create(newSession);

        SessionObj response = CreateSessionObj(newSession);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override async Task<GetSessionAndUserResponse> GetSessionAndUser(GetSessionAndUserRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        // Find Session
        Session session = DatabaseCore.sessions.Read("token",request.SessionToken);
        if (session == null) throw new RpcException(new Status(StatusCode.NotFound, $"Session with token: '{request.SessionToken}' not found."));
        Console.WriteLine($"Session Found: {session}");

        // Find User
        User user = await DatabaseCore.users.ReadAsync("_id",session.UserId.ToString());
        if (user == null) throw new RpcException(new Status(StatusCode.NotFound, $"User with id: '{session.UserId.ToString()}' not found."));
        
        GetSessionAndUserResponse response = CreateGetSessionAndUserResponse(session, user);
        
        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public override Task<SessionObj> UpdateSession(UpdateSessionRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        // Find Session
        Session sessionFound = DatabaseCore.sessions.Read("token", request.SessionToken);
        if (sessionFound == null) throw new RpcException(new Status(StatusCode.NotFound, "Session with token: '" + request.SessionToken + "' not found."));
        
        Session replacementSession = new Session(request.SessionToken, request.UserId, request.Expires.ToDateTime());

        // Replace Session
        var result = DatabaseCore.sessions.Update("token", request.SessionToken, replacementSession);
        if (!result) throw new RpcException(new Status(StatusCode.Aborted, "Not acknowledged"));

        SessionObj response = CreateSessionObj(replacementSession);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<SessionObj> DeleteSession(DeleteSessionRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        // Find session
        Session sessionFound = DatabaseCore.sessions.Read("token", request.SessionToken);
        if (sessionFound == null) throw new RpcException(new Status(StatusCode.NotFound, "Session with token: '" + request.SessionToken + "' not found."));
        
        // Delete session
        var result = DatabaseCore.sessions.Delete("token", request.SessionToken);
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

    private GetSessionAndUserResponse CreateGetSessionAndUserResponse(Session session, User user)
    {
        return new GetSessionAndUserResponse()
        {
            Session = CreateSessionObj(session),
            User = UserService.CreateBasicUserResponse(user)
        };
    }
}