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
    }

    public void closeShopButtonAction() {
        shopPopUp.SetActive(false);
    }
}
