using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryManager : MonoBehaviour
{
    public Button[] useButtons;
    public GameObject[] inventoryPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] inventoryPanels; //Reference to scripts
    public ContentDistributor contentDistributor;

    //Dummy
    public ArrayList itemsInInventory = new ArrayList();

    void Start()
    {
        RefreshPanels();
    }

    //Refreshes panels --> new values are displayed.
    public void RefreshPanels()
    {
        contentDistributor = ContentDistributor.contentDistributor;
        //Sets panels unused panels inactive and starts with the last panel in the list.
        int indexInventoryPanels = inventoryPanelsGO.Length - 1;
        for (int i = 0; i < (inventoryPanelsGO.Length - Account.skinList.Capacity); i++)
        {
            inventoryPanelsGO[indexInventoryPanels - i].SetActive(false);
            //Debug.Log("Index : " + (indexShopPanels - i) + "/" + (shopPanelsGO.Length -1));
        }

        itemsInInventory = ContentDistributor.contentDistributor.boughtItemsOfPlayer;
        //Goes through every shop item in the array and refreshes title, description, icon and price.
        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            inventoryPanelsGO[i].SetActive(true);
            ItemEffect item = (ItemEffect)itemsInInventory[i];
            inventoryPanels[i].shopItemAmount.text = item.shopItem.amount.ToString();
            inventoryPanels[i].shopItemIcon = item.shopItem.icon;
            inventoryPanels[i].shopItemDescription.text = item.shopItem.description;
            inventoryPanels[i].shopItemTitle.text = item.shopItem.title;

            if (inventoryPanels[i].shopItemIcon != null)
            {
                GameObject test = FindObjectHelper.FindObjectInParent(inventoryPanelsGO[i], "Image");
                test.GetComponentInChildren<Image>().sprite = inventoryPanels[i].shopItemIcon;
            }
        }
    }

    /*
    *   Action for the use button in the inventory.
    *   pos is a hardcoded param in unity --> InventoryPanel --> Items --> (...) --> InventoryItem --> (...) --> UseButton
    */
    public void UseButtonAction(int pos)
    {
        //only woking, if there are no stacks!
        ItemEffect item = (ItemEffect)itemsInInventory[pos];
        item.EffectOfItem();
        inventoryPanelsGO[ContentDistributor.contentDistributor.boughtItemsOfPlayer.Count - 1].SetActive(false);
        ContentDistributor.contentDistributor.boughtItemsOfPlayer.Remove(item);
        Account.SetPurchasedItemCount(item.id, contentDistributor.itemsDictionary[item.id].shopItem.amount);
        RefreshPanels();
    }


}
