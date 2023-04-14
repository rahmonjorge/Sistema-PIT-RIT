namespace Database.CRUD.Services;

using HTools;
using System.Threading.Tasks;
using Database.Crud;
using Grpc.Core;
using DatabaseModule.Controllers;

public class _CRUDService : CRUDService.CRUDServiceBase
{
    private readonly ILogger<_CRUDService> _logger;

    public _CRUDService(ILogger<_CRUDService> logger)
    {
        _logger = logger;
    }

    public override Task<ShowCollectionResponse> ShowCollection(ShowCollectionRequest request, ServerCallContext context)
    {
        Printer.LogRequest(request, context.Host, context.Method);

        var collection = request.Collection.ToLower();
        ShowCollectionResponse response = new ShowCollectionResponse();

        switch (collection)
        {
            case "accounts":
                foreach (var value in DatabaseCore.accounts.ReadAll()) response.Data.Add(value.ToString());
                break;
            case "pits":
                foreach (var value in DatabaseCore.pits.ReadAll()) response.Data.Add(value.ToString());
                break;
            case "rits":
                foreach (var value in DatabaseCore.rits.ReadAll()) response.Data.Add(value.ToString());
                break;
            case "sessions":
                foreach (var value in DatabaseCore.sessions.ReadAll()) response.Data.Add(value.ToString());
                break;
            case "users":
                foreach (var value in DatabaseCore.users.ReadAll()) response.Data.Add(value.ToString());
                break;
            case "verification_tokens":
                foreach (var value in DatabaseCore.tokens.ReadAll()) response.Data.Add(value.ToString());
                break;
            default:
                response.Error = $"Collection '{collection}' does not exist in the database.";
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Collection '{collection}' does not exist in the database."));
        }

        Console.WriteLine("Response sent: " + response);

        return Task.FromResult(response);
    }
}