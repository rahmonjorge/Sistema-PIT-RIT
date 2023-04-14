namespace HTools;

static class Utils
{
    public static string ArrayToString(string[] array) => string.Join(", ", array);

    public static string ArrayToString(string[] array, string join) => string.Join(join, array);

    public static bool StringToBool(string str) => bool.Parse(str.ToLower());

    public static Int32 StringToInt32(string str) => Int32.Parse(str.ToLower());
}