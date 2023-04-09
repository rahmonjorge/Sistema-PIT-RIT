using Database.Auth;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;

namespace DatabaseModuleClient;

class ClientMain
{
    // The port number must match the port of the gRPC server.
    private static GrpcChannel channel = GrpcChannel.ForAddress("http://26.52.183.15:6924"); // TEM QUE SER HTTP SE NAO N PEGA

    public static readonly UserServiceClient userService = new UserServiceClient(channel);
    public static readonly SessionServiceClient sessionService = new SessionServiceClient(channel);
    public static readonly SheetServiceClient sheetService = new SheetServiceClient(channel);

    public static readonly string[] servicesAvaliable = {"user", "session", "account", "token", "sheet"};

    public static void Main()
    {        
        CLI.Run();
    }
}
