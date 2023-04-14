namespace DatabaseModuleCLI;

using HTools;
using DatabaseModuleCLI.Menus;

static class Controller
{
    public static Stack<string> control = new Stack<string>();

    public static void Start()
    {
        new MainMenu().Run();

        Printer.GreenLn("Thank you for using the Database Module CLI!");
        Console.WriteLine("Made by Rahmon Jorge");
    }

    public static string GetControlString()
    {
        string output = "";
        foreach (string s in control.Reverse()) output += s + "/";
        output = output.Remove(output.Length - 1, 1);
        return output + ">";
    }
}