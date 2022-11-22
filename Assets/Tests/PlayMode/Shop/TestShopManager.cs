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

    public ItemTemplate[] scriptableObjectItemsTest;
    private ItemTemplate itemTemplate1;


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
        string description = "Test description";
        string id = "DoubleEffect";
        string title = "TestItemV1";
        int price = 333;

        itemTemplate1 = ScriptableObject.CreateInstance("ItemTemplate") as ItemTemplate;
        itemTemplate1.amount = 0;
        itemTemplate1.description = description;
        itemTemplate1.icon = null;
        itemTemplate1.id = id;
        itemTemplate1.price = price;
        itemTemplate1.title = title;
    }

    private void OpenShopPopUpWithButton()
    {
        Button shopButton = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopButton").
            GetComponent<Button>();
        shopButton.onClick.Invoke();
    }

    [UnityTest]
    public IEnumerator ShopPopUpIsNotActiveWhenTheSceneLoaded()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        yield return null;
        canvasGameObject = GameObject.Find("Canvas");
        GameObject shopPanel = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopPanel");
        yield return null;
        Assert.AreEqual(false, shopPanel.activeSelf);
    }

    [UnityTest]
    public IEnumerator ShopPopUpOpensPopUpWindow()
    {
        GameObject shopPanel = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopPanel");
        shopPanel.SetActive(false);
        Button shopButton = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopButton").
            GetComponent<Button>();
        Assert.AreEqual(false, shopPanel.activeSelf);
        shopButton.onClick.Invoke();
        yield return null;
        Assert.AreEqual(true, shopPanel.activeSelf);
    }

    [UnityTest]
    public IEnumerator CloseButtonActionOfShopPopUp()
    {
        GameObject shopPanel = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopPanel");
        OpenShopPopUpWithButton();
        Assert.AreEqual(true, shopPanel.activeSelf);

        Button closeButton = GameObject.Find("ShopCloseButton").GetComponent<Button>();
        closeButton.onClick.Invoke();

        yield return null;
        Assert.AreEqual(false, shopPanel.activeSelf);
    }

    //Tests if panels are correctly active. Empty list can't be tested (scriptableObjectItemsTest)
    [UnityTest]
    public IEnumerator ShowActivePanelsInItemsInShop()
    {
        GameObject shopPanel = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopPanel");
        ShopManager manager = shopPanel.GetComponent<ShopManager>();
        OpenShopPopUpWithButton();
        //Array.Clear(distributor.scriptableObjectItems, 0, distributor.scriptableObjectItems.Length);

        scriptableObjectItemsTest[0] = itemTemplate1;
        distributor.scriptableObjectItems = scriptableObjectItemsTest;

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
        Assert.AreEqual(counterForActivePanels, 0);
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
