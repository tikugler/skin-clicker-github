using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedButtonWorkers : MonoBehaviour
{
    public int Level1WorkerCount;
    private int level1WorkerScorePerSec;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("UpdateScorePerSecondByWorkers");
    }

    public void SetLevel1WorkerCount(int workerCount)
    {
        Level1WorkerCount = workerCount;
        level1WorkerScorePerSec = Level1WorkerCount;
    }


    IEnumerator UpdateScorePerSecondByWorkers()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ContentDistributor.contentDistributor.mainButton.WorkerAction(level1WorkerScorePerSec);
            ContentDistributor.contentDistributor.shopManager.RefreshPanels();
            ContentDistributor.contentDistributor.shopSkinManager.RefreshPanels();
        }
    }
}
