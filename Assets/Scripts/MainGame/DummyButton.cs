using UnityEngine;

public class DummyButton : MonoBehaviour
{
    public int multiplicator = 1;
    public int basePoints = 1;
    public int multiplicatorOfSkin = 1;
    public static float criticalMultiplicator = 1;
    public static float criticalChance = 0f;


    public void MainButtonAction()
    {
        float randValue = Random.value;
        if (randValue > (1.0f - criticalChance))
        {
            Account.credits += (int)System.Math.Round(basePoints * multiplicator * multiplicatorOfSkin * criticalMultiplicator);
        }
        else
        {
            Account.credits += basePoints * multiplicator * multiplicatorOfSkin;
        }

    }

    public void WorkerAction(int worker)
    {
        Account.credits += basePoints * multiplicatorOfSkin * worker;
    }


    public void SetMultiplicator(int multi)
    {
        multiplicator = multi;
    }
}
