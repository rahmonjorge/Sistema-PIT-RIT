namespace Database.Gui.Services;

using HTools;
using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using System.Threading.Tasks;

public class RitGuiService : RitService.RitServiceBase
{
    private readonly ILogger<RitGuiService> _logger;

    public RitGuiService(ILogger<RitGuiService> logger)
    {
        _logger = logger;
    }

    public override async Task<Gui.Sheet> CreateRit(CreateRitRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);
        
        // Check if rit already exists
        var found = await DatabaseCore.rits.ReadAsync("user_id", request.UserId, "ano", request.Ano);
        if (found != null) throw new RpcException(new Status(StatusCode.AlreadyExists, $"rit of year {request.Ano} already exists for user_id: {request.UserId}"));

        // Create empty rit
        RIT rit = new RIT(request.UserId, request.Ano, new Sheet());

        // Add to database
        DatabaseCore.rits.Create(rit);

        Gui.Sheet response = CreateSheetResponse(rit.Planilha);
        
        Console.WriteLine("Response sent: " + response);

        return response;
    }

    public override async Task<Gui.Sheet> GetRit(GetRitRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);
        
        // Find rit
        RIT found = await DatabaseCore.rits.ReadAsync("user_id", request.UserId, "ano", request.Ano);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, $"rit of year {request.Ano} for user_id: {request.UserId} not found."));

        Gui.Sheet response = CreateSheetResponse(found.Planilha);
        
        Console.WriteLine("Response sent: " + response);

        return response;
    }
    
    public override async Task<Gui.Sheet> UpdateRit(UpdateRitRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);
        
        // Find rit
        RIT found = await DatabaseCore.rits.ReadAsync("user_id", request.UserId, "ano", request.Ano);
        if (found == null) throw new RpcException(new Status(StatusCode.NotFound, $"rit of year {request.Ano} for user_id: {request.UserId} not found."));

        // Create replacement rit
        RIT replace = GetUpdatedrit(found, request);

        // Replace rit
        DatabaseCore.rits.Update("_id", found.Id.ToString(), replace);

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

    public RIT GetUpdatedrit(RIT rit, UpdateRitRequest request)
    {
        rit.Planilha = FromSheetToSheet(request.Sheet);
        return rit;
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
