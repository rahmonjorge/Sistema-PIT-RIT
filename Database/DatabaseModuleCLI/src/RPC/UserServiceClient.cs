namespace DatabaseModuleCLI;

using Database.Auth;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;

using static Database.Auth.UserAuthService;

class UserServiceClient
{
    private UserAuthServiceClient _userClient;

    public UserServiceClient(GrpcChannel channel)
    {
        _userClient = new UserAuthServiceClient(channel);
    }

    public void CreateUser(string email)
    {
        var request = new CreateUserRequest 
        {
            Name = "Lorem Ipsum",
            Email = email,
            EmailVerified = Timestamp.FromDateTime(DateTime.UtcNow),
            Image = "myimage"
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _userClient.CreateUser(request);

        Console.WriteLine("Reply received: " + reply);
    }

    public BasicUserResponse GetUser(string id)
    {
        var request = new GetUserRequest 
        {
            Id = id
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _userClient.GetUser(request);

        Console.WriteLine("Reply received: " + reply);

        return reply;
    }

    public BasicUserResponse GetUserByEmail(string email)
    {
        var request = new GetUserByEmailRequest 
        {
            Email = email
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _userClient.GetUserByEmail(request);

        Console.WriteLine("Reply received: " + reply);

        return reply;
    }

    public BasicUserResponse GetUserByAccount()
    {
        var request = new GetUserByAccountRequest 
        {
            ProviderAccountId = "provider_id",
            Provider = "provider"
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _userClient.GetUserByAccount(request);

        Console.WriteLine("Reply received: " + reply);

        return reply;
    }

    public void UpdateUser(string id, string name)
    {
        var request = new UpdateUserRequest 
        {
            Id = id,
            Name = name,
            Email = "updated email",
            EmailVerified = Timestamp.FromDateTime(DateTime.UtcNow),
            Image = "Updated image"
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _userClient.UpdateUser(request);

        Console.WriteLine("Reply received: " + reply);
    }

    public BasicUserResponse DeleteUser(string id)
    {
        var request = new DeleteUserRequest 
        {
            Id = id
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _userClient.DeleteUser(request);

        Console.WriteLine("Reply received: " + reply);

        return reply;
    }
}