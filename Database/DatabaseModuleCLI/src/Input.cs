namespace DatabaseModuleClient;

static class Input
{
    public static readonly string STANDARD_PREFIX = "->";

    public static string[]? GetCommand(string prefix)
    {
        string? input = GetUserInput();
        return input?.Split(' ');
    }

    public static string? GetUserInput(string prefix)
    {
        Console.Write(prefix + " ");
        string? input = Console.ReadLine();
        return input?.ToLower();
    }

    public static string? GetUserInput() => GetUserInput(STANDARD_PREFIX);
}