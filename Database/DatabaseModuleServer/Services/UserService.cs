namespace Database.Auth.Services;

using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Google.Protobuf.WellKnownTypes;

public class UserService : UserAuthService.UserAuthServiceBase
{
    private readonly ILogger<UserService> _logger;

    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;
    }

    public override Task<BasicUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        if (request == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Null request."));
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        User newUser = new User(request.Email, request.Image)
        {
            EmailVerified = request.EmailVerified.ToDateTime(),
            Nome = request.Name
        };

        DatabaseModuleMain.users.Create(newUser);

        BasicUserResponse response = CreateBasicUserResponse(newUser);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<BasicUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = DatabaseModuleMain.users.Read("_id", request.Id);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));

        // Send responses
        BasicUserResponse response = CreateBasicUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<BasicUserResponse> GetUserByEmail(GetUserByEmailRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = DatabaseModuleMain.users.Read("email", request.Email);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with email: '" + request.Email + "' not found."));
        Console.WriteLine(userFound);

        // Send responses
        BasicUserResponse response = CreateBasicUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    // TODO: PROBABLY A MESS
    public override Task<BasicUserResponse> GetUserByAccount(GetUserByAccountRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find Account
        Account account = DatabaseModuleMain.accounts.Read("provider_id", request.ProviderAccountId, "provider", request.Provider);
        if (account == null || account.TokenId == null) 
            throw new RpcException(new Status(StatusCode.NotFound, $"Account with provider_id: '{request.ProviderAccountId}' and provider: '{request.Provider}' not found."));

        User dummy = new User("DUMMYEMAIL","DUMMYIMAGE")
        {
            EmailVerified = DateTime.UtcNow,
            Nome = "DUMMYIMAGE"
        };
        
        BasicUserResponse response = CreateBasicUserResponse(dummy);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<BasicUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = DatabaseModuleMain.users.Read("_id", request.Id);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));
        
        User replacementUser = new User(request.Email, request.Image)
        {
            EmailVerified = request.EmailVerified.ToDateTime(),
            Nome = request.Name
        };

        // Replace user
        var result = DatabaseModuleMain.users.Update("_id", request.Id, replacementUser);
        if (!result) throw new RpcException(new Status(StatusCode.Aborted, "Not acknowledged"));

        BasicUserResponse response = CreateBasicUserResponse(replacementUser);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<BasicUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = DatabaseModuleMain.users.Read("_id", request.Id);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));
        
        // Delete user
        var result = DatabaseModuleMain.users.Delete("_id", request.Id);
        if (!result) throw new RpcException(new Status(StatusCode.Aborted, "Not acknowledged"));

        // Send response
        BasicUserResponse response = CreateBasicUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public static BasicUserResponse CreateBasicUserResponse(User user)
    {
        return new BasicUserResponse
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            CadastroCompleto = false,
            EmailVerified = Timestamp.FromDateTime(user.EmailVerified),
            Name = user.Nome,
            Image = user.Image
        };
    }
}
