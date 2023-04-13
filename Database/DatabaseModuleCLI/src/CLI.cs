using Database.Auth;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;
using HTools;

using static Database.Auth.VerificationTokenAuthService;
using static Database.Auth.AccountAuthService;

namespace DatabaseModuleClient;

static class CLI
{
    private static string? error;

    public static void Run()
    {
        Console.Clear();
        for (string? input = ""; input != "exit" ; Console.Clear())
        {
            Printer.PrintRainbowLn("Welcome to the Database Module CLI.");
            Console.WriteLine("1 - Open services client\n2 - Remote CRUD");
            Printer.RedLn(error);

            input = Input.GetUserInput();
            
            switch(input)
            {
                case "1":
                    ChooseService(ClientMain.servicesAvaliable);
                    break;
                case "2":
                    Console.WriteLine("Work in progress");
                    break;
                default:
                    error = "Invalid input.";
                    break;
            }
        }
        Printer.GreenLn("Thank you for using the Database Module CLI!");
        Console.WriteLine("Made by Rahmon Jorge");
    }

    private static void Exit()
    {
        error = null;
        Console.Clear();
    }

    private static void ChooseService(string[] clients)
    {
        Console.Clear();
        for (string? input = "" ; input != "exit" ; Console.Clear())
        {
            Console.WriteLine($"Choose a service. Services avaliable: {Utils.ArrayToString(clients)}");

            input = Input.GetUserInput("Services>");

            switch (input)
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
                    Printer.RedLn("Invalid input.");
                    break;
            }
        }
        Exit();
    }

    private static void UserClientMenu()
    {
        Console.WriteLine("User Service Client started.");
        Console.WriteLine("Commands avaliable: create, delete, exit");
        while (true)
        {
            var args = Input.GetCommand("Services>Users>");

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
                    var input = Input.GetUserInput();
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
            var args = Input.GetCommand("Services>Sessions>");

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
            var args = Input.GetCommand("Services>Sheets>");

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
                    Int32 ano = Utils.StringToInt32(args[3]);
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
        Console.WriteLine("Account Service Client started.");
        Console.WriteLine("Commands avaliable: link, unlink, exit");
        while (true)
        {
            var args = Input.GetCommand("Services>Account>");

            switch (args[0])
            {
                case "link":
                    // var email = args[1];
                    // Console.WriteLine($"Creating new user with email: {email}");
                    // ClientMain.userService.CreateUser(email);
                    break;
                case "unlink":
                    // var id = args[1];
                    // Console.WriteLine($"Looking for user with id: {id}");
                    // var found = ClientMain.userService.GetUser(id);
                    // Console.WriteLine($"User '{found.Name}' found. Are you sure you want to delete it? (Y/N)");
                    // var input = GetUserInput();
                    // if (input == "y")
                    // {
                    //     var result = ClientMain.userService.DeleteUser(id);
                    //     Console.WriteLine($"User '{result.Name}' successfully removed from database.");
                    // }
                    // else continue;
                    break;
                case "exit":
                    return;
                default:
                    break;
            }
        }
    }
}