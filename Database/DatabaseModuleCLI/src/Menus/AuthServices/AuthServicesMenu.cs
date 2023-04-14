namespace DatabaseModuleCLI.Menus;

using HTools;

public class AuthServicesMenu
{
    private string? input;
    private string? error;
    private bool exit = false;
    private string[] services = 
    {
        "UserAuthService", 
        "SessionAuthService", 
        "AccountAuthService", 
        "VerificationTokenAuthService"
    };

    public string Run()
    {
        Controller.control.Push("Auth");
        string output = "";
        Printer.PrintMenuOptions("Services Avaliable:", this.services);

        while (!exit)
        {
            Printer.RedLn(error);
            input = Input.GetUserInput(Controller.GetControlString());

            switch (input)
            {
                case "1":
                    new UserAuthServicesMenu().Run();
                    break;
                case "2":
                    SessionClientMenu();
                    break;
                case "3":
                    AccountClientMenu();
                    break;
                case "4":
                    TokenClientMenu();
                    break;
                case "back":
                case "exit":
                    exit = true;
                    output = input;
                    break;
                default:
                    error = "Invalid input";
                    break;
            }
        }
        Controller.control.Pop();
        return output;
    }

    

    private void SessionClientMenu()
    {
        Console.WriteLine("Session Service Client started.");
        Console.WriteLine("Commands avaliable: create, get, update, delete, exit");
        while (true)
        {
            var args = Input.GetCommand("Services/Sessions>");

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

    private void SheetClientMenu()
    {
        Console.WriteLine("Sheet Service Client started.");
        Console.WriteLine("Commands avaliable: user, sheet, exit");
        while (true)
        {
            var args = Input.GetCommand("Services/Sheets>");

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

    private void TokenClientMenu()
    {
        throw new NotImplementedException();
    }

    private void AccountClientMenu()
    {
        Console.WriteLine("Account Service Client started.");
        Console.WriteLine("Commands avaliable: link, unlink, exit");
        while (true)
        {
            var args = Input.GetCommand("Services/Account>");

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