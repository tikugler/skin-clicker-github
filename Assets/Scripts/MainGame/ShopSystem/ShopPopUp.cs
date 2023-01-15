using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller of shop panel/popup
/// </summary>
public class ShopPopUp : MonoBehaviour
{
    public GameObject shopPopUp;
    public GameObject openShopButton;
    public GameObject closeShopButton;
    public ShopManager shopManager;

    /// <summary>
    /// Action of ShopButton in MainGame --> opens shop panel.
    /// </summary>
    public void ShopButtonAction() {
        shopManager.RefreshPanels();
        ContentDistributor.contentDistributor.shopSkinManager.RefreshPanels();
        shopPopUp.SetActive(true);
        closeShopButton.SetActive(true);
    }

    /// <summary>
    /// Action of CloseButton inside of the shop panel --> closes shop panel.
    /// </summary>
    public void CloseShopButtonAction() {
        shopPopUp.SetActive(false);
        openShopButton.SetActive(true);
        closeShopButton.SetActive(false);
    }
}
