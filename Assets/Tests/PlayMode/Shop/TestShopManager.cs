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

    private ItemTemplate itemTemplate1;
    private string id1 = "DoubleEffect";
    private string title1 = "Double Effect 1";
    private string description1 = "Test description double effect 1";
    private int amount1 = 0;
    private int price1 = 333;
    private Sprite icon1 = null;

    private ItemTemplate itemTemplate2;
    private string id2 = "Worker";
    private string title2 = "Worker 1";
    private string description2 = "Test description Worker 1";
    private int amount2 = 0;
    private int price2 = 4;
    private Sprite icon2 = null;

private ItemTemplate itemTemplate3;
    private string id3 = "Worker";
    private string title3 = "Deutsche Bahn";
    private string description3 = "Guess who's late";
    private int amount3 = 3;
    private int price3 = 90;
    private Sprite icon3 = null;

    private ItemTemplate itemTemplate4 ;
    private string id4 = "DoubleEffect";
    private string title4 = "rng text";
    private string description4 = "Too tired for a creative text";
    private int amount4 = 7;
    private int price4 = 921;
    private Sprite icon4 = null;

    private ItemTemplate itemTemplate5;
    private string id5 = "DoubleEffect";
    private string title5 = "Free Item";
    private string description5 = "Free and useless effect ;)";
    private int amount5 = 78;
    private int price5 = 0;
    private Sprite icon5 = null;

    public ItemTemplate[] scriptableObjectItemsTestOneItem = new ItemTemplate[1];
    public ItemTemplate[] scriptableObjectItemsTestTwoItems = new ItemTemplate[2];
    public ItemTemplate[] scriptableObjectItemsTestFiveItems = new ItemTemplate[5];


    [OneTimeSetUp]
    public void OneTimeSetUp()
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
        Button shopButton = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopButton").
            GetComponent<Button>();
        shopButton.onClick.Invoke();
    }


    /*
    *   Tests if panels are correctly active. Empty list can't be tested (scriptableObjectItemsTest) ---> NullPoiner in ShopManager
    *   For every test with diffrent size, there has to be a new Array (fix size & fully filled means no empty spot).
    *   Dont forget to refresh panels...
    */
    [UnityTest]
    public IEnumerator ShowActivePanelsInItemsInShop()
    {
        GameObject shopPanel = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopPanel");
        ShopManager manager = shopPanel.GetComponent<ShopManager>();
        OpenShopPopUpWithButton();

        //Problems with creating of itemtemplate --> Test crashes
        CreateItemTemplateForTesting();
        Debug.Log("Till here, everything is working...");
        yield return null;
        //Array.Clear(distributor.scriptableObjectItems, 0, distributor.scriptableObjectItems.Length);

        Debug.Log("Size of new SO-List before adding: " + scriptableObjectItemsTestOneItem.Length);
        Debug.Log("Dead");
        distributor.scriptableObjectItems = scriptableObjectItemsTestOneItem;
        Debug.Log("Size of new SO-List after adding: " + scriptableObjectItemsTestOneItem.Length);
        int counterForActivePanels = 0;
        manager.RefreshPanels();
        yield return null;

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
        Debug.Log("Active Panels should be 0 : " + counterForActivePanels + " || " + "0");
        //Assert.AreEqual(counterForActivePanels, 0);
    }

    [UnityTest]
    public IEnumerator ShowScriptableObjectItemsInShop()
    {
        GameObject shopPanel = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopPanel");
        OpenShopPopUpWithButton();

        distributor.itemsDictionary.Clear();
        Assert.AreEqual(distributor.itemsDictionary, 0);
        //Check if Active Panels = 0

        distributor.CreateItems();
        //Check if distributor.itemsDictionary size is same as active;

        // How do I get the Text of a ShopTemplate????
        // Prefab shopButton = FindObjectHelper.
        //     FindObjectInParent(shopPanel, "ShopItemTemplate").
        //     GetComponent<Button>();
        yield return null;
    }
}
