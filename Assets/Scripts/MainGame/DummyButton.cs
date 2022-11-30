using UnityEngine;

public class DummyButton : MonoBehaviour
{
    public int multiplicator = 1;
    public int basePoints = 1;
    private int xClickBonusCounter = 0;
   

    public void MainButtonAction() 
    {
        Account.credits += basePoints * multiplicator;
        xClickBonusCounter++;
        if(xClickBonusCounter == 50)
        {
            Account.credits += 400;
            xClickBonusCounter = 0;
        }
    }


    public void SetMultiplicator(int multi) 
    {
        multiplicator = multi;
    }
}
