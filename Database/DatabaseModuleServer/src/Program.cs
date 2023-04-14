using HTools;
using Database.Auth.Services;
using Database.Sheets.Services;
using Database.Gui.Services;
using Database.Requerimento.Services;
using Database.CRUD.Services;

/// WARNING: Check if IP address is added at cloud.mongodb.com.
public static class DatabaseModuleMain
{
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

        // database.crud.proto
        app.MapGrpcService<_CRUDService>();
        
        // database.gui.proto
        app.MapGrpcService<UserGuiService>();
        app.MapGrpcService<PitGuiService>();
        app.MapGrpcService<RitGuiService>();

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