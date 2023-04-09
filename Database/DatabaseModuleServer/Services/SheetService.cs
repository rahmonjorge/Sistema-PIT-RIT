namespace Database.Sheets.Services;

using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Database.Sheets;
using Google.Protobuf.Collections;
using Google.Protobuf;

public class SheetService : SpreadsheetService.SpreadsheetServiceBase
{
    private readonly ILogger<SheetService> _logger;
    private UserController _users;
    private PitsController _pits;
    private RitsController _rits;

    public SheetService(ILogger<SheetService> logger)
    {
        _logger = logger;

        _users = new UserController();
        _pits = new PitsController();
        _rits = new RitsController();
    }

    public override Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        if (request == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Null request."));
        Console.WriteLine($"'{context.Method}' request received from '{context.Host}' at '{DateTime.UtcNow}': {request}");
        
        // Find user
        User userFound = _users.Read("_id", request.Id);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));

        // Send responses
        UserResponse response = CreateUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);


    }
    public override Task<SheetResponse> GetSheet(GetSheetRequest request, ServerCallContext context)
    {
        if (request == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Null request."));
        Console.WriteLine($"'{context.Method}' request received from '{context.Host}': {request}");

        SheetResponse response;

        if (request.Type.ToLower() == "pit")
        {
            // Find
            PIT pitFound = _pits.Read("user_id",request.UserId);
            if (pitFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.UserId + "' not found."));

            // Send response
            response = CreateSheetResponse(pitFound.Planilha);
        }
        else if (request.Type.ToLower() == "rit")
        {
            RIT ritFound = _rits.Read("user_id",request.UserId);
            if (ritFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.UserId + "' not found."));

            // Send response
            response = CreateSheetResponse(ritFound.Planilha);
        }
        else throw new RpcException(new Status(StatusCode.InvalidArgument, "Tipo de planilha inválido."));


        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }

    private UserResponse CreateUserResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            Name = user.Nome,
            Siape = user.Siape,
            Departamento = user.Departamento,
            Vinculo = user.Vinculo,
            Regime = user.Regime,
            Reducao = user.CargaHoraria
        };
    }

    private SheetResponse CreateSheetResponse(Sheet sheet)
    {
        SheetResponse response = new SheetResponse
        {
            ChGrad = sheet.CHGrad,
            ChPos = sheet.CHPos,
            ChAdm = sheet.CHAdm,
            ChEnsino = sheet.CHEnsino,
            ChExtensao = sheet.CHExtensao,
            ChPesquisa = sheet.CHPesquisa,
        };

        response.Ensino.Add(sheet.Ensino);
        response.Extensao.Add(sheet.Extensao);
        response.Pesquisa.Add(sheet.Pesquisa);
        response.Adm.Add(sheet.Adm);

        return response;
    }
    
}
