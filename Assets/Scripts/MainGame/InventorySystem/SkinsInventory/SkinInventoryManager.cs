using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinInventoryManager : MonoBehaviour
{
    public Button[] useButtons;
    public GameObject[] inventoryPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] inventoryPanels; //Reference to scripts

    //Dummy
    public ArrayList skinsInInventory = new ArrayList();

    void Start()
    {
        RefreshPanels();
    }

    //Refreshes panels --> new values are displayed.
    public void RefreshPanels()
    {
        skinsInInventory = ContentDistributor.contentDistributor.boughtSkinsOfPlayer;
        //Goes through every shop item in the array and refreshes title, description, icon and price.
        for (int i = 0; i < skinsInInventory.Count; i++)
        {
            inventoryPanelsGO[i].SetActive(true);
            SkinEffect item = (SkinEffect)skinsInInventory[i];
            //inventoryPanels[i].shopItemAmount.text = item.shopItem.amount.ToString();
            inventoryPanels[i].shopItemIcon = item.skinTemplate.icon;
            inventoryPanels[i].shopItemDescription.text = item.skinTemplate.description;
            inventoryPanels[i].shopItemDescription.text = "Random Shit";
            inventoryPanels[i].shopItemTitle.text = item.skinTemplate.title;
            inventoryPanels[i].rarity = item.skinTemplate.rarity;
        }
    }

    public void UseButtonAction(int pos)
    {
        //only woking, if there are no stacks!
        SkinEffect item = (SkinEffect)skinsInInventory[pos];
        item.EffectOfSkin();
        //inventoryPanelsGO[ContentDistributor.contentDistributor.boughtSkinsOfPlayer.Count - 1].   SetFancyEffectToSeeThatSkinIsActive()
        RefreshPanels();
    }
}
