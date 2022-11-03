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
        var doubleEffect = new DoubleEffect();
        effects.Add(doubleEffect.id.ToString(), doubleEffect);
        //Debug.Log("new DoubleEffect ID: " + doubleEffect.id);
    }

    public void RefreshPanels()
    {
        credit = dummyButtonObj.GetCredits(); //dummy
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

    public void PurchaseButtonAction(int pos)
    {
        if (credit >= shopItems[pos].price)
        {
            if (effects.ContainsKey(shopItems[pos].id)) {
                Debug.Log("Effects-Dictionary contains key");
                Debug.Log("ID Shop Item: " + shopItems[pos].id.ToString());
                credit -= shopItems[pos].price;
                dummyButtonObj.SetCredits(credit); //dummy
                effects[shopItems[pos].id].PurchaseButtonAction();
            } 
            RefreshPanels();
        }
    }
}
