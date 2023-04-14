namespace DatabaseModuleCLI.Menus;

using HTools;

class MainMenu
{
    private string? input;
    private string? error;
    private bool exit = false;
    private string[] options = {"Auth", "CRUD", "Sheets"};

    public void Run()
    {
        Console.Clear();
        Controller.control.Push("DBMCLI");
        Printer.Yellow("Welcome to the "); Printer.PrintRainbowLn("Database Module CLI.");
        Printer.PrintMenuOptions("Protocols avaliable:", options);
        while (!exit)
        {
            Printer.RedLn(error);
            this.input = Input.GetUserInput(Controller.GetControlString());

            switch (input)
            {
                case "1":
                    input = new AuthServicesMenu().Run();
                    if (input == "exit") exit = true;
                    break;
                case "2":
                    new CrudMenu().Run();
                    break;
                case "back":
                case "exit":
                    exit = true;
                    return;
                default:
                    error = "Invalid Input.";
                    break;
            }
        }
        Controller.control.Pop();
    }
}