using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int credit;
    public Text creditUIText;
    public ShopItem[] shopItems;
    public GameObject[] shopPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] shopPanels; //Reference to scripts
    public Button[] purchaseButtons;

    //Get credits from dummy
    public DummyButton dummyButtonObj;
    [SerializeField] GameObject dummyButton;

    void Start() {
        dummyButtonObj = dummyButton.GetComponent<DummyButton>(); //dummy
        for (int i = 0; i < shopItems.Length; i++) {
            shopPanelsGO[i].SetActive(true);
        }
        RefreshPanels();
    }

    public void RefreshPanels() {
        credit = dummyButtonObj.GetCredits(); //dummy
        for (int i = 0; i < shopItems.Length; i++) {
            shopPanels[i].shopItemTitle.text = shopItems[i].title;
            shopPanels[i].shopItemDescription.text = shopItems[i].description;
            shopPanels[i].shopItemPrice.text = "$ " + shopItems[i].price.ToString();
        }
        creditUIText.text = "$ " + credit.ToString();
        CheckPurchaseable();
    }

    //Checks for credits >= price of item, if true --> button is clickable.
    public void CheckPurchaseable() {
        for (int i = 0; i < shopItems.Length; i++) {
            if (credit >= shopItems[i].price) {
                purchaseButtons[i].interactable = true;
                //mb some effects like backlighting for an active button
            } else {
                purchaseButtons[i].interactable = false;
            }
        }
    }
    
    public void PurchaseButtonAction(int pos) {
        if (credit >= shopItems[pos].price) {
            credit -= shopItems[pos].price;
            dummyButtonObj.SetCredits(credit); //dummy
            RefreshPanels();
        }
    }

}
