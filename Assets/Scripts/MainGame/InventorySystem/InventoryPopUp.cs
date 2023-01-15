using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller of inventory panel/popup
/// </summary>
public class InventoryPopUp : MonoBehaviour
{
    public GameObject inventoryPopUp;
    public GameObject openinventoryButton;
    public GameObject closeinventoryButton;
    public ItemInventoryManager inventoryManager;

    /// <summary>
    /// Action of InventoryButton in MainGame --> opens inventory panel.
    /// </summary>
    public void ShopButtonAction() 
    {
        inventoryManager.RefreshPanels();
        ContentDistributor.contentDistributor.skinInventoryManager.RefreshPanels();
        inventoryPopUp.SetActive(true);
        closeinventoryButton.SetActive(true);
    }

    /// <summary>
    /// Action of CloseButton inside of the inventory panel --> closes inventory panel.
    /// </summary>
    public void CloseShopButtonAction() 
    {
        inventoryPopUp.SetActive(false);
        closeinventoryButton.SetActive(false);
    }
}
