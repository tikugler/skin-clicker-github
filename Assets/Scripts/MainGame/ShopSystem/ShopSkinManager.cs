using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSkinManager : MonoBehaviour
{
    public static int credit;
    public Text creditUIText;
    public GameObject[] shopPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] shopPanels; //Reference to scripts
    public Button[] purchaseButtons;
    private ContentDistributor contentDistributor;

    //Set as many panels active/visible as needed.
    //Copy scriptableObjectSkins Inhalte in neue ItemTemplates? --> Abkoppelung der erstellten SO-Items und neue Items kann man ebenfalls, wie gewünscht bearbeiten.
    void Start()
    {
        contentDistributor = ContentDistributor.contentDistributor;
        for (int i = 0; i < contentDistributor.scriptableObjectSkins.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }

        RefreshPanels();
    }

    //If change "$ " + also change tests.
    private void RefreshCredits()
    {
        credit = Account.credits;
        creditUIText.text = "$ " + credit.ToString();
    }

    /* 
    *  Refreshes panels --> new values are displayed.
    *  New items in the shop can't be added after Start()
    *  --> if needed in furture, take a look at ItemInventoryManager's RefreshPanels()
    */
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

        //Goes through every shop item in the array and refreshes title, description, icon and price.
        for (int i = 0; i < contentDistributor.scriptableObjectSkins.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
            shopPanels[i].shopItemTitle.text = contentDistributor.scriptableObjectSkins[i].title;
            shopPanels[i].shopItemDescription.text = contentDistributor.scriptableObjectSkins[i].description;
            shopPanels[i].shopItemPrice.text = "$ " + contentDistributor.scriptableObjectSkins[i].price.ToString();
            shopPanels[i].shopItemIcon = contentDistributor.scriptableObjectSkins[i].icon;
        }
        CheckPurchaseable();
    }

    //Checks for credits >= price of item, if true --> button is clickable.
    private void CheckPurchaseable()
    {
        for (int i = 0; i < contentDistributor.scriptableObjectSkins.Length; i++)
        {
            if (credit >= contentDistributor.scriptableObjectSkins[i].price)
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
        if (credit >= contentDistributor.scriptableObjectSkins[pos].price)
        {
            //Check, if key (Effect) is in the list.
            SkinTemplate item = contentDistributor.scriptableObjectSkins[pos];
            if (contentDistributor.itemsDictionary.ContainsKey(item.id))
            {
                credit -= item.price;
                Account.credits = credit;
                //Search for effect id in array with effects that has same id as id of ShopItem.
                contentDistributor.skinsDictionary[item.id].PurchaseButtonAction(item);
                contentDistributor.skinsDictionary[item.id].skinTemplate = item;
                // update amount of selected update in PlayFab
                GameObject.FindGameObjectWithTag("PlayFabUpdate").GetComponent<PlayfabUpdateUserData>().SetUpgradeAmountOnPlayFab(
                    item.id, contentDistributor.itemsDictionary[item.id].shopItem.amount);
            }
            RefreshPanels();
        }
    }
}
