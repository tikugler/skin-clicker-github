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
        Level1WorkerCount = 1;
        StartCoroutine("UpdateScorePerSecondByWorkers");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetLevel1WorkerCount(int workerCount)
    {
        Level1WorkerCount = workerCount;
        UpdateLevel1WorkerScorePerSec();
    }

    private void UpdateLevel1WorkerScorePerSec()
    {
        level1WorkerScorePerSec = Level1WorkerCount;
    }

    IEnumerator UpdateScorePerSecondByWorkers()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Account.credits += level1WorkerScorePerSec;
            ContentDistributor.contentDistributor.shopManager.RefreshPanels();
        }
    }
}
