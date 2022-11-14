using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyButton : MonoBehaviour
{
    public int multiplicator = 1;
    public int basePoints = 1; 
    public int credits = 0;
   
    void start() 
    {
        multiplicator = 1;
        basePoints = 1;
    }

    public void MainButtonAction() 
    {
        credits += basePoints * multiplicator;
    }

    public int GetCredits() 
    {
        return credits;
    }

    public void SetCredits(int value) 
    {
        credits = value;
    }

    public void SetMultiplicator(int multi) 
    {
        multiplicator = multi;
    }

    public void IncreaseCreditBy(int value)
    {
        credits += value;
    }
}
