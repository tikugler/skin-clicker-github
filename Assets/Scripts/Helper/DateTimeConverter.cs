using System;
using UnityEngine;

public static class DateTimeConverter
{
    public static int ToUnixTimeSeconds(DateTime date)
    {
        DateTime point = new DateTime(1970, 1, 1);
        TimeSpan time = date.Subtract(point);

        return (int)time.TotalSeconds;
    }

    public static int ToUnixTimeSeconds()
    {
        return ToUnixTimeSeconds(DateTime.UtcNow);
    }
}
