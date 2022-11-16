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


    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        SceneManager.LoadScene("StartMenu");
    }


    [UnitySetUp]
    public IEnumerator Setup()
    {
        yield return null; //Wait till next update
        canvasGameObject = GameObject.Find("Canvas");

        GameObject contentDistributor = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ContentDistributor");
        distributor = contentDistributor.GetComponentInParent<ContentDistributor>();
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

    [UnityTest]
    public IEnumerator ShowActivePanelsInItemsInShop()
    {
        GameObject shopPanel = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopPanel");
        OpenShopPopUpWithButton();

        distributor.itemsDictionary.Clear();
        yield return null;
        Assert.Equals(distributor.itemsDictionary, 0);
        //Check if Active Panels = 0

        distributor.CreateItems();
        yield return null;
        //Check if distributor.itemsDictionary size is same as active;
    }

    [UnityTest]
    public IEnumerator ShowScriptableObjectItemsInShop()
    {
        GameObject shopPanel = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ShopPanel");
        OpenShopPopUpWithButton();

        distributor.itemsDictionary.Clear();
        Assert.Equals(distributor.itemsDictionary, 0);
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
