using Database.Auth;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;

namespace DatabaseModuleCLI
{
    class ClientMain
    {
        // The port number must match the port of the gRPC server.
        private static GrpcChannel channel = GrpcChannel.ForAddress("http://26.52.183.15:6924"); // TEM QUE SER HTTP SE NAO N PEGA

        public static readonly UserServiceClient userService = new UserServiceClient(channel);
        public static readonly SessionServiceClient sessionService = new SessionServiceClient(channel);
        public static readonly SheetServiceClient sheetService = new SheetServiceClient(channel);
        public static readonly CRUDClient crudService = new CRUDClient(channel);
        public static readonly string[] databasesAvaliable = {"accounts", "pits", "rits", "sessions", "users", "verification_tokens"};

        public static void Main()
        {        
            Controller.Start();
        }
    }
}

