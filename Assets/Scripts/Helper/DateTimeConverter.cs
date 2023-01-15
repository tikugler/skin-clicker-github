using System;
public static class DateTimeConverter
{
    public static int ToUnixTimeSeconds(DateTime date)
    {
        DateTime point = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan time = date.Subtract(point);

        return (int)time.TotalSeconds;
    }

    public static DateTime UnixTimeStampToDateTime(double unixTime)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTime).ToUniversalTime();
        return dateTime;
    }
    public static int ToUnixTimeSeconds()
    {
        return ToUnixTimeSeconds(DateTime.UtcNow);
    }
}