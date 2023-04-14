namespace DatabaseModuleCLI;

using Database.Sheets;
using Grpc.Net.Client;

using static Database.Sheets.SpreadsheetService;

class SheetServiceClient
{
    private SpreadsheetServiceClient _client;

    public SheetServiceClient(GrpcChannel channel)
    {
        _client = new SpreadsheetServiceClient(channel);
    }

    public UserResponse GetUser(string id)
    {
        var request = new GetUserRequest()
        {
            Id = id
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _client.GetUser(request);

        Console.WriteLine("Reply received: " + reply);

        return reply;
    }

    public SheetResponse GetSheet(string userId, string type, Int32 ano)
    {
        var request = new GetSheetRequest()
        {
            UserId = userId,
            Type = type,
            Ano = ano
        };

        Console.WriteLine("Request sent: " + request);

        var reply = _client.GetSheet(request);

        Console.WriteLine("Reply received: " + reply);

        return reply;
    }
}