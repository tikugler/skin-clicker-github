using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AchievementIdentifier
{
    public static readonly string Achieve10Points = "Earn 10 Points !";
    public static readonly string Achieve10PointsDes = "You earned 10 Points !";
    public static readonly int Achieve10PointsBonus = 50;

    public static readonly string Achieve500Points = "Earn 500 Points !";
    public static readonly string Achieve500PointsDes = "You earned 500 Points !";
    public static readonly int Achieve500PointsBonus = 2000;

    public static readonly string Achieve5000Points = "Earn 5000 Points !";
    public static readonly string Achieve5000PointsDes = "You earned 5000 Points !";
    public static readonly int Achieve5000PointsBonus = 15000;

    public static readonly string Achieve50000Points = "Earn 50000 Points !";
    public static readonly string Achieve50000PointsDes = "You earned 50000 Points !";
    public static readonly int Achieve50000PointsBonus = 30000;

    public static readonly string Achieve500000Points = "Earn 500000 Points !";
    public static readonly string Achieve500000PointsDes = "You earned 500000 Points !";
    public static readonly int Achieve500000PointsBonus = 100000;

    public static readonly string Achieve1000000Points = "Earn 1000000 Points !";
    public static readonly string Achieve1000000PointsDes = "You earned 1000000 Points !";
    public static readonly int Achieve1000000PointsBonus = 250000;

    public static readonly string Achieve5000000Points = "Earn 5000000 Points !";
    public static readonly string Achieve5000000PointsDes = "You earned 5000000 Points !";
    public static readonly int Achieve5000000PointsBonus = 1500000;

    public static readonly string Achieve10000000Points = "Earn 10000000 Points !";
    public static readonly string Achieve10000000PointsDes = "You earned 10000000 Points !";
    public static readonly int Achieve10000000PointsBonus = 3500000;

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

    public static string GetAchievementBonusText (string achievement)
    {
        if (achievement.Equals(Achieve10Points))
        {
            return  Achieve10PointsBonus.ToString() + " \nBonus Points!";
        }
        else if (achievement.Equals(Achieve500Points))
        {
            return Achieve500PointsBonus.ToString() + " Bonus Points!";
        }
        else if (achievement.Equals(Achieve5000Points))
        {
            return Achieve5000PointsBonus.ToString() + " Bonus Points!";
        }
        else if (achievement.Equals(Achieve50000Points))
        {
            return Achieve50000PointsBonus.ToString() + " Bonus Points!";
        }
        else if (achievement.Equals(Achieve500000Points))
        {
            return Achieve500000PointsBonus.ToString() + " Bonus Points!";
        }
        else if (achievement.Equals(Achieve1000000Points))
        {
            return Achieve1000000PointsBonus.ToString() + " Bonus Points!";
        }
        else if (achievement.Equals(Achieve5000000Points))
        {
            return Achieve5000000PointsBonus.ToString() + " Bonus Points!";
        }
        else if (achievement.Equals(Achieve10000000Points))
        {
            return Achieve10000000PointsBonus.ToString() + " Bonus Points!";
        }
        else
        {
            return "";
        }
    }
}
