using Database.Auth.Services;
using Database.Sheets.Services;
using MongoDB.Driver;

/// WEBAPP AREA ///
public static class DatabaseModuleMain
{
    public static IMongoDatabase _database;

    public static void Main(string[] args)
    {
        /// WARNING: Check if IP address is added at cloud.mongodb.com.

        // setting up mongodb connection
        string? connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        if (connectionString == null) Console.WriteLine("'MONGODB_URI' environmental variable not found.");
        MongoClient client = new MongoClient(connectionString);
        _database = client.GetDatabase("pit-rit");

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

        // Add services to the container.
        builder.Services.AddGrpc();

        WebApplication app = builder.Build();

        // preciso chamar um método que está em UserDatabaseService a partir do SessionDatabaseService.
        // app.Services.GetService(typeof(UserDatabaseService)); <------------ sera que é essa?

        // Configure the HTTP request pipeline.
        
        // Auth Services
        app.MapGrpcService<AccountService>();
        app.MapGrpcService<SessionService>();
        app.MapGrpcService<UserService>();
        app.MapGrpcService<VerificationTokenService>();
        
        // Spreadsheets Services
        app.MapGrpcService<SheetService>();

        app.MapGet("/", () => "gRPC server is up and running. Waiting for gRPC clients.");

        app.Run();
    }
}