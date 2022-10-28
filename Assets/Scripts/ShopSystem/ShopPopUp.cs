using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopUp : MonoBehaviour
{
    public GameObject shopPopUp;
    public GameObject openShopButton;
    public GameObject closeShopButton;

    public void ShopButtonAction() {
        shopPopUp.SetActive(true);
        openShopButton.SetActive(false);
        closeShopButton.SetActive(true);
    }

    public void CloseShopButtonAction() {
        shopPopUp.SetActive(false);
        openShopButton.SetActive(true);
        closeShopButton.SetActive(false);
    }
}
