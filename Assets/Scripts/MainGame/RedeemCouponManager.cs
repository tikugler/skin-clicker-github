using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RedeemCouponManager : MonoBehaviour
{

    [SerializeField] Button redeemCouponToggleButton;
    [SerializeField] GameObject redeemCouponPanel;
    [SerializeField] Button redeemButtonButton;
    [SerializeField] TMP_InputField redeemCouponInputField;
    [SerializeField] TextMeshProUGUI redeemCouponInfoText;
    private static string usedCouponCode;



    // Start is called before the first frame update
    // adds listeners
    private void OnEnable()
    {
        redeemCouponToggleButton.onClick.AddListener(ToggleRedeemPanel);
        redeemButtonButton.onClick.AddListener(CallRedeemCatalogItem);
    }

    // removes listeners
    private void OnDisable()
    {
        redeemCouponToggleButton.onClick.RemoveListener(ToggleRedeemPanel);
        redeemButtonButton.onClick.RemoveListener(CallRedeemCatalogItem);
    }

    // toggles redeem coupon panel
    // cleans user inputs, if it is closed
    public void ToggleRedeemPanel()
    {
        redeemCouponPanel.SetActive(!redeemCouponPanel.activeSelf);
        if (!redeemCouponPanel.activeSelf)
        {
            redeemCouponInfoText.text = "";
            redeemCouponInputField.text = "";
        }
    }


    /// <summary>
    /// uses to redeem a coupon
    /// if the given coupon is already used or user typed nothing,
    /// then user will be informed with info text
    /// Otherwise, coupon is gonna be looked at Catalog
    /// </summary>
    public void CallRedeemCatalogItem()
    {
        usedCouponCode = redeemCouponInputField.text;
        if (string.IsNullOrEmpty(usedCouponCode))
        {
            redeemCouponInfoText.color = Color.red;

            redeemCouponInfoText.text = "Geben Sie ein Code ein!";
        }
        else if (Account.UsedCoupons.Contains(usedCouponCode))
        {
            redeemCouponInfoText.color = Color.red;

            redeemCouponInfoText.text = "Dieses Coupon ist schon eingelöst";
        }
        else
        {
            redeemButtonButton.interactable = false;
            redeemCouponInfoText.text = "";
            GetCatalogItem();
        } 
    }

    /// <summary>
    /// all coupons are in CouponCatalog.
    /// And each item in the catalog is consider to be an coupon
    /// All items are called in CouponCatalog
    /// </summary>
    private void GetCatalogItem()
    {
        var request = new GetCatalogItemsRequest();
        request.CatalogVersion = "CouponCatalog";
        PlayFabClientAPI.GetCatalogItems(request, GetCataLogItemSuccess, GetCataLogItemError);
    }

    /// <summary>
    /// called is Catalog cannot be accessed.
    /// This can happen because of internet problems or the catalog might not exist
    /// </summary>
    /// <param name="error">contains error details</param>
    private void GetCataLogItemError(PlayFabError error)
    {
        redeemCouponInfoText.text = "Fehler";
        redeemButtonButton.interactable = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result">containes items in the catalog and other information</param>
    private void GetCataLogItemSuccess(GetCatalogItemsResult result)
    {
        foreach (var item in result.Catalog)
        {
            if (item.ItemId.Equals(usedCouponCode))
            {
                UseCouponReward(JObject.Parse(item.CustomData), item.Description);
                return;
            }
        }
        redeemCouponInfoText.color = Color.red;
        redeemCouponInfoText.text = "Dieses Coupon ist nicht gültig";
        redeemButtonButton.interactable = true;
    }

    private void UseCouponReward(JObject keyValuePairs, string description)
    {
        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        string addedCurrenciesText = "";
        List<string> statisticsToSearch = new List<string> { "Credits", "RealMoney" };
        foreach (string statName in statisticsToSearch)
        {
            if (keyValuePairs.ContainsKey(statName))
            {
                var isNumeric = int.TryParse(keyValuePairs[statName].ToString(), out int value);

                if (isNumeric)
                {
                    addedCurrenciesText += keyValuePairs[statName] + " " + statName + ", ";
                    if (statName == "Credits")
                    {
                        Account.credits += value;
                        request.Statistics.Add(new StatisticUpdate { StatisticName = statName, Value = Account.credits });

                    }
                    else if (statName == "RealMoney")
                    {
                        Account.realMoney += value;
                        request.Statistics.Add(new StatisticUpdate { StatisticName = statName, Value = Account.realMoney });

                    }
                }
            }
        }

        addedCurrenciesText = addedCurrenciesText.TrimEnd(' ').TrimEnd(',');
        request.Statistics.Add(new StatisticUpdate { StatisticName = "USED_" + usedCouponCode, Value = 1 });
        redeemButtonButton.interactable = true;

        if (request.Statistics.Count > 1)
        {
            Account.UsedCoupons.Add(usedCouponCode);
            PlayFabClientAPI.UpdatePlayerStatistics(request,
                success =>
                {
                    redeemCouponInfoText.color = Color.magenta;

                    addedCurrenciesText += " wurden geladen.";

                    if (!string.IsNullOrEmpty(description))
                    {
                        addedCurrenciesText += "\n" + description;
                    }
                    redeemCouponInfoText.text = addedCurrenciesText;

                },

                error => {
                    redeemCouponInfoText.color = Color.red;

                    redeemCouponInfoText.text = error.GenerateErrorReport();
                }
                );
        }
        else
        {
            redeemCouponInfoText.color = Color.red;
            redeemCouponInfoText.text = "Coupon ist ungültig";
        }
    }

}
