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

    private void OnEnable()
    {
        redeemCouponToggleButton.onClick.AddListener(ToggleRedeemPanel);
        redeemButtonButton.onClick.AddListener(CallRedeemCatalogItem);
    }

    private void OnDisable()
    {
        redeemCouponToggleButton.onClick.RemoveListener(ToggleRedeemPanel);
        redeemButtonButton.onClick.RemoveListener(CallRedeemCatalogItem);


    }


    public void ToggleRedeemPanel()
    {
        redeemCouponPanel.SetActive(!redeemCouponPanel.activeSelf);
        if (!redeemCouponPanel.activeSelf)
        {
            redeemCouponInfoText.text = "";
            redeemCouponInputField.text = "";
        }
    }


    public void UseCoupon(string couponCode)
    {
        var primaryCatalogName = "CouponCatalog"; // In your game, this should just be a constant matching your primary catalog
        var request = new RedeemCouponRequest
        {
            CatalogVersion = primaryCatalogName,
            CouponCode = couponCode // This comes from player input, in this case, one of the coupon codes generated above
        };
        PlayFabClientAPI.RedeemCoupon(request, LogSuccess, LogFailure);
    }

    private void LogFailure(PlayFabError obj)
    {
        throw new NotImplementedException();
    }

    private void LogSuccess(RedeemCouponResult obj)
    {
        //throw new NotImplementedException();
        Debug.Log("Successful");
    }



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

    private void GetCatalogItem()
    {
        var request = new GetCatalogItemsRequest();
        request.CatalogVersion = "CouponCatalog";
        PlayFabClientAPI.GetCatalogItems(request, GetCataLogItemSuccess, GetCataLogItemError);

    }

    private void GetCataLogItemError(PlayFabError error)
    {
        redeemCouponInfoText.text = "Fehler";
        redeemButtonButton.interactable = true;
    }

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



    //private void RedeemCatalogItem(string couponCode)
    //{
    //    PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
    //    {
    //        ItemId = couponCode,
    //        VirtualCurrency = "CN",
    //        Price = 00,
    //    }, OnPurchaseItemSuccess, OnPurchaseItemError);
    //}

    //private void OnPurchaseItemSuccess(PurchaseItemResult result)
    //{
    //    Debug.Log(result.Items[0].DisplayName);


    //    Debug.Log(result.Items.Count);

    //    Debug.Log(result.Request);
    //    Debug.Log(result.CustomData);



    //}


    //private void OnPurchaseItemError(PlayFabError error)
    //{

    //    Debug.Log(error.GenerateErrorReport());
    //}


}
