namespace Database.Auth.Services;

using HTools;
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
        Printer.BlueLn($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

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

    public override async Task<BasicUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        Printer.BlueLn($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = await DatabaseModuleMain.users.Read("_id", request.Id);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));

        // Send responses
        BasicUserResponse response = CreateBasicUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public override async Task<BasicUserResponse> GetUserByEmail(GetUserByEmailRequest request, ServerCallContext context)
    {
        Printer.BlueLn($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = await DatabaseModuleMain.users.Read("email", request.Email);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with email: '" + request.Email + "' not found."));
        Console.WriteLine(userFound);

        // Send responses
        BasicUserResponse response = CreateBasicUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return response;
    }

    // TODO: PROBABLY A MESS
    public override async Task<BasicUserResponse> GetUserByAccount(GetUserByAccountRequest request, ServerCallContext context)
    {
        Printer.BlueLn($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find Account
        Account account = await DatabaseModuleMain.accounts.Read("provider_id", request.ProviderAccountId, "provider", request.Provider);
        if (account == null || account.TokenId == null) 
            throw new RpcException(new Status(StatusCode.NotFound, $"Account with provider_id: '{request.ProviderAccountId}' and provider: '{request.Provider}' not found."));

        // Find User
        User userFound = await DatabaseModuleMain.users.Read("_id",account.UserId);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User not found."));

        BasicUserResponse response = CreateBasicUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public override async Task<BasicUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        Printer.BlueLn($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = await DatabaseModuleMain.users.Read("_id", request.Id);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));
        
        // Create replacement user
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

        return response;
    }

    public override async Task<BasicUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        Printer.BlueLn($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = await DatabaseModuleMain.users.Read("_id", request.Id);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));
        
        // Delete user
        var result = DatabaseModuleMain.users.Delete("_id", request.Id);
        if (!result) throw new RpcException(new Status(StatusCode.Aborted, "Not acknowledged"));

        // Send response
        BasicUserResponse response = CreateBasicUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public static BasicUserResponse CreateBasicUserResponse(User user)
    {
        return new BasicUserResponse
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            CadastroCompleto = user.CadastroCompleto,
            EmailVerified = Timestamp.FromDateTime(user.EmailVerified),
            Name = user.Nome,
            Image = user.Image
        };
    }
}
