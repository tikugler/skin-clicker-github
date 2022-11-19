using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyButton : MonoBehaviour
{
    public int multiplicator = 1;
    public int basePoints = 1; 
   
    void Start() 
    {
        multiplicator = 1;
        basePoints = 1;
    }

    public void MainButtonAction() 
    {
        Account.credits += basePoints * multiplicator;
    }

    public int GetCredits() 
    {
        return Account.credits;
    }

    public void SetCredits(int value) 
    {
        Account.credits = value;

    }

    public void SetMultiplicator(int multi) 
    {
        multiplicator = multi;
    }

    public void IncreaseCreditBy(int value)
    {
        Account.credits += value;
    }
}
