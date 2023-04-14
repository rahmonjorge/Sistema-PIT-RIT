namespace DatabaseModuleCLI.Menus;

using HTools;
using Google.Protobuf.Collections;

class CrudMenu
{
    public void Run()
    {
        Console.Clear();
        for (string? input = "" ; input != "exit" ; Console.Clear())
        {
            Console.WriteLine($"Choose a database to view. Databases avaliable: {Utils.ArrayToString(ClientMain.databasesAvaliable)}");
            input = Input.GetUserInput("CRUD>");

            if (input == null || !ClientMain.databasesAvaliable.Contains(input)) 
            {
                Printer.RedLn("Invalid input.");
                continue;
            } else ShowDocuments(input, ClientMain.crudService.ShowCollection(input).Data);
        }
    }

    public void ShowDocuments(string collectionName, RepeatedField<String> documents)
    {
        Console.Clear();
        Printer.GreenLn($"Showing '{collectionName}' collection with {documents.Count} documents:");
        for (int i = 0; i < documents.Count; i++) Console.WriteLine($"{i++}: {documents[i]}");
        Printer.BlueLn("Press [enter] to continue.");
        Input.GetUserInput("CRUD>");
    }
}