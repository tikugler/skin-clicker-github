using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinInventoryManager : MonoBehaviour
{
    public Button[] useButtons;
    public GameObject[] inventoryPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] inventoryPanels; //Reference to scripts
    public ContentDistributor contentDistributor;


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
        for (int i = 0; i < (inventoryPanelsGO.Length - Account.skinList.Count); i++)
        {
            inventoryPanelsGO[indexInventoryPanels - i].SetActive(false);
            //Debug.Log("Index : " + (indexShopPanels - i) + "/" + (shopPanelsGO.Length -1));
        }

        //Goes through every shop item in the array and refreshes title, description, icon and price.
        for (int i = 0; i < Account.skinList.Count; i++)
        {
            inventoryPanelsGO[i].SetActive(true);
            SkinEffect item = (SkinEffect)Account.skinList[i];
            //inventoryPanels[i].shopItemAmount.text = item.shopItem.amount.ToString();
            inventoryPanels[i].shopItemDescription.text = item.skinTemplate.description;
            inventoryPanels[i].shopItemTitle.text = item.skinTemplate.title;
            inventoryPanels[i].rarity.text = item.skinTemplate.rarity;
            inventoryPanels[i].shopItemIcon = item.skinTemplate.icon;
            useButtons[i].GetComponentInChildren<Text>().text = "Use";

            if (inventoryPanels[i].shopItemIcon != null)
            {
                GameObject test = FindObjectHelper.FindObjectInParent(inventoryPanelsGO[i], "Image");
                test.GetComponent<Image>().sprite = inventoryPanels[i].shopItemIcon;
            }

            string rarity = item.rarity;
            if (rarity.Equals(Rarities.Common))
            {
                inventoryPanels[i].rarity.color = Color.cyan;
                FindObjectHelper.FindObjectInParent(inventoryPanelsGO[i], "Background").GetComponent<Image>().color = Color.cyan;
            }
            else if (rarity.Equals(Rarities.Uncommon))
            {
                inventoryPanels[i].rarity.color = Color.blue;
                FindObjectHelper.FindObjectInParent(inventoryPanelsGO[i], "Background").GetComponent<Image>().color = Color.blue;
            }
            else if (rarity.Equals(Rarities.Rare))
            {
                inventoryPanels[i].rarity.color = Color.magenta;
                FindObjectHelper.FindObjectInParent(inventoryPanelsGO[i], "Background").GetComponent<Image>().color = Color.magenta;
            }
            else if (rarity.Equals(Rarities.Mythical))
            {
                inventoryPanels[i].rarity.color = Color.red;
                FindObjectHelper.FindObjectInParent(inventoryPanelsGO[i], "Background").GetComponent<Image>().color = Color.red;
            }
            else if (rarity.Equals(Rarities.Legendary))
            {
                inventoryPanels[i].rarity.color = Color.yellow;
                FindObjectHelper.FindObjectInParent(inventoryPanelsGO[i], "Background").GetComponent<Image>().color = Color.yellow;
            }
        }
        CheckSkinAlreadyInUse();
    }

    //Checks for credits >= price of item, if true --> button is clickable.
    private void CheckSkinAlreadyInUse()
    {
        for (int i = 0; i < Account.skinList.Count; i++)
        {
            if (Account.ActiveSkin == null)
            {
                useButtons[i].interactable = true;
                //mb some effects like backlighting for an active button
            }
            else if (!Account.skinList[i].id.Equals(Account.ActiveSkin.id))
            {
                useButtons[i].interactable = true;
            }
            else
            {
                useButtons[i].interactable = false;
                useButtons[i].GetComponentInChildren<Text>().text = "In use";
            }
        }
    }

    /*
    *   Action for the use button in the skin inventory.
    *   pos is a hardcoded param in unity --> InventoryPanel --> Skins --> (...) --> ShopSkinTemplate --> stats --> PurchaseButton
    */
    public void UseButtonAction(int pos)
    {
        if (Account.ActiveSkin != null) { Account.ActiveSkin.UnequipSkin(); }
        SkinEffect item = (SkinEffect)Account.skinList[pos];
        item.EquipSkin();
        //inventoryPanelsGO[ContentDistributor.contentDistributor.boughtSkinsOfPlayer.Count - 1].   SetFancyEffectToSeeThatSkinIsActive()
        RefreshPanels();
    }
}
