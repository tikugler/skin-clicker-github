using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewards : MonoBehaviour
{
    private int daysLoggedIn;
    private DateTime lastReward;
    private int realMoneyReward;
    private int ingameMoneyReward;

    public GameObject dailyRewardPopup;
    public Text dailyText;
    public TextMeshProUGUI realMoneyText;
    public TextMeshProUGUI ingameMoneyText;
    public GameObject backgroundRight;

    // Get Account latest stats on startup
    void Awake()
    {
        daysLoggedIn = Account.daysLoggedInARow;
        lastReward = Account.lastReward;
    }

    void Start()
    {
        var hours = (DateTime.Now - lastReward).TotalHours;
        if (hours >= 48)
        {
            daysLoggedIn = 1;
        }
        if (hours >= 24)
        {
            //Give out reward
            var rewards = GiveoutReward();
            OpenPopUp(rewards);
            dailyText.text = "Anmeldebonus: Tag " + daysLoggedIn;
            daysLoggedIn++;
            // Update Account last Reward time
            // Update Account logged in days in a row
            Account.daysLoggedInARow = daysLoggedIn;
            Account.lastReward = DateTime.Now;
            PlayfabUpdateUserData.SetLastRewardDate();
        }

    }

    public void ClosePopup()
    {
        //Update rewards after closing reward window
        dailyRewardPopup.SetActive(false);
        Account.credits += ingameMoneyReward;
        Account.realMoney += realMoneyReward;
        PlayfabUpdateUserData.SetMoneyAmount();
    }
    private void OpenPopUp(Dictionary<string, int> rewards)
    {
        dailyRewardPopup.SetActive(true);
        PopulateRewards(rewards);
    }

    private void PopulateRewards(Dictionary<string, int> rewards)
    {
        if (rewards["realMoney"] == 0)
        {
            backgroundRight.SetActive(false);
        }
        ingameMoneyText.text = rewards["gameMoney"].ToString();
        realMoneyText.text = rewards["realMoney"].ToString();
    }

    private Dictionary<string, int> GiveoutReward()
    {
        var rewards = new Dictionary<string, int>();

        int baseMoneyReward = 1000;
        int baseRealMoney = 50;
        int realMoneyHandoutInDays = 15;

        int currentReward = baseMoneyReward * daysLoggedIn;

        if (daysLoggedIn % realMoneyHandoutInDays == 0)
        {
            rewards.Add("realMoney", baseRealMoney);
            realMoneyReward = baseRealMoney;
        }
        else
            rewards.Add("realMoney", 0);

        if (daysLoggedIn >= 30)
            currentReward = 30000;

        ingameMoneyReward = currentReward;
        rewards.Add("gameMoney", currentReward);
        return rewards;
    }
}