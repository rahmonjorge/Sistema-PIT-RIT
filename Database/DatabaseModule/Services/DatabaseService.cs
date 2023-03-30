using Grpc.Core;
using DatabaseModule;

namespace Database.Auth;

public class DatabaseService : UserAuthService.UserAuthServiceBase
{
    private readonly ILogger<DatabaseService> _logger;
    public DatabaseService(ILogger<DatabaseService> logger)
    {
        _logger = logger;
    }

    public override Task<BasicUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        return Task.FromResult(new BasicUserResponse
        {
            Id = "id lindo demais modeu",
            Email = "rahmonjorge",
            CadastroCompleto = false,
            EmailVerified = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.Now),
            Name = "shrek 6",
            Image = "https://picsum.photos/1080/720"
        });
    }
}
