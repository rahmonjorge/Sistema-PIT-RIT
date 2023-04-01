namespace Database.Auth;

using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Google.Protobuf.WellKnownTypes;

public class UserService : UserAuthService.UserAuthServiceBase
{
    private readonly ILogger<UserService> _logger;
    private UserController _controller;

    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;

        _controller = new UserController();
    }

    public override Task<BasicUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        User newUser = new User()
        {
            Email = request.Email,
            EmailVerified = request.EmailVerified.ToDateTime(),
            Nome = request.Name,
            Image = request.Image
        };

        _controller.Create(newUser);

        BasicUserResponse response = CreateBasicUserResponse(newUser);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<BasicUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = _controller.Read("_id", request.Id);
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
        User userFound = _controller.Read("email", request.Email);

        // Send responses
        BasicUserResponse response = CreateBasicUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<BasicUserResponse> GetUserByAccount(GetUserByAccountRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");
        throw new RpcException(new Status(StatusCode.Unimplemented,$"{context.Method} unimplemented by server"));
    }

    public override Task<BasicUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = _controller.Read("_id", request.Id);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));
        
        User replacementUser = new User()
        {
            Email = request.Email,
            EmailVerified = request.EmailVerified.ToDateTime(),
            Nome = request.Name,
            Image = request.Image
        };

        // Replace user
        var result = _controller.Update("_id", request.Id, replacementUser);
        if (!result) throw new RpcException(new Status(StatusCode.Aborted, "Not acknowledged"));

        BasicUserResponse response = CreateBasicUserResponse(replacementUser);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    public override Task<BasicUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");

        // Find user
        User userFound = _controller.Read("_id", request.Id);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));
        
        // Delete user
        var result = _controller.Delete("_id", request.Id);
        if (!result) throw new RpcException(new Status(StatusCode.Aborted, "Not acknowledged"));

        // Send response
        BasicUserResponse response = CreateBasicUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    private BasicUserResponse CreateBasicUserResponse(User user)
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
