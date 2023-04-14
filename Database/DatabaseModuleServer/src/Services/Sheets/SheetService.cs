namespace Database.Sheets.Services;

using HTools;
using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Database.Sheets;
using Google.Protobuf.Collections;
using Google.Protobuf;

public class SheetService : SpreadsheetService.SpreadsheetServiceBase
{
    private readonly ILogger<SheetService> _logger;

    public SheetService(ILogger<SheetService> logger)
    {
        _logger = logger;
    }

    public override async Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        if (request == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Null request."));
        Printer.LogRequest(request, context.Host, context.Method);
        
        // Find user
        User userFound = await DatabaseCore.users.ReadAsync("_id", request.Id);
        if (userFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.Id + "' not found."));

        // Send responses
        UserResponse response = CreateUserResponse(userFound);

        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public override Task<SheetResponse> GetSheet(GetSheetRequest request, ServerCallContext context)
    {
        if (request == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Null request."));
        Printer.LogRequest(request, context.Host, context.Method);

        SheetResponse response;

        if (request.Type.ToLower() == "pit")
        {
            // Find
            PIT pitFound = DatabaseCore.pits.Read("user_id", request.UserId, "ano", request.Ano); // TODO: BUSCAR NAO SÓ POR USUÁRIO MAS POR ANO TAMBÉM 
            if (pitFound == null) throw new RpcException(new Status(StatusCode.NotFound, "User with id: '" + request.UserId + "' not found."));

            // Send response
            response = CreateSheetResponse(pitFound.Planilha);
        }
        else if (request.Type.ToLower() == "rit")
        {
            RIT ritFound = DatabaseCore.rits.Read("user_id",request.UserId, "ano", request.Ano);
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
