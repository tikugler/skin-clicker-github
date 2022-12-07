using UnityEngine;

public class DummyButton : MonoBehaviour
{
    public int multiplicator = 1;
    public int basePoints = 1;
    public int skinMultiplicator = 1;
    public float criticalMultiplicator = 1;
    public float criticalChance = 0.1f;


    public void MainButtonAction()
    {
        float randValue = Random.value;
        Debug.Log("Rand Value: " + randValue);
        Debug.Log("Chance: " + (1.0f - criticalChance));
        if (randValue > (1.0f - criticalChance))
        {
            Account.credits += (int)System.Math.Round(basePoints * multiplicator * skinMultiplicator * criticalMultiplicator);
            Debug.Log("Hit Crit");
        }
        else
        {
            Account.credits += basePoints * multiplicator * skinMultiplicator;
        }

    }

    public void WorkerAction(int worker)
    {
        Account.credits += basePoints * multiplicator * worker;
    }


    public void SetMultiplicator(int multi)
    {
        multiplicator = multi;
    }
}
