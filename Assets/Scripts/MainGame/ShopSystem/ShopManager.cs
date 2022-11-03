using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private string id = "double";
    public static int credit;
    public Text creditUIText;
    public ShopItem[] shopItems;
    public GameObject[] shopPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] shopPanels; //Reference to scripts
    public Button[] purchaseButtons;
    public Dictionary<string, ItemEffect> effects = new Dictionary<string, ItemEffect>();

    //Get credits from dummy
    public DummyButton dummyButtonObj;
    public GameObject dummyButton;

    void Start()
    {
        CreateItems();
        //dummyButtonObj = dummyButton.GetComponent<DummyButton>(); //dummy
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        RefreshPanels();
    }

    //Creats and adds ItemEffects to key-value-pair.
    //The key is ALWAYS the exact class name of a item!
    public void CreateItems() {
        var doubleEffect = new DoubleEffect(this);
        effects.Add(doubleEffect.id.ToString(), doubleEffect);
        //Debug.Log("new DoubleEffect ID: " + doubleEffect.id);
    }

    //Refreshes panels --> new values are displayed.
    public void RefreshPanels()
    {
        credit = dummyButtonObj.GetCredits(); //dummy
        //Goes through every shop item in the array and refreshes title, description and price.
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopPanels[i].shopItemTitle.text = shopItems[i].title;
            shopPanels[i].shopItemDescription.text = shopItems[i].description;
            shopPanels[i].shopItemPrice.text = "$ " + shopItems[i].price.ToString();
        }
        creditUIText.text = "$ " + credit.ToString();
        CheckPurchaseable();
    }

    //Checks for credits >= price of item, if true --> button is clickable.
    private void CheckPurchaseable()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (credit >= shopItems[i].price)
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
        if (credit >= shopItems[pos].price)
        {
            //Check, if key (Effect) is in the list.
            var item = shopItems[pos];
            if (effects.ContainsKey(item.id)) {
                credit -= item.price;
                dummyButtonObj.SetCredits(credit); //dummy
                //Search for effect id in array with effects that has same id as id of ShopItem.
                effects[item.id].PurchaseButtonAction();
                effects[item.id].CalculateNewPrice(item);
            } 
            RefreshPanels();
        }
    }
}
