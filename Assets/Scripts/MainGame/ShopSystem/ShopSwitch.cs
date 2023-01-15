using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller of shop, switch between skins and items.
/// </summary>
public class ShopSwitch : MonoBehaviour
{
    public GameObject skinsShop;
    public GameObject itemsShop;
    public GameObject skinsButton;
    public GameObject itemsButton;

    void Start()
    {
        ItemsButtonAction();
    }

    /// <summary>
    /// Show skins in inventory.
    /// </summary>
    public void SkinsButtonAction()
    {
        ContentDistributor.contentDistributor.shopSkinManager.RefreshPanels();
        itemsShop.SetActive(false);
        skinsShop.SetActive(true);
        itemsButton.GetComponent<Image>().color = Color.white;
        skinsButton.GetComponent<Image>().color = Color.red;
    }

    /// <summary>
    /// Show items in inventory
    /// </summary>
    public void ItemsButtonAction()
    {
        ContentDistributor.contentDistributor.shopManager.RefreshPanels();
        skinsShop.SetActive(false);
        itemsShop.SetActive(true);
        skinsButton.GetComponent<Image>().color = Color.white;
        itemsButton.GetComponent<Image>().color = Color.red;
    }
}
