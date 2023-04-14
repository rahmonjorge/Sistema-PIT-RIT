namespace Database.Requerimento.Services;

using HTools;
using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Google.Protobuf.WellKnownTypes;
using System.Threading.Tasks;

public class UserRequerimentoService : UserService.UserServiceBase
{
    private readonly ILogger<UserRequerimentoService> _logger;

    public UserRequerimentoService(ILogger<UserRequerimentoService> logger)
    {
        _logger = logger;
    }

    public override Task<UserInfo> GetUserInfo(UserIdRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        // Find user
        User found = DatabaseCore.users.Read("_id", request.Id);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));

        UserInfo response = new UserInfo
        {
            Name = found.Nome,
            Siape = found.Siape,
            Dpto = found.Departamento,
            Regime = found.Regime
        };

        return Task.FromResult(response);
    }
}
