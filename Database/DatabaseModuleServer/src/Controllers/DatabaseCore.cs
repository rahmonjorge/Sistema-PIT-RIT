namespace DatabaseModule.Controllers;

using MongoDB.Driver;
using DatabaseModule.Entities;

static class DatabaseCore
{
    // Connection
    private static string? connectionString = GetConnectionString("MONGODB_URI");
    private static MongoClient client = new MongoClient(connectionString);
    private static IMongoDatabase _database = client.GetDatabase("pit-rit");

    // Controllers
    public static readonly AccountController accounts = new AccountController(_database.GetCollection<Account>("accounts"));
    public static readonly PitsController pits = new PitsController(_database.GetCollection<PIT>("pits"));
    public static readonly RitsController rits = new RitsController(_database.GetCollection<RIT>("rits"));
    public static readonly SessionController sessions = new SessionController(_database.GetCollection<Session>("sessions"));
    public static readonly UserController users = new UserController(_database.GetCollection<User>("users"));
    public static readonly VerificationTokenController tokens = new VerificationTokenController(_database.GetCollection<VerificationToken>("verification_tokens"));

    private static string GetConnectionString(string environmentVariable)
    {
        string? connectionString = Environment.GetEnvironmentVariable(environmentVariable);
        if (connectionString == null) throw new KeyNotFoundException($"'{environmentVariable}' environmental variable not found.");
        else return connectionString;
    }
}