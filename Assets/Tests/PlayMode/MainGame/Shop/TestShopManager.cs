using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ShopManagerTest
{
    private GameObject canvasGameObject;
    private ContentDistributor distributor;
    private ShopManager manager;

    private ItemTemplate itemTemplate1;
    private string id1 = ItemNames.DoubleEffect;
    private string title1 = "Double Effect 1";
    private string description1 = "Test description double effect 1";
    private int amount1 = 0;
    private int price1 = 333;
    private Sprite icon1 = null;

    private ItemTemplate itemTemplate2;
    private string id2 = ItemNames.Worker;
    private string title2 = "Worker 1";
    private string description2 = "Test description Worker 1";
    private int amount2 = 0;
    private int price2 = 4;
    private Sprite icon2 = null;

    private ItemTemplate itemTemplate3;
    private string id3 = ItemNames.Worker;
    private string title3 = "Deutsche Bahn";
    private string description3 = "Guess who's late";
    private int amount3 = 3;
    private int price3 = 90;
    private Sprite icon3 = null;

    private ItemTemplate itemTemplate4;
    private string id4 = ItemNames.DoubleEffect;
    private string title4 = "rng text";
    private string description4 = "Too tired for a creative text";
    private int amount4 = 7;
    private int price4 = 921;
    private Sprite icon4 = null;

    private ItemTemplate itemTemplate5;
    private string id5 = ItemNames.DoubleEffect;
    private string title5 = "Free Item";
    private string description5 = "Free and useless effect ;)";
    private int amount5 = 78;
    private int price5 = 0;
    private Sprite icon5 = null;

    public ItemTemplate[] scriptableObjectItemsTestNoItem = new ItemTemplate[0];
    public ItemTemplate[] scriptableObjectItemsTestOneItem = new ItemTemplate[1];
    public ItemTemplate[] scriptableObjectItemsTestTwoItems = new ItemTemplate[2];
    public ItemTemplate[] scriptableObjectItemsTestFiveItems = new ItemTemplate[5];


    [OneTimeSetUp]
    public void Before()
    {
        SceneManager.LoadScene("MainGame");
    }


    [UnitySetUp]
    public IEnumerator Setup()
    {
        yield return null; //Wait till next update
        canvasGameObject = GameObject.Find("Canvas");

        GameObject contentDistributor = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ContentDistributor");
        distributor = contentDistributor.GetComponentInParent<ContentDistributor>();

        CreateItemTemplateForTesting();
    }

    private void SetupForPurchase()
    {
        GameObject shopPanel = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopPanel");
        manager = shopPanel.GetComponent<ShopManager>();
        OpenShopPopUpWithButton();

    }

    private void CreateItemTemplateForTesting()
    {
        //Create new testItem and add to scriptableObjectItemsTest
        itemTemplate1 = ScriptableObject.CreateInstance("ItemTemplate") as ItemTemplate;
        itemTemplate1.amount = amount1;
        itemTemplate1.description = description1;
        itemTemplate1.icon = icon1;
        itemTemplate1.id = id1;
        itemTemplate1.price = price1;
        itemTemplate1.title = title1;

        itemTemplate2 = ScriptableObject.CreateInstance("ItemTemplate") as ItemTemplate;
        itemTemplate2.amount = amount2;
        itemTemplate2.description = description2;
        itemTemplate2.icon = icon2;
        itemTemplate2.id = id2;
        itemTemplate2.price = price2;
        itemTemplate2.title = title2;

        itemTemplate3 = ScriptableObject.CreateInstance("ItemTemplate") as ItemTemplate;
        itemTemplate3.amount = amount3;
        itemTemplate3.description = description3;
        itemTemplate3.icon = icon3;
        itemTemplate3.id = id3;
        itemTemplate3.price = price3;
        itemTemplate3.title = title3;

        itemTemplate4 = ScriptableObject.CreateInstance("ItemTemplate") as ItemTemplate;
        itemTemplate4.amount = amount4;
        itemTemplate4.description = description4;
        itemTemplate4.icon = icon4;
        itemTemplate4.id = id4;
        itemTemplate4.price = price4;
        itemTemplate4.title = title4;

        itemTemplate5 = ScriptableObject.CreateInstance("ItemTemplate") as ItemTemplate;
        itemTemplate5.amount = amount5;
        itemTemplate5.description = description5;
        itemTemplate5.icon = icon5;
        itemTemplate5.id = id5;
        itemTemplate5.price = price5;
        itemTemplate5.title = title5;

        scriptableObjectItemsTestOneItem[0] = itemTemplate1;

        scriptableObjectItemsTestTwoItems[0] = itemTemplate1;
        scriptableObjectItemsTestTwoItems[1] = itemTemplate2;

        scriptableObjectItemsTestFiveItems[0] = itemTemplate1;
        scriptableObjectItemsTestFiveItems[1] = itemTemplate2;
        scriptableObjectItemsTestFiveItems[2] = itemTemplate3;
        scriptableObjectItemsTestFiveItems[3] = itemTemplate4;
        scriptableObjectItemsTestFiveItems[4] = itemTemplate5;

    }

    private void OpenShopPopUpWithButton()
    {
        Button shopOpenButton = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopButton").
            GetComponent<Button>();
        shopOpenButton.onClick.Invoke();
    }

    private void CloseShopPopUpWithButton()
    {
        Button shopCloseButton = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopCloseButton").
            GetComponent<Button>();
        shopCloseButton.onClick.Invoke();
    }


    /*
    *   Tests if panels are correctly active. Empty list can't be tested (scriptableObjectItemsTest) ---> NullPoiner in ShopManager
    *   For every test with diffrent size, there has to be a new Array (fix size & fully filled means no empty spot).
    *   Dont forget to refresh panels...
    */

    [UnityTest]
    public IEnumerator ShowNoItemInShop()
    {
        distributor.scriptableObjectItems = scriptableObjectItemsTestNoItem;
        SetupForPurchase();

        yield return null;
        //Array.Clear(distributor.scriptableObjectItems, 0, distributor.scriptableObjectItems.Length);

        int counterForActivePanels = 0;

        //Check if Active Panels = 1
        Assert.AreEqual(distributor.scriptableObjectItems.Length, 0);
        for (int i = 0; i < manager.shopPanelsGO.Length; i++)
        {
            if (manager.shopPanelsGO[i].activeSelf == true)
            {
                counterForActivePanels += 1;
            }
        }

        yield return null;
        Assert.AreEqual(counterForActivePanels, distributor.scriptableObjectItems.Length);
        CloseShopPopUpWithButton();
    }


    [UnityTest]
    public IEnumerator ShowOneItemInShop()
    {
        distributor.scriptableObjectItems = scriptableObjectItemsTestOneItem;
        SetupForPurchase();

        yield return null;
        //Array.Clear(distributor.scriptableObjectItems, 0, distributor.scriptableObjectItems.Length);

        int counterForActivePanels = 0;

        //Check if Active Panels = 1
        Assert.AreEqual(distributor.scriptableObjectItems.Length, 1);
        for (int i = 0; i < manager.shopPanelsGO.Length; i++)
        {
            if (manager.shopPanelsGO[i].activeSelf == true)
            {
                counterForActivePanels += 1;
            }
        }

        yield return null;
        Assert.AreEqual(counterForActivePanels, distributor.scriptableObjectItems.Length);
        CloseShopPopUpWithButton();
    }

    [UnityTest]
    public IEnumerator ShowTwoItemInShop()
    {
        distributor.scriptableObjectItems = scriptableObjectItemsTestTwoItems;
        SetupForPurchase();

        yield return null;
        //Array.Clear(distributor.scriptableObjectItems, 0, distributor.scriptableObjectItems.Length);

        int counterForActivePanels = 0;

        //Check if Active Panels = 2
        Assert.AreEqual(distributor.scriptableObjectItems.Length, 2);
        for (int i = 0; i < manager.shopPanelsGO.Length; i++)
        {
            if (manager.shopPanelsGO[i].activeSelf == true)
            {
                counterForActivePanels += 1;
            }
        }

        yield return null;
        Assert.AreEqual(counterForActivePanels, distributor.scriptableObjectItems.Length);
        CloseShopPopUpWithButton();
    }

    [UnityTest]
    public IEnumerator ShowFiveItemInShop()
    {
        distributor.scriptableObjectItems = scriptableObjectItemsTestFiveItems;
        SetupForPurchase();

        yield return null;
        //Array.Clear(distributor.scriptableObjectItems, 0, distributor.scriptableObjectItems.Length);

        int counterForActivePanels = 0;

        //Check if Active Panels = 5
        Assert.AreEqual(distributor.scriptableObjectItems.Length, 5);
        int panelsCounter = 0;
        for (int i = 0; i < manager.shopPanelsGO.Length; i++)
        {
            panelsCounter++;
            if (manager.shopPanelsGO[i].activeSelf == true)
            {
                counterForActivePanels += 1;
            }
        }

        yield return null;
        //Debug.Log("Anzahl Panels 21/ " + panelsCounter);
        //Debug.Log("Anzahl Items in List: " + scriptableObjectItemsTestFiveItems.Length + "  ||  Anzahl Counter: " + counterForActivePanels);
        Assert.AreEqual(counterForActivePanels, distributor.scriptableObjectItems.Length);
        CloseShopPopUpWithButton();
    }

    /*
     * Gets string/text from shopitem and checks if given text matches with original template string.
     * Tried to compare/get ShopTemplate it self from GO but didnt work, so just compare strings.
     * No need to test scriptableObjectItemsTestNoItem --> item is invisible
     * "$ " + itemTemplate price is hardcoded like in ShopMamanger's RefreshCredits().
    */
    [UnityTest]
    public IEnumerator TestForTextInShopTemplateOneItem()
    {
        distributor.scriptableObjectItems = scriptableObjectItemsTestOneItem;
        SetupForPurchase();

        yield return null;


        GameObject shopItemGO = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate");
        var shopItemTemplate1 = shopItemGO.GetComponent<ShopTemplate>();


        Assert.AreEqual(shopItemTemplate1.shopItemTitle.text, itemTemplate1.title);
        Assert.AreEqual(shopItemTemplate1.shopItemDescription.text, itemTemplate1.description);
        Assert.AreEqual(shopItemTemplate1.shopItemPrice.text, "$ " + itemTemplate1.price.ToString());
        Assert.AreEqual(shopItemTemplate1.shopItemAmount.text, amount1.ToString());
        //Assert.AreEqual(shopItemTemplate1.shopItemicon.text, itemTemplate1.icon);

        CloseShopPopUpWithButton();
    }

    [UnityTest]
    public IEnumerator TestForTextInShopTemplateTwoItems()
    {
        distributor.scriptableObjectItems = scriptableObjectItemsTestTwoItems;
        SetupForPurchase();
        yield return null;

        GameObject shopItemGO = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate");
        var shopItemTemplate1 = shopItemGO.GetComponent<ShopTemplate>();

        GameObject shopItemGO2 = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate (1)");
        var shopItemTemplate2 = shopItemGO2.GetComponent<ShopTemplate>();

        Assert.AreEqual(shopItemTemplate1.shopItemTitle.text, itemTemplate1.title);
        Assert.AreEqual(shopItemTemplate1.shopItemDescription.text, itemTemplate1.description);
        Assert.AreEqual(shopItemTemplate1.shopItemPrice.text, "$ " + itemTemplate1.price.ToString());
        Assert.AreEqual(shopItemTemplate1.shopItemAmount.text, itemTemplate1.amount.ToString());
        //Assert.AreEqual(shopItemTemplate1.shopItemicon.text, itemTemplate1.icon);

        Assert.AreEqual(shopItemTemplate2.shopItemTitle.text, itemTemplate2.title);
        Assert.AreEqual(shopItemTemplate2.shopItemDescription.text, itemTemplate2.description);
        Assert.AreEqual(shopItemTemplate2.shopItemPrice.text, "$ " + itemTemplate2.price.ToString());
        Assert.AreEqual(shopItemTemplate2.shopItemAmount.text, itemTemplate2.amount.ToString());
        //Assert.AreEqual(shopItemTemplate2.shopItemicon.text, itemTemplate2.icon);

        CloseShopPopUpWithButton();
    }


    [UnityTest]
    public IEnumerator AddFiveItemsAndOpenShopAgain()
    {
        distributor.scriptableObjectItems = scriptableObjectItemsTestFiveItems;
        SetupForPurchase();
        yield return null;

        GameObject shopItemGO = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate");
        var shopItemTemplate1 = shopItemGO.GetComponent<ShopTemplate>();

        GameObject shopItemGO2 = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate (1)");
        var shopItemTemplate2 = shopItemGO2.GetComponent<ShopTemplate>();

        GameObject shopItemGO3 = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate (2)");
        var shopItemTemplate3 = shopItemGO3.GetComponent<ShopTemplate>();

        GameObject shopItemGO4 = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate (3)");
        var shopItemTemplate4 = shopItemGO4.GetComponent<ShopTemplate>();

        GameObject shopItemGO5 = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate (4)");
        var shopItemTemplate5 = shopItemGO5.GetComponent<ShopTemplate>();

        Assert.AreEqual(shopItemTemplate1.shopItemTitle.text, itemTemplate1.title);
        Assert.AreEqual(shopItemTemplate1.shopItemDescription.text, itemTemplate1.description);
        Assert.AreEqual(shopItemTemplate1.shopItemPrice.text, "$ " + itemTemplate1.price.ToString());
        Assert.AreEqual(shopItemTemplate1.shopItemAmount.text, itemTemplate1.amount.ToString());
        //Assert.AreEqual(shopItemTemplate1.shopItemicon.text, itemTemplate1.icon);

        Assert.AreEqual(shopItemTemplate2.shopItemTitle.text, itemTemplate2.title);
        Assert.AreEqual(shopItemTemplate2.shopItemDescription.text, itemTemplate2.description);
        Assert.AreEqual(shopItemTemplate2.shopItemPrice.text, "$ " + itemTemplate2.price.ToString());
        Assert.AreEqual(shopItemTemplate2.shopItemAmount.text, itemTemplate2.amount.ToString());
        //Assert.AreEqual(shopItemTemplate2.shopItemicon.text, itemTemplate2.icon);

        Assert.AreEqual(shopItemTemplate3.shopItemTitle.text, itemTemplate3.title);
        Assert.AreEqual(shopItemTemplate3.shopItemDescription.text, itemTemplate3.description);
        Assert.AreEqual(shopItemTemplate3.shopItemPrice.text, "$ " + itemTemplate3.price.ToString());
        Assert.AreEqual(shopItemTemplate3.shopItemAmount.text, itemTemplate3.amount.ToString());
        //Assert.AreEqual(shopItemTemplate3.shopItemicon.text, itemTemplate3.icon);

        Assert.AreEqual(shopItemTemplate4.shopItemTitle.text, itemTemplate4.title);
        Assert.AreEqual(shopItemTemplate4.shopItemDescription.text, itemTemplate4.description);
        Assert.AreEqual(shopItemTemplate4.shopItemPrice.text, "$ " + itemTemplate4.price.ToString());
        Assert.AreEqual(shopItemTemplate4.shopItemAmount.text, itemTemplate4.amount.ToString());
        //Assert.AreEqual(shopItemTemplate4.shopItemicon.text, itemTemplate4.icon);

        Assert.AreEqual(shopItemTemplate5.shopItemTitle.text, itemTemplate5.title);
        Assert.AreEqual(shopItemTemplate5.shopItemDescription.text, itemTemplate5.description);
        Assert.AreEqual(shopItemTemplate5.shopItemPrice.text, "$ " + itemTemplate5.price.ToString());
        Assert.AreEqual(shopItemTemplate5.shopItemAmount.text, itemTemplate5.amount.ToString());
        //Assert.AreEqual(shopItemTemplate5.shopItemicon.text, itemTemplate5.icon);

        CloseShopPopUpWithButton();
        yield return null;
        OpenShopPopUpWithButton();
        yield return null;

        Assert.AreEqual(shopItemTemplate1.shopItemTitle.text, itemTemplate1.title);
        Assert.AreEqual(shopItemTemplate1.shopItemDescription.text, itemTemplate1.description);
        Assert.AreEqual(shopItemTemplate1.shopItemPrice.text, "$ " + itemTemplate1.price.ToString());
        Assert.AreEqual(shopItemTemplate1.shopItemAmount.text, itemTemplate1.amount.ToString());
        //Assert.AreEqual(shopItemTemplate1.shopItemicon.text, itemTemplate1.icon);

        Assert.AreEqual(shopItemTemplate2.shopItemTitle.text, itemTemplate2.title);
        Assert.AreEqual(shopItemTemplate2.shopItemDescription.text, itemTemplate2.description);
        Assert.AreEqual(shopItemTemplate2.shopItemPrice.text, "$ " + itemTemplate2.price.ToString());
        Assert.AreEqual(shopItemTemplate2.shopItemAmount.text, itemTemplate2.amount.ToString());
        //Assert.AreEqual(shopItemTemplate2.shopItemicon.text, itemTemplate2.icon);

        Assert.AreEqual(shopItemTemplate3.shopItemTitle.text, itemTemplate3.title);
        Assert.AreEqual(shopItemTemplate3.shopItemDescription.text, itemTemplate3.description);
        Assert.AreEqual(shopItemTemplate3.shopItemPrice.text, "$ " + itemTemplate3.price.ToString());
        Assert.AreEqual(shopItemTemplate3.shopItemAmount.text, itemTemplate3.amount.ToString());
        //Assert.AreEqual(shopItemTemplate3.shopItemicon.text, itemTemplate3.icon);

        Assert.AreEqual(shopItemTemplate4.shopItemTitle.text, itemTemplate4.title);
        Assert.AreEqual(shopItemTemplate4.shopItemDescription.text, itemTemplate4.description);
        Assert.AreEqual(shopItemTemplate4.shopItemPrice.text, "$ " + itemTemplate4.price.ToString());
        Assert.AreEqual(shopItemTemplate4.shopItemAmount.text, itemTemplate4.amount.ToString());
        //Assert.AreEqual(shopItemTemplate4.shopItemicon.text, itemTemplate4.icon);

        Assert.AreEqual(shopItemTemplate5.shopItemTitle.text, itemTemplate5.title);
        Assert.AreEqual(shopItemTemplate5.shopItemDescription.text, itemTemplate5.description);
        Assert.AreEqual(shopItemTemplate5.shopItemPrice.text, "$ " + itemTemplate5.price.ToString());
        Assert.AreEqual(shopItemTemplate5.shopItemAmount.text, itemTemplate5.amount.ToString());
        //Assert.AreEqual(shopItemTemplate5.shopItemicon.text, itemTemplate5.icon);

        CloseShopPopUpWithButton();
    }

    [UnityTest]
    public IEnumerator BuySomethingInShopAndCheckIfItemsAndCreditsAreChanging()
    {
        Account.credits = ((itemTemplate1.price + itemTemplate2.price)*10);
        distributor.scriptableObjectItems = scriptableObjectItemsTestTwoItems;
        SetupForPurchase();

        yield return null;

        GameObject shopItemGO = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate");
        var shopItemTemplate1 = shopItemGO.GetComponent<ShopTemplate>();
        GameObject shopItemButtonGO1 = FindObjectHelper.
            FindObjectInParent(shopItemGO, "PurchaseButton");
        var shopItemButton1 = shopItemButtonGO1.GetComponent<Button>();

        GameObject shopItemGO2 = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate (1)");
        var shopItemTemplate2 = shopItemGO2.GetComponent<ShopTemplate>();
        GameObject shopItemButtonGO2 = FindObjectHelper.
            FindObjectInParent(shopItemGO2, "PurchaseButton");
        var shopItemButton2 = shopItemButtonGO2.GetComponent<Button>();

        Assert.AreEqual(shopItemTemplate1.shopItemTitle.text, itemTemplate1.title);
        Assert.AreEqual(shopItemTemplate1.shopItemDescription.text, itemTemplate1.description);
        Assert.AreEqual(shopItemTemplate1.shopItemPrice.text, "$ " + itemTemplate1.price.ToString());
        Assert.AreEqual(shopItemTemplate1.shopItemAmount.text, itemTemplate1.amount.ToString());
        //Assert.AreEqual(shopItemTemplate1.shopItemicon.text, itemTemplate1.icon);

        Assert.AreEqual(shopItemTemplate2.shopItemTitle.text, itemTemplate2.title);
        Assert.AreEqual(shopItemTemplate2.shopItemDescription.text, itemTemplate2.description);
        Assert.AreEqual(shopItemTemplate2.shopItemPrice.text, "$ " + itemTemplate2.price.ToString());
        Assert.AreEqual(shopItemTemplate2.shopItemAmount.text, itemTemplate2.amount.ToString());
        //Assert.AreEqual(shopItemTemplate2.shopItemicon.text, itemTemplate2.icon);

        int oldCredits = Account.credits;

        int oldPrice1 = itemTemplate1.price;
        int oldAmount1 = itemTemplate1.amount;

        int oldPrice2 = itemTemplate2.price;
        int oldAmount2 = itemTemplate2.amount;

        shopItemButton1.onClick.Invoke();
        shopItemButton2.onClick.Invoke();
        yield return null;

        int newCredits = oldCredits - (oldPrice1 + oldPrice2);
        Assert.AreEqual(Account.credits, newCredits);

        Assert.AreEqual(shopItemTemplate1.shopItemTitle.text, itemTemplate1.title);
        Assert.AreEqual(shopItemTemplate1.shopItemDescription.text, itemTemplate1.description);
        Assert.AreNotEqual(shopItemTemplate1.shopItemPrice, oldPrice1);
        Assert.AreNotEqual(shopItemTemplate1.shopItemAmount, oldAmount1);
        //Assert.AreEqual(shopItemTemplate1.shopItemicon.text, itemTemplate1.icon);

        Assert.AreEqual(shopItemTemplate2.shopItemTitle.text, itemTemplate2.title);
        Assert.AreEqual(shopItemTemplate2.shopItemDescription.text, itemTemplate2.description);
        Assert.AreNotEqual(shopItemTemplate2.shopItemPrice,  oldPrice2);
        Assert.AreNotEqual(shopItemTemplate2.shopItemAmount, oldAmount2);
        //Assert.AreEqual(shopItemTemplate2.shopItemicon.text, itemTemplate2.icon);

        CloseShopPopUpWithButton();
    }

    [UnityTest]
    public IEnumerator TryToBuyItemWithLessCreditsThanPrice()
    {
        Account.credits = itemTemplate1.price - 1;
        distributor.scriptableObjectItems = scriptableObjectItemsTestOneItem;
        SetupForPurchase();

        yield return null;

        manager.RefreshPanels();
        yield return null;

        GameObject shopItemGO = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate");
        var shopItemTemplate1 = shopItemGO.GetComponent<ShopTemplate>();
        GameObject shopItemButtonGO1 = FindObjectHelper.
            FindObjectInParent(shopItemGO, "PurchaseButton");
        var shopItemButton1 = shopItemButtonGO1.GetComponent<Button>();

        int oldCredits = Account.credits;

        manager.RefreshPanels();
        Assert.IsFalse(shopItemButton1.interactable);
        shopItemButton1.onClick.Invoke();
        Assert.AreEqual(Account.credits, oldCredits);

        CloseShopPopUpWithButton();
    }

    [UnityTest]
    public IEnumerator TryToBuyItemWithExactAmountOfCreditsAsPrice()
    {
        Account.credits = itemTemplate1.price;
        distributor.scriptableObjectItems = scriptableObjectItemsTestOneItem;
        SetupForPurchase();

        yield return null;

        GameObject shopItemGO = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate");
        var shopItemTemplate1 = shopItemGO.GetComponent<ShopTemplate>();
        GameObject shopItemButtonGO1 = FindObjectHelper.
            FindObjectInParent(shopItemGO, "PurchaseButton");
        var shopItemButton1 = shopItemButtonGO1.GetComponent<Button>();

        int oldCredits = Account.credits;

        manager.RefreshPanels();
        Assert.IsTrue(shopItemButton1.interactable);
        shopItemButton1.onClick.Invoke();
        Assert.AreEqual(Account.credits, 0);

        CloseShopPopUpWithButton();
    }

    [UnityTest]
    public IEnumerator TryToBuyItemWithMoreCreditsThanPrice()
    {
        Account.credits = itemTemplate1.price * 2;
        distributor.scriptableObjectItems = scriptableObjectItemsTestOneItem;
        SetupForPurchase();

        yield return null;

        GameObject shopItemGO = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate");
        var shopItemTemplate1 = shopItemGO.GetComponent<ShopTemplate>();
        GameObject shopItemButtonGO1 = FindObjectHelper.
            FindObjectInParent(shopItemGO, "PurchaseButton");
        var shopItemButton1 = shopItemButtonGO1.GetComponent<Button>();

        int oldCredits = Account.credits;

        manager.RefreshPanels();
        Assert.IsTrue(shopItemButton1.interactable);
        shopItemButton1.onClick.Invoke();
        Assert.AreEqual(Account.credits, oldCredits/2);

        CloseShopPopUpWithButton();
    }

    [UnityTest]
    public IEnumerator RefreshCreditsTest()
    {
        Account.credits = itemTemplate1.price;
        distributor.scriptableObjectItems = scriptableObjectItemsTestOneItem;
        SetupForPurchase();

        yield return null;

        manager.RefreshPanels();
        yield return null;

        GameObject shopItemGO = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopItemTemplate");
        var shopItemTemplate1 = shopItemGO.GetComponent<ShopTemplate>();
        GameObject shopItemButtonGO1 = FindObjectHelper.
            FindObjectInParent(shopItemGO, "PurchaseButton");
        var shopItemButton1 = shopItemButtonGO1.GetComponent<Button>();
        GameObject shopCreditGO = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "CreditUIText");
        var shopCreditText = shopCreditGO.GetComponent<Text>();

        manager.RefreshPanels();
        Assert.AreEqual("$ " + Account.credits.ToString(), shopCreditText.text.ToString());

        yield return null;

        shopItemButton1.onClick.Invoke();
        Assert.AreEqual("$ 0", shopCreditText.text.ToString());

        CloseShopPopUpWithButton();
    }
}
