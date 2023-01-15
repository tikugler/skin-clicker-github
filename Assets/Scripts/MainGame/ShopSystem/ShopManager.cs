using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ShopManager of items.
/// Shows every item that has been added to ContentDistributor's scriptableObjectItems.
/// </summary>
public class ShopManager : MonoBehaviour
{
    public Text creditUIText;
    public Text creditRealMoneyUIText;
    public GameObject[] shopPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] shopPanels; //Reference to scripts
    public Button[] purchaseButtons;
    private ContentDistributor contentDistributor;
    [SerializeField] SoundSceneManager soundManager;

    void Start()
    {
        RefreshPanels();
    }

    /// <summary>
    /// If change "$ " + also change tests.
    /// </summary>
    private void RefreshCredits()
    {
        creditUIText.text = "$ " + Account.credits.ToString();
        creditRealMoneyUIText.text = Account.realMoney.ToString();
    }

    /// <summary>
    /// Set just as many panels active/visible as needed.
    /// Refreshs credits and also checks if item is buyable.
    /// </summary>
    public void RefreshPanels()
    {
        //contentDistributor has to be here otherwise nullpointer becaus Start() isn't working before this methode call.
        contentDistributor = ContentDistributor.contentDistributor;

        RefreshCredits();

        //Sets panels unused panels inactive and starts with the last panel in the list.
        int indexShopPanels = shopPanelsGO.Length - 1;
        for (int i = 0; i < (shopPanelsGO.Length - contentDistributor.scriptableObjectItems.Length); i++)
        {
            shopPanelsGO[indexShopPanels - i].SetActive(false);
            //Debug.Log("Index : " + (indexShopPanels - i) + "/" + (shopPanelsGO.Length -1));
        }

        //Goes through every shop item in the array and refreshes title, description, icon and price.
        for (int i = 0; i < contentDistributor.scriptableObjectItems.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
            shopPanels[i].shopItemTitle.text = contentDistributor.scriptableObjectItems[i].title;
            shopPanels[i].shopItemDescription.text = contentDistributor.scriptableObjectItems[i].description;
            shopPanels[i].shopItemPrice.text = "$ " + contentDistributor.scriptableObjectItems[i].price.ToString();
            shopPanels[i].shopItemAmount.text = contentDistributor.scriptableObjectItems[i].amount.ToString();
            shopPanels[i].shopItemIcon = contentDistributor.scriptableObjectItems[i].icon;
            shopPanels[i].rarity.text = contentDistributor.scriptableObjectItems[i].rarity;

            //If icon != null, show icon of item
            if (shopPanels[i].shopItemIcon != null)
            {
                GameObject test = FindObjectHelper.FindObjectInParent(shopPanelsGO[i], "Image");
                test.GetComponent<Image>().sprite = shopPanels[i].shopItemIcon;
            }

            //Show rarity of item and also change backgound of PreFab
            string rarity = contentDistributor.scriptableObjectItems[i].rarity;
            if (rarity != null)
            {
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
        }
        CheckPurchaseable();
    }

    /// <summary>
    /// Checks for credits >= price of item, if true --> button is clickable.
    /// </summary>
    private void CheckPurchaseable()
    {
        for (int i = 0; i < contentDistributor.scriptableObjectItems.Length; i++)
        {
            if (Account.credits >= contentDistributor.scriptableObjectItems[i].price)
            {
                purchaseButtons[i].interactable = true;
                //mb some effects like backlighting for an active button
            }
            else
            {
                purchaseButtons[i].interactable = false;
            }
        }
    }

    /// <summary>
    /// Action of purchase button of the UI.
    /// Calculates new balance/credits and applys effect of the item.
    /// int pos is position of the button, which is hardcoded in the UI:  
    ///         Scene: MainGame-->Canvas-->ShopPanel-->ScrollArea-->Items-->ShopItemTemplate-->PurchaseButton
    /// </summary>
    /// <param name="pos"></param>
    public void PurchaseButtonAction(int pos)
    {
        //If credit >= as price of shopItem on position pos in array.
        if (Account.credits >= contentDistributor.scriptableObjectItems[pos].price)
        {
            //Check, if key (Effect) is in the list.
            ItemTemplate item = contentDistributor.scriptableObjectItems[pos];
            if (contentDistributor.itemsDictionary.ContainsKey(item.id))
            {
                soundManager.PlayPayWithCoinsSound();
                Account.credits -= item.price;         
                //Search for effect id in array with effects that has same id as id of ShopItem.
                contentDistributor.itemsDictionary[item.id].PurchaseButtonAction(item);
                contentDistributor.itemsDictionary[item.id].shopItem = item;
                // update amount of selected item in Account (updated in PlayFab automatically)
                Account.SetPurchasedItemCount(item.id, contentDistributor.itemsDictionary[item.id].shopItem.amount);
            }
            RefreshPanels();
        }
    }
}
