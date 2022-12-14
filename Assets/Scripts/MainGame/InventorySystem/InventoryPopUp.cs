using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPopUp : MonoBehaviour
{
    public GameObject inventoryPopUp;
    public GameObject openinventoryButton;
    public GameObject closeinventoryButton;

    //Dummy
    public ItemInventoryManager inventoryManager;
 
    public void ShopButtonAction() 
    {
        inventoryManager.RefreshPanels();
        ContentDistributor.contentDistributor.skinInventoryManager.RefreshPanels();
        inventoryPopUp.SetActive(true);
        closeinventoryButton.SetActive(true);
    }

    public void CloseShopButtonAction() 
    {
        inventoryPopUp.SetActive(false);
        closeinventoryButton.SetActive(false);
    }
}
