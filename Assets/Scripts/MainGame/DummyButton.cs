using UnityEngine;

public class DummyButton : MonoBehaviour
{
    public int multiplicator = 1;
    public int basePoints = 1; 
   

    public void MainButtonAction() 
    {
        Account.credits += basePoints * multiplicator;
    }


    public void SetMultiplicator(int multi) 
    {
        multiplicator = multi;
    }
}
