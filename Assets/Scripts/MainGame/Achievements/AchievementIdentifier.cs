using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is an alternative to enum, since enum didnt work properly in unity.
/// Every Achievement has 3 Identifiers.
/// </summary>
public static class AchievementIdentifier
{
    public static readonly string Achieve10Points = "Sammle 10 Punkte";
    public static readonly string Achieve10PointsDes = "Du hast 10 Punkte gesammelt!";
    public static readonly int Achieve10PointsBonus = 50;

    public static readonly string Achieve500Points = "Sammle 500 Punkte";
    public static readonly string Achieve500PointsDes = "Du hast 500 Punkte gesammelt!";
    public static readonly int Achieve500PointsBonus = 2000;

    public static readonly string Achieve5000Points = "Sammle 5000 Punkte";
    public static readonly string Achieve5000PointsDes = "Du hast 5000 Punkte gesammelt!";
    public static readonly int Achieve5000PointsBonus = 15000;

    public static readonly string Achieve50000Points = "Sammle 50000 Punkte";
    public static readonly string Achieve50000PointsDes = "Du hast 50000 Punkte gesammelt!";
    public static readonly int Achieve50000PointsBonus = 30000;

    public static readonly string Achieve500000Points = "Sammle 500000 Punkte";
    public static readonly string Achieve500000PointsDes = "Du hast 500000 Punkte gesammelt!";
    public static readonly int Achieve500000PointsBonus = 100000;

    public static readonly string Achieve1000000Points = "Sammle 1000000 Punkte";
    public static readonly string Achieve1000000PointsDes = "Du hast 1000000 Punkte gesammelt!";
    public static readonly int Achieve1000000PointsBonus = 250000;

    public static readonly string Achieve5000000Points = "Sammle 5000000 Punkte";
    public static readonly string Achieve5000000PointsDes = "Du hast 5000000 Punkte gesammelt!";
    public static readonly int Achieve5000000PointsBonus = 1500000;

    public static readonly string Achieve10000000Points = "Sammle 10000000 Punkte";
    public static readonly string Achieve10000000PointsDes = "Du hast 10000000 Punkte gesammelt!";
    public static readonly int Achieve10000000PointsBonus = 3500000;

    /// <summary>
    /// This Method applies the Bonus of the Achievement.
    /// </summary>
    /// <param name="achievement"></param>
    public static void GetAchievementBonus (string achievement)
    {
        if (achievement.Equals(Achieve10Points))
        {
            Account.credits += Achieve10PointsBonus;
        }
        else if (achievement.Equals(Achieve500Points))
        {
            Account.credits += Achieve500PointsBonus;
        }
        else if (achievement.Equals(Achieve5000Points))
        {
            Account.credits += Achieve5000PointsBonus;
        }
        else if (achievement.Equals(Achieve50000Points))
        {
            Account.credits += Achieve50000PointsBonus;
        }
        else if (achievement.Equals(Achieve500000Points))
        {
            Account.credits += Achieve500000PointsBonus;
        }
        else if (achievement.Equals(Achieve1000000Points))
        {
            Account.credits += Achieve1000000PointsBonus;
        }
        else if (achievement.Equals(Achieve5000000Points))
        {
            Account.credits += Achieve5000000PointsBonus;
        }
        else if (achievement.Equals(Achieve10000000Points))
        {
            Account.credits += Achieve10000000PointsBonus;
        }
        else
        {
            return;
        }
        return;
    }

    /// <summary>
    /// This Method declares the Bonus Text of every Achievement.
    /// </summary>
    /// <param name="achievement"></param>
    /// <returns></returns>
    public static string GetAchievementBonusText (string achievement)
    {
        if (achievement.Equals(Achieve10Points))
        {
            return Achieve10PointsBonus.ToString() + " \nBonus Punkte!";
        }
        else if (achievement.Equals(Achieve500Points))
        {
            return Achieve500PointsBonus.ToString() + " Bonus Punkte!";
        }
        else if (achievement.Equals(Achieve5000Points))
        {
            return Achieve5000PointsBonus.ToString() + " Bonus Punkte!";
        }
        else if (achievement.Equals(Achieve50000Points))
        {
            return Achieve50000PointsBonus.ToString() + " Bonus Punkte!";
        }
        else if (achievement.Equals(Achieve500000Points))
        {
            return Achieve500000PointsBonus.ToString() + " Bonus Punkte!";
        }
        else if (achievement.Equals(Achieve1000000Points))
        {
            return Achieve1000000PointsBonus.ToString() + " Bonus Punkte!";
        }
        else if (achievement.Equals(Achieve5000000Points))
        {
            return Achieve5000000PointsBonus.ToString() + " Bonus Punkte!";
        }
        else if (achievement.Equals(Achieve10000000Points))
        {
            return Achieve10000000PointsBonus.ToString() + " Bonus Punkte!";
        }
        else
        {
            return "";
        }
    }
}
