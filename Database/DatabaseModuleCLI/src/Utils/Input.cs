namespace HTools;

static class Input
{
    public static readonly string STANDARD_PREFIX = "->";

    public static string[]? GetCommand(string prefix)
    {
        string? input = GetUserInput(prefix);
        return input?.Split(' ');
    }

    public static string? GetUserInput(string prefix)
    {
        Printer.Yellow(prefix + " ");
        string? input = Console.ReadLine();
        return input?.ToLower();
    }

    public static string? GetUserInput() => GetUserInput(STANDARD_PREFIX);
}