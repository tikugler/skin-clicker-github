using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static int credit;
    public Text creditUIText;
    public GameObject[] shopPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] shopPanels; //Reference to scripts
    public Button[] purchaseButtons;
    private ContentDistributor contentDistributor;

    //Set as many panels active/visible as needed.
    //Copy scriptableObjectItems Inhalte in neue ItemTemplates? --> Abkoppelung der erstellten SO-Items und neue Items kann man ebenfalls, wie gewünscht bearbeiten.
    void Start()
    {
        RefreshPanels();
    }

    //If change "$ " + also change tests.
    private void RefreshCredits()
    {
        credit = Account.credits;
        creditUIText.text = "$ " + credit.ToString();
    }

    /* 
    *  New items in the shop can't be added after Start()
    */
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
            //Debug.Log("Item Name:" + contentDistributor.scriptableObjectItems[i].id + " || Item Price: " + contentDistributor.scriptableObjectItems[i].price.ToString());
            shopPanels[i].shopItemAmount.text = contentDistributor.scriptableObjectItems[i].amount.ToString();
            shopPanels[i].shopItemIcon = contentDistributor.scriptableObjectItems[i].icon;

            if (shopPanels[i].shopItemIcon != null)
            {
                GameObject test = FindObjectHelper.FindObjectInParent(shopPanelsGO[i], "Image");
                test.GetComponent<Image>().sprite = shopPanels[i].shopItemIcon;
            }
        }
        CheckPurchaseable();
    }

    //Checks for credits >= price of item, if true --> button is clickable.
    private void CheckPurchaseable()
    {
        for (int i = 0; i < contentDistributor.scriptableObjectItems.Length; i++)
        {
            if (credit >= contentDistributor.scriptableObjectItems[i].price)
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
    /*
    *   Action of purchase button of the UI.
    *   Calculates new balance/credits and applys effect of the item.
    *   int pos is position of the button, which is hardcoded in the UI:   
    *           Scene: MainGame-->Canvas-->ShopPanel-->ScrollArea-->Items-->ShopItemTemplate-->PurchaseButton
    */
    public void PurchaseButtonAction(int pos)
    {
        //If credit >= as price of shopItem on position pos in array.
        if (credit >= contentDistributor.scriptableObjectItems[pos].price)
        {
            //Check, if key (Effect) is in the list.
            ItemTemplate item = contentDistributor.scriptableObjectItems[pos];
            if (contentDistributor.itemsDictionary.ContainsKey(item.id))
            {
                credit -= item.price;
                Account.credits = credit;
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
