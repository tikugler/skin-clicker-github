using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Button[] useButtons;
    public GameObject[] inventoryPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] inventoryPanels; //Reference to scripts
    public ArrayList items = new ArrayList(ShopManager.effects);

    //Dummy
    public ArrayList itemsInInventory = new ArrayList();

    void Start() 
    {
        itemsInInventory = ShopManager.boughtItems;
        RefreshPanels();
    }

    //Refreshes panels --> new values are displayed.
    public void RefreshPanels()
    {
        itemsInInventory = ShopManager.boughtItems;
        //Goes through every shop item in the array and refreshes title, description, icon and price.
        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            inventoryPanelsGO[i].SetActive(true);
            ItemEffect item = (ItemEffect) itemsInInventory[i];
            inventoryPanels[i].shopItemAmount.text = item.shopItem.amount.ToString();
            inventoryPanels[i].itemIcon = item.shopItem.icon;
            inventoryPanels[i].shopItemDescription.text = item.shopItem.description;
            inventoryPanels[i].shopItemTitle.text = item.shopItem.title;
        }
    }

        public void UseButtonAction(int pos)
    {
        //only woking, if there are no stacks!
        ItemEffect item = (ItemEffect) itemsInInventory[pos];
        item.EffectOfItem();
        inventoryPanelsGO[ShopManager.boughtItems.Count - 1].SetActive(false);
        ShopManager.boughtItems.Remove(item);
        RefreshPanels();
    }


}
