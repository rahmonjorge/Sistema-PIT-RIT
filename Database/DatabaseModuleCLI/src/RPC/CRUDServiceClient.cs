namespace DatabaseModuleCLI;

using Database.Crud;
using Grpc.Net.Client;

using static Database.Crud.CRUDService;

class CRUDClient
{
    private CRUDServiceClient _client;

    public CRUDClient(GrpcChannel channel)
    {
        _client = new CRUDServiceClient(channel);
    }

    public ShowCollectionResponse ShowCollection(string collection)
    {
        var request = new ShowCollectionRequest()
        {
            Collection = collection
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _client.ShowCollection(request);

        Console.WriteLine("Reply received: " + reply);

        return reply;
    }
}