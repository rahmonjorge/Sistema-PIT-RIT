using Database.Auth;
using DatabaseModule.Services;
using DatabaseModule.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

/// WEBAPP AREA ///
public static class DatabaseModuleMain
{

    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

        // Add services to the container.
        builder.Services.AddGrpc();

        WebApplication app = builder.Build();

        // preciso chamar um método que está em UserDatabaseService a partir do SessionDatabaseService.
        // app.Services.GetService(typeof(UserDatabaseService)); <------------ sera que é essa?

        // Configure the HTTP request pipeline.

        app.MapGrpcService<UserService>();
        app.MapGrpcService<SessionService>();

        app.MapGet("/", () => "gRPC server is up and running. Waiting for gRPC clients.");

        app.Run();
    }
}