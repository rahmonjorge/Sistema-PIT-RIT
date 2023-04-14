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
        Printer.LogRequest(request, context.Host, context.Method);
        
        // Check if pit already exists
        var found = await DatabaseCore.pits.ReadAsync("user_id", request.UserId, "ano", request.Ano);
        if (found != null) throw new RpcException(new Status(StatusCode.AlreadyExists, $"PIT of year {request.Ano} already exists for user_id: {request.UserId}"));

        // Create empty PIT
        PIT pit = new PIT(request.UserId, request.Ano, new Sheet());

        // Add to database
        DatabaseCore.pits.Create(pit);

        Gui.Sheet response = CreateSheetResponse(pit.Planilha);
        
        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public override async Task<Gui.Sheet> GetPit(GetPitRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);
        
        // Find pit
        PIT found = await DatabaseCore.pits.ReadAsync("user_id", request.UserId, "ano", request.Ano);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, $"PIT of year {request.Ano} for user_id: {request.UserId} not found."));

        Gui.Sheet response = CreateSheetResponse(found.Planilha);
        
        Console.WriteLine("Response sent: " + response);

        return response;
    }
    
    public override async Task<Gui.Sheet> UpdatePit(UpdatePitRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);
        
        // Find pit
        PIT found = await DatabaseCore.pits.ReadAsync("user_id", request.UserId, "ano", request.Ano);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, $"PIT of year {request.Ano} for user_id: {request.UserId} not found."));

        // Create replacement PIT
        PIT replace = GetUpdatedPit(found, request);

        // Replace pit
        DatabaseCore.pits.Update("_id", found.Id.ToString(), replace);

        Gui.Sheet response = CreateSheetResponse(replace.Planilha);
        
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

    public PIT GetUpdatedPit(PIT pit, UpdatePitRequest request)
    {
        pit.Planilha = FromSheetToSheet(request.Sheet);
        return pit;
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
