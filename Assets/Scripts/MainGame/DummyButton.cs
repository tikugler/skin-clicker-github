using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyButton : MonoBehaviour
{
    public static int multiplicator = 1;
    public static int basePoints = 1; //idk warum {get; set;} nicht funktioniert :(
    public static int credits = 0;
   
    void start() {
        multiplicator = 1;
        basePoints = 1;
    }
    public void MainButtonAction() {
        credits += basePoints * multiplicator;
    }

    public int GetCredits() {
        return credits;
    }

    public void SetCredits(int value) {
        credits = value;
    }
}
