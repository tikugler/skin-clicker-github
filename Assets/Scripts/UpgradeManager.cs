using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Text coinsCount;
    public double coins;
    public Text coinsPerSecText;
    public double coinsPerSec;
    public double coinsClickValue;

    public Text workerUpgradeText;
    public double workerUpgradeCost;
    public int workerAmount;

    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        workerUpgradeCost = 10;
        coinsClickValue = 1;

    }

    // Update is called once per frame
    void Update()
    {

        coinsPerSec = workerAmount * 5;
        coinsCount.text = "Münzen: " + coins.ToString("F0");
        coinsPerSecText.text = coinsPerSec.ToString("F0") + " Münzen/s";
        workerUpgradeText.text = "Kosten: " + workerUpgradeCost.ToString("F0") + " Münzen\nAnzahl: " + workerAmount;
        coins += coinsPerSec  * Time.deltaTime;
    }

    public void ClickButton() {

        coins += coinsClickValue;
    }

    public void BuyWorkerUpgrade() {

        if (coins >= workerUpgradeCost)
        {
            workerAmount++;
            coins -= workerUpgradeCost;
            workerUpgradeCost *= 1.3;
        } 
    }
        
        
        
}
