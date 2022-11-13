using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPopUp : MonoBehaviour
{
    public GameObject inventoryPopUp;
    public GameObject openinventoryButton;
    public GameObject closeinventoryButton;

    //Dummy
    public InventoryManager inventoryManager;
 
    public void ShopButtonAction() {
        inventoryManager.RefreshPanels();

        inventoryPopUp.SetActive(true);
        //openinventoryButton.SetActive(false);
        closeinventoryButton.SetActive(true);
    }

    public void CloseShopButtonAction() {
        inventoryPopUp.SetActive(false);
        //openinventoryButton.SetActive(true);
        closeinventoryButton.SetActive(false);
    }
}
