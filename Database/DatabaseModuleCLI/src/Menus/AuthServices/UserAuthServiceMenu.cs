namespace DatabaseModuleCLI.Menus;

using HTools;

public class UserAuthServicesMenu
{
    private string? input;
    private string? error;
    private bool exit = false;
    private string[] methods = 
    {
        "CreateUser",
        "GetUser",
        "GetUserByEmail",
        "GetUserByAccount",
        "UpdateUser",
        "DeleteUser"
    };

    public void Run()
    {
        Controller.control.Push("Users");
        error = null;
        Printer.PrintMenuOptions("Methods avaliable:", methods);

        while (!exit)
        {
            Printer.RedLn(error);

            input = Input.GetUserInput(Controller.GetControlString());

            switch (input)
            {
                case "1":
                    CreateUser();
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    DeleteUser();
                    break;
                case "back":
                case "exit":
                    exit = true;
                    break;
                default:
                    error = "Invalid input";
                    break;
            }
        }
        Controller.control.Pop();
    }

    public void CreateUser()
    {
        
    }

    public void DeleteUser()
    {

    }
}