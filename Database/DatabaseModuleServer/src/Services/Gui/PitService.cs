namespace Database.Gui.Services;

using HTools;
using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Google.Protobuf.WellKnownTypes;
using System.Threading.Tasks;

public class PitGuiService : PitService.PitServiceBase
{
    private readonly ILogger<PitGuiService> _logger;

    public PitGuiService(ILogger<PitGuiService> logger)
    {
        _logger = logger;
    }

    
    public override async Task<Gui.Sheet> CreatePit(CreatePitRequest request, ServerCallContext context)
    {
        Printer.BlueLn($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");
        
        // Check if pit already exists
        var found = await DatabaseModuleMain.pits.Read("user_id", request.UserId, "ano", request.Ano);
        if (found != null) throw new RpcException(new Status(StatusCode.AlreadyExists, $"PIT of year {request.Ano} already exists for user_id: {request.UserId}"));

        // Create empty PIT
        PIT pit = new PIT(request.UserId, request.Ano, new Sheet());

        Gui.Sheet response = CreateSheetResponse(pit.Planilha);
        
        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public override async Task<Gui.Sheet> GetPit(GetPitRequest request, ServerCallContext context)
    {
        Printer.BlueLn($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");
        
        // Find pit
        PIT found = await DatabaseModuleMain.pits.Read("user_id", request.UserId, "ano", request.Ano);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, $"PIT of year {request.Ano} for user_id: {request.UserId} not found."));

        Gui.Sheet response = CreateSheetResponse(found.Planilha);
        
        Console.WriteLine("Response sent: " + response);

        return response;
    }
    
    public override async Task<Gui.Sheet> UpdatePit(UpdatePitRequest request, ServerCallContext context)
    {
        Printer.BlueLn($"Request received: '{request}' from host '{context.Host}' using method '{context.Method}'");
        
        // Find pit
        PIT found = await DatabaseModuleMain.pits.Read("user_id", request.UserId, "ano", request.Ano);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, $"PIT of year {request.Ano} for user_id: {request.UserId} not found."));

        // Create replacement PIT
        PIT replace = UpdatePit(found, request);

        // Replace pit
        DatabaseModuleMain.pits.Update("_id", found.Id.ToString(), replace);

        Gui.Sheet response = CreateSheetResponse(found.Planilha);
        
        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public Sheet FromSheetToSheet(Gui.Sheet request)
    {
        return new Sheet()
        {
            CHAdm = request.ChAdm,
            CHEnsino = request.ChEnsino,
            CHExtensao = request.ChExtensao,
            CHGrad = request.ChGrad,
            CHPesquisa = request.ChPesquisa,
            CHPos = request.ChPos,

            Ensino = request.Ensino.ToArray(),
            Extensao = request.Extensao.ToArray(),
            Pesquisa = request.Pesquisa.ToArray(),
            Adm = request.Adm.ToArray(),
        };
    }

    public PIT UpdatePit(PIT pit, UpdatePitRequest request)
    {
        Gui.Sheet updated = request.Sheet;

        return new PIT(pit.UserId, pit.Ano, FromSheetToSheet(updated));
    }

    public Gui.Sheet CreateSheetResponse(Sheet sheet)
    {

        Gui.Sheet response = new Gui.Sheet()
        {
            ChAdm = sheet.CHAdm,
            ChEnsino = sheet.CHEnsino,
            ChExtensao = sheet.CHExtensao,
            ChGrad = sheet.CHGrad,
            ChPesquisa = sheet.CHPesquisa,
            ChPos = sheet.CHPos
        };

        foreach (bool value in sheet.Adm) response.Adm.Add(value);
        foreach (bool value in sheet.Ensino) response.Ensino.Add(value);
        foreach (bool value in sheet.Pesquisa) response.Pesquisa.Add(value);
        foreach (bool value in sheet.Extensao) response.Extensao.Add(value);

        return response;
    }
}
