namespace DatabaseModuleCLI;

using Database.Auth;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;

using static Database.Auth.VerificationTokenAuthService;

class VerificationTokenServiceClient
{
    private VerificationTokenAuthServiceClient _tokenClient;

    public VerificationTokenServiceClient(GrpcChannel channel)
    {
        _tokenClient = new VerificationTokenAuthServiceClient(channel);
    }

    public void CreateVerificationToken(string id, string token)
    {
        var request = new VerificationTokenObj
        {
            Identifier = id,
            Expires = Timestamp.FromDateTime(DateTime.UtcNow),
            Token = token
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _tokenClient.CreateVerificationToken(request);

        Console.WriteLine("Reply received: " + reply);
    }

    public void UseVerificationToken(string id, string token)
    {
        var request = new UseVerificationTokenRequest
        {
            Identifier = id,
            Token = token
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _tokenClient.UseVerificationToken(request);

        Console.WriteLine("Reply received: " + reply);
    }
}