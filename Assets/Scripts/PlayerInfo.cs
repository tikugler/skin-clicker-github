// this is a temporary class
public static class PlayerInfo
{
    public static string playerID;
    public static string username;
    public static int score;

    public static bool LoggedIn { get { return username != null; } }
    public static void LogOut()
    {
        username = null;
    }
}