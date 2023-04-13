using HTools;
using Database.Auth.Services;
using Database.Sheets.Services;
using Database.Gui.Services;
using Database.Requerimento.Services;

/// WARNING: Check if IP address is added at cloud.mongodb.com.
public static class DatabaseModuleMain
{
    /*
    #region Private
    private static string? connectionString = GetConnectionString("MONGODB_URI");
    private static MongoClient client = new MongoClient(connectionString);
    #endregion

    #region Public
    // Databases
    public static readonly string databaseName = "pit-rit";
    public static readonly IMongoDatabase _database = client.GetDatabase(databaseName);

    // Controllers
    // TODO: CLASSE PARA OS CONTROLLERS COM TRY CATCH DE TIMEOUT
    public static readonly AccountController accounts = new AccountController();
    public static readonly PitsController pits = new PitsController();
    public static readonly RitsController rits = new RitsController();
    public static readonly SessionController sessions = new SessionController();
    public static readonly UserController users = new UserController();
    public static readonly VerificationTokenController tokens = new VerificationTokenController();
    #endregion
    */

    // Module names
    public static readonly Tuple<string, string> client1 = new Tuple<string, string>("26.52.183.15:6924", "Gian");

    public static void Main(string[] args)
    {
        Printer.PrintRainbowLn("Bem vindo ao servidor do módulo da base de dados!!");

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        
        // database.auth.proto
        app.MapGrpcService<AccountService>();
        app.MapGrpcService<SessionService>();
        app.MapGrpcService<UserService>();
        app.MapGrpcService<VerificationTokenService>();
        
        // database.gui.proto
        app.MapGrpcService<UserGuiService>();
        app.MapGrpcService<PitGuiService>();

        // database.requerimento.proto
        app.MapGrpcService<UserRequerimentoService>();

        // database.sheets.proto
        app.MapGrpcService<SheetService>();

        app.MapGet("/", () => "gRPC server is up and running. Waiting for gRPC clients.");

        app.Run();

    }

    public static string GetConnectionString(string environmentVariable)
    {
        string? connectionString = Environment.GetEnvironmentVariable(environmentVariable);
        if (connectionString == null) throw new KeyNotFoundException($"'{environmentVariable}' environmental variable not found.");
        else return connectionString;
    }
}