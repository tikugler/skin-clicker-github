using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller of inventory, switch between skins and items.
/// </summary>
public class InventorySwitch : MonoBehaviour
{
    public GameObject skinsInventory;
    public GameObject itemsIntentory;
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
        itemsIntentory.SetActive(false);
        skinsInventory.SetActive(true);
        itemsButton.GetComponent<Image>().color = Color.white;
        skinsButton.GetComponent<Image>().color = Color.red;
    }

    /// <summary>
    /// Show items in inventory
    /// </summary>
    public void ItemsButtonAction()
    {
        skinsInventory.SetActive(false);
        itemsIntentory.SetActive(true);
        skinsButton.GetComponent<Image>().color = Color.white;
        itemsButton.GetComponent<Image>().color = Color.red;
    }
}
