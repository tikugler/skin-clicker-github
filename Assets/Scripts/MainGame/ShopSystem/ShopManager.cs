using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private string id = "double";
    public static int credit;
    public Text creditUIText;
    public GameObject[] shopPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] shopPanels; //Reference to scripts
    public Button[] purchaseButtons;
    private ContentDistributor contentDistributor;

    //dummy get List from Player
    public static ArrayList boughtItems = new ArrayList();

    //Set as many panels active/visible as needed.
    void Start()
    {
        for (int i = 0; i < contentDistributor.scriptableObjectItems.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        RefreshPanels();
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

        credit = contentDistributor.mainButton.credits; //dummy
        
        //Goes through every shop item in the array and refreshes title, description, icon and price.
        for (int i = 0; i < contentDistributor.scriptableObjectItems.Length; i++)
        {
            shopPanels[i].shopItemTitle.text = contentDistributor.scriptableObjectItems[i].title;
            shopPanels[i].shopItemDescription.text = contentDistributor.scriptableObjectItems[i].description;
            shopPanels[i].shopItemPrice.text = "$ " + contentDistributor.scriptableObjectItems[i].price.ToString();
            shopPanels[i].shopItemAmount.text = contentDistributor.scriptableObjectItems[i].amount.ToString();
            shopPanels[i].itemIcon = contentDistributor.scriptableObjectItems[i].icon;
        }
        creditUIText.text = "$ " + credit.ToString();
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
            if (contentDistributor.itemsDictionary.ContainsKey(item.id)) {
                credit -= item.price;
                contentDistributor.mainButton.SetCredits(credit); //dummy
                //Search for effect id in array with effects that has same id as id of ShopItem.
                contentDistributor.itemsDictionary[item.id].PurchaseButtonAction(item);
                contentDistributor.itemsDictionary[item.id].shopItem = contentDistributor.scriptableObjectItems[pos];
            }
            RefreshPanels();
        }
    }
}
