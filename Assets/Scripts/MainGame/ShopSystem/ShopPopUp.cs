using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopUp : MonoBehaviour
{
    public GameObject shopPopUp;
    public GameObject openShopButton;
    public GameObject closeShopButton;

    //Dummy
    public ShopManager shopManager;

    public void ShopButtonAction() {
        shopManager.RefreshPanels();
        ContentDistributor.contentDistributor.shopSkinManager.RefreshPanels();
        shopPopUp.SetActive(true);
        closeShopButton.SetActive(true);
    }

    public void CloseShopButtonAction() {
        shopPopUp.SetActive(false);
        openShopButton.SetActive(true);
        closeShopButton.SetActive(false);
    }
}
