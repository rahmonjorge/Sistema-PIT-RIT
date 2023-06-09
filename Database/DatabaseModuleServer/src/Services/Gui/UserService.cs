namespace Database.Gui.Services;

using HTools;
using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Google.Protobuf.WellKnownTypes;
using System.Threading.Tasks;

public class UserGuiService : UserService.UserServiceBase
{
    private readonly ILogger<UserGuiService> _logger;

    public UserGuiService(ILogger<UserGuiService> logger)
    {
        _logger = logger;
    }

    public override async Task<UserInfo> CompletarCadastro(CompletarCadastroRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        // Find user
        User found = await DatabaseCore.users.ReadAsync("_id", request.Id);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));

        // Create completed user
        User completed = CompleteUser(found, request);

        // Update user
        DatabaseCore.users.Update("_id", request.Id, completed);

        UserInfo response = CreateUserInfoResponse(completed, true);

        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public override async Task<UserInfo> UpdateUserInfo(UpdateUserInfoRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        // Find user
        User found = await DatabaseCore.users.ReadAsync("_id", request.Id);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));

        // Create updated user
        User updated = UpdateUser(found, request);

        // Update user
        DatabaseCore.users.Update("_id", request.Id, updated);

        UserInfo response = CreateUserInfoResponse(updated, updated.CadastroCompleto);

        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public override async Task<UserInfo> GetUserInfo(UserIdRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        // Find user
        User found = await DatabaseCore.users.ReadAsync("_id", request.Id);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));

        UserInfo response = CreateUserInfoResponse(found, found.CadastroCompleto);

        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public override async Task<Anos> GetAnosFromUser(UserIdRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        // Find user
        User found = await DatabaseCore.users.ReadAsync("_id", request.Id);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));
    
        List<PIT> pits = DatabaseCore.pits.ReadMany("user_id", request.Id); // Find all PITs
        List<RIT> rits = DatabaseCore.rits.ReadMany("user_id", request.Id); // Find all RITs    

        List<Ano> anos = new List<Ano>();

        // Loop na lista de pits. Adiciona um novo ano para cada pit e verifica se o ano possui um rit ao checar a lista de rits.
        foreach (PIT pit in pits) if (pit != null) anos.Add(new Ano {Ano_ = pit.Ano, Rit = rits.Exists(r => r.Ano == pit.Ano)});

        Anos response = CreateAnosResponse(anos);

        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public User CompleteUser(User user, CompletarCadastroRequest request)
    {
        user.Nome = request.Name;
        user.Siape = request.Siape;
        user.Departamento = request.Dpto;
        user.Vinculo = request.Vinculo;
        user.Regime = request.Regime;
        user.CargaHoraria = request.Reducao;
        user.CadastroCompleto = true;

        return user;
    }

    public User UpdateUser(User user, UpdateUserInfoRequest request)
    {
        user.Nome = request.Name;
        user.Siape = request.Siape;
        user.Departamento = request.Dpto;
        user.Vinculo = request.Vinculo;
        user.Regime = request.Regime;
        user.CargaHoraria = request.Reducao;

        return user;
    }

    public UserInfo CreateUserInfoResponse(User user, bool cadastroCompleto)
    {
        return new UserInfo()
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            CadastroCompleto = cadastroCompleto,
            EmailVerified = Timestamp.FromDateTime(user.EmailVerified),
            Name = user.Nome,
            Image = user.Image,
            Siape = user.Siape,
            Dpto = user.Departamento,
            Vinculo = user.Vinculo,
            Regime = user.Regime,
            Reducao = user.CargaHoraria,
        };
    }

    public Anos CreateAnosResponse(List<Ano> anos)
    {
        var response = new Anos();
        foreach (Ano ano in anos) response.Anos_.Add(ano);
        return response;
    }
}
