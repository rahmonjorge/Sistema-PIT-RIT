using Database.Auth;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;

using static Database.Auth.VerificationTokenAuthService;
using static Database.Auth.AccountAuthService;

namespace DatabaseModuleClient;

static class CLI
{
    public static void Run()
    {
        Console.WriteLine("GRPC Server Started.");

        var clientChosen = ChooseClientMenu(ClientMain.servicesAvaliable);

        switch (clientChosen)
        {
            case "user":
                UserClientMenu();
                break;
            case "session":
                SessionClientMenu();
                break;
            case "sheet":
                SheetClientMenu();
                break;
            case "account":
                AccountClientMenu();
                break;
            case "token":
                TokenClientMenu();
                break;
            default:
                break;
        }

        
    }

    private static void UserClientMenu()
    {
        Console.WriteLine("User Service Client started.");
        Console.WriteLine("Commands avaliable: create, delete, exit");
        while (true)
        {
            var args = GetCommand("User Service>");

            switch (args[0])
            {
                case "create":
                    var email = args[1];
                    Console.WriteLine($"Creating new user with email: {email}");
                    ClientMain.userService.CreateUser(email);
                    break;
                case "delete":
                    var id = args[1];
                    Console.WriteLine($"Looking for user with id: {id}");
                    var found = ClientMain.userService.GetUser(id);
                    Console.WriteLine($"User '{found.Name}' found. Are you sure you want to delete it? (Y/N)");
                    var input = GetUserInput();
                    if (input == "y")
                    {
                        var result = ClientMain.userService.DeleteUser(id);
                        Console.WriteLine($"User '{result.Name}' successfully removed from database.");
                    }
                    else continue;
                    break;
                case "exit":
                    return;
                default:
                    break;
            }
        }
    }

    private static void SessionClientMenu()
    {
        Console.WriteLine("Session Service Client started.");
        Console.WriteLine("Commands avaliable: create, get, update, delete, exit");
        while (true)
        {
            var args = GetCommand("Session Service>");

            switch (args[0])
            {
                case "create":
                    var token = args[1];
                    ClientMain.sessionService.CreateSession(token);
                    break;
                case "get":
                    token = args[1];
                    var response = ClientMain.sessionService.GetSessionAndUser(token);
                    Console.WriteLine("Session found:");
                    Console.WriteLine(response.Session);
                    Console.WriteLine("User found:");
                    Console.WriteLine(response.User);
                    break;
                case "update":
                    break;
                case "delete":
                    token = args[1];
                    Console.WriteLine($"Looking for session with token: {token}");
                    var found = ClientMain.sessionService.DeleteSession(token);
                    Console.WriteLine($"Session '{found.SessionToken}' deleted.");
                    break;
                case "exit":
                    return;
                default:
                    break;
            }
        }
    }

    private static void SheetClientMenu()
    {
        Console.WriteLine("Sheet Service Client started.");
        Console.WriteLine("Commands avaliable: user, sheet, exit");
        while (true)
        {
            var args = GetCommand("Sheet Service>");

            switch (args[0])
            {
                case "user":
                    var userId = args[1];
                    Console.WriteLine($"Looking for user with id: {userId}");
                    var getUserReply = ClientMain.sheetService.GetUser(userId);
                    Console.WriteLine($"User '{getUserReply.Name}' found:");
                    Console.WriteLine(getUserReply);
                    break;
                case "sheet":
                    userId = args[1];
                    string type = args[2];
                    Int32 ano = StringToInt32(args[3]);
                    var getSheetReply = ClientMain.sheetService.GetSheet(userId, type, ano);
                    Console.WriteLine(getSheetReply);
                    break;
                case "exit":
                    return;
                default:
                    break;
            }
        }
    }

    private static void TokenClientMenu()
    {
        throw new NotImplementedException();
    }

    private static void AccountClientMenu()
    {
        throw new NotImplementedException();
    }


    private static string ChooseClientMenu(string[] clients)
    {
        Console.WriteLine($"Choose a client. Clients avaliable: {ArrayToString(clients)}");

        var input = GetUserInput();

        if (clients.Contains(input)) return input;
        else throw new ArgumentNullException("Invalid input.");
    }

    private static string[] GetCommand(string prefix)
    {
        Console.Write(prefix + " ");
        string? input = Console.ReadLine();
        if (input == null) throw new ArgumentNullException("Invalid input.");
        input = input.ToLower();
        return input.Split(' ');
    }

    private static string GetUserInput()
    {
        Console.Write("-> ");
        string? input = Console.ReadLine();
        if (input == null) throw new ArgumentNullException("Invalid input.");
        return input.ToLower();
    }

    private static string ArrayToString(string[] array) => string.Join(", ", array);

    private static bool StringToBool(string str) => bool.Parse(str.ToLower());

    private static Int32 StringToInt32(string str) => Int32.Parse(str.ToLower());
}