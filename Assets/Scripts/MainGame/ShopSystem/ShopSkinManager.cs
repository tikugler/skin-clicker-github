using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ShopManager of skins.
/// Shows every Skin that has been added to ContentDistributor's scriptableObjectSkins.
/// </summary>
public class ShopSkinManager : MonoBehaviour
{
    public static int credit;
    public Text creditUIText;
    public GameObject[] shopPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] shopPanels; //Reference to scripts
    public Button[] purchaseButtons;
    private ContentDistributor contentDistributor;
    [SerializeField] SoundSceneManager soundManager;
    public Text realMoneyUIText;

    bool PayWithRealMoney = false;


    void Start()
    {
        RefreshPanels();
    }

    /// <summary>
    /// If change "$ " + also change tests.
    /// </summary>
    private void RefreshCredits()
    {
        credit = Account.credits;
        creditUIText.text = "$ " + credit.ToString();
    }

    /// <summary>
    /// Set just as many panels active/visible as needed.
    /// Refreshs credits and also checks if skin is buyable.
    /// </summary>
    public void RefreshPanels()
    {
        //contentDistributor has to be here otherwise nullpointer becaus Start() isn't working before this methode call.
        contentDistributor = ContentDistributor.contentDistributor;

        RefreshCredits();

        //Sets panels unused panels inactive and starts with the last panel in the list.
        int indexShopPanels = shopPanelsGO.Length - 1;
        for (int i = 0; i < (shopPanelsGO.Length - contentDistributor.scriptableObjectSkins.Length); i++)
        {
            shopPanelsGO[indexShopPanels - i].SetActive(false);
            //Debug.Log("Index : " + (indexShopPanels - i) + "/" + (shopPanelsGO.Length -1));
        }

        //Goes through every shop skin in the array and refreshes title, description, icon and price.
        for (int i = 0; i < contentDistributor.scriptableObjectSkins.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
            shopPanels[i].shopItemTitle.text = contentDistributor.scriptableObjectSkins[i].title;
            shopPanels[i].shopItemDescription.text = contentDistributor.scriptableObjectSkins[i].description;
            shopPanels[i].shopItemIcon = contentDistributor.scriptableObjectSkins[i].icon;
            shopPanels[i].rarity.text = contentDistributor.scriptableObjectSkins[i].rarity;

            //Shows price of skin, if already bought --> "Out of Stock"
            if (!Account.IsSkinInInventory(contentDistributor.scriptableObjectSkins[i].id))
            {
                if (!PayWithRealMoney)
                    shopPanels[i].shopItemPrice.text = "$ " + contentDistributor.scriptableObjectSkins[i].price.ToString();
                else
                    shopPanels[i].shopItemPrice.text = contentDistributor.scriptableObjectSkins[i].PriceAsRealMoney.ToString() + " RM";
            }
            else
            {
                shopPanels[i].shopItemPrice.text = "Nicht vorr�tig!";
            }

            //If icon != null, show icon of skin
            if (shopPanels[i].shopItemIcon != null)
            {
                FindObjectHelper.FindObjectInParent(shopPanelsGO[i], "Image").GetComponent<Image>().sprite = shopPanels[i].shopItemIcon;
            }

            //Show rarity of skin and also change backgound of PreFab
            string rarity = contentDistributor.scriptableObjectSkins[i].rarity;
            if (rarity.Equals(Rarities.Common))
            {
                shopPanels[i].rarity.color = Color.cyan;
                FindObjectHelper.FindObjectInParent(shopPanelsGO[i], "Background").GetComponent<Image>().color = Color.cyan;
            }
            else if (rarity.Equals(Rarities.Uncommon))
            {
                shopPanels[i].rarity.color = Color.blue;
                FindObjectHelper.FindObjectInParent(shopPanelsGO[i], "Background").GetComponent<Image>().color = Color.blue;
            }
            else if (rarity.Equals(Rarities.Rare))
            {
                shopPanels[i].rarity.color = Color.magenta;
                FindObjectHelper.FindObjectInParent(shopPanelsGO[i], "Background").GetComponent<Image>().color = Color.magenta;
            }
            else if (rarity.Equals(Rarities.Mythical))
            {
                shopPanels[i].rarity.color = Color.red;
                FindObjectHelper.FindObjectInParent(shopPanelsGO[i], "Background").GetComponent<Image>().color = Color.red;
            }
            else if (rarity.Equals(Rarities.Legendary))
            {
                shopPanels[i].rarity.color = Color.yellow;
                FindObjectHelper.FindObjectInParent(shopPanelsGO[i], "Background").GetComponent<Image>().color = Color.yellow;
            }
        }
        CheckPurchaseable();
    }

    /// <summary>
    /// Checks for credits >= price of skin, if true --> button is clickable.
    /// </summary>
    private void CheckPurchaseable()
    {
        for (int i = 0; i < contentDistributor.scriptableObjectSkins.Length; i++)
        {
            if ((!PayWithRealMoney && Account.credits >= contentDistributor.scriptableObjectSkins[i].price) && !Account.IsSkinInInventory(contentDistributor.scriptableObjectSkins[i].id))
            {
                purchaseButtons[i].interactable = true;
            }
            else if ((PayWithRealMoney && Account.realMoney >= contentDistributor.scriptableObjectSkins[i].PriceAsRealMoney) && !Account.IsSkinInInventory(contentDistributor.scriptableObjectSkins[i].id))
            {
                purchaseButtons[i].interactable = true;
            }
            else
            {
                purchaseButtons[i].interactable = false;
            }
        }
    }

    /// <summary>
    /// Action of purchase button of the UI.
    /// Calculates new balance/credits and adds skin to inventory.
    /// int pos is position of the button, which is hardcoded in the UI:  
    ///         Scene: MainGame-->Canvas-->ShopPanel-->Skins-->ScrollArea-->ShopItemTemplate-->PurchaseButton
    /// </summary>
    /// <param name="pos"></param>
    public void PurchaseButtonAction(int pos)
    {
        //If credit >= as price of skin on position pos in array.
        if (credit >= contentDistributor.scriptableObjectSkins[pos].price)
        {
            //Check, if key (skin) is in the list.
            SkinTemplate skin = contentDistributor.scriptableObjectSkins[pos];
            if (contentDistributor.skinsDictionary.ContainsKey(skin.id))
            {
                credit -= skin.price;
                soundManager.PlayPayWithCoinsSound();
                Account.credits = credit;
                //Search for skin in array with skins that has same id as id of skin.
                contentDistributor.skinsDictionary[skin.id].PurchaseButtonAction(skin);
                contentDistributor.skinsDictionary[skin.id].skinTemplate = skin;

                //Adds skin in Account (added in Playfab automatically)
                Account.AddSkin(skin.id);
            }
        }

        // If credit >= as price of shopItem on position pos in array.
        else if (PayWithRealMoney && Account.realMoney >= contentDistributor.scriptableObjectSkins[pos].PriceAsRealMoney)
        {
            //Check, if key (Effect) is in the list.
            SkinTemplate item = contentDistributor.scriptableObjectSkins[pos];
            if (contentDistributor.skinsDictionary.ContainsKey(item.id))
            {
                Account.realMoney -= item.PriceAsRealMoney;
                soundManager.PlayPayWithCoinsSound();
                //Search for effect id in array with effects that has same id as id of ShopItem.
                contentDistributor.skinsDictionary[item.id].PurchaseButtonAction(item);
                contentDistributor.skinsDictionary[item.id].skinTemplate = item;

                // adds skin in Account (added in Playfab automatically)
                Account.AddSkin(item.id);
                realMoneyUIText.text = Account.realMoney.ToString();
            }
        }
        RefreshPanels();
    }


    public void SetPayWithRealMoney(bool isSelected)
    {
        PayWithRealMoney = isSelected;
        RefreshPanels();
    }
}
