namespace DatabaseModuleCLI;

using Database.Auth;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;

using static Database.Auth.SessionAuthService;

class SessionServiceClient
{
    private SessionAuthServiceClient _sessionClient;

    public SessionServiceClient(GrpcChannel channel)
    {
        _sessionClient = new SessionAuthServiceClient(channel);
    }

    public void CreateSession(string token)
    {
        var request = new SessionObj 
        {
            SessionToken = token,
            UserId = "642787967038f7ff18cab08b",
            Expires = Timestamp.FromDateTime(DateTime.UtcNow)
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _sessionClient.CreateSession(request);

        Console.WriteLine("Reply received: " + reply);
    }

    public GetSessionAndUserResponse GetSessionAndUser(string token)
    {
        var request = new GetSessionAndUserRequest 
        {
            SessionToken = token
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _sessionClient.GetSessionAndUser(request);

        Console.WriteLine("Reply received: " + reply);

        return reply;
    }

    public void UpdateSession(string token)
    {
        var request = new UpdateSessionRequest 
        {
            SessionToken = token,
            UserId = "642787967038f7ff18cab08b",
            Expires = Timestamp.FromDateTime(DateTime.UtcNow)
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _sessionClient.UpdateSession(request);

        Console.WriteLine("Reply received: " + reply);
    }

    public SessionObj DeleteSession(string token)
    {
        var request = new DeleteSessionRequest 
        {
            SessionToken = token
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _sessionClient.DeleteSession(request);

        Console.WriteLine("Reply received: " + reply);

        return reply;
    }
}