using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ShopManager of items.
/// Shows every item that has been added to ContentDistributor's scriptableObjectItems.
/// </summary>
public class ShopPackageManager : MonoBehaviour
{
    public static int credit;
    public Text creditUIText;
    public GameObject[] shopPanelsGO; //GO means GameObject, has reference to GameObjects
    public ShopTemplate[] shopPanels; //Reference to scripts
    public Button[] purchaseButtons;
    private ContentDistributor contentDistributor;
    [SerializeField] SoundSceneManager soundManager;


    void Start()
    {
        contentDistributor = ContentDistributor.contentDistributor;
        for (int i = 0; i < contentDistributor.scriptableObjectPackages.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }

        RefreshPanels();
    }

    /// <summary>
    /// If change "$ " + also change tests.
    /// </summary>
    private void RefreshCredits()
    {
        credit = Account.credits;
        creditUIText.text = "$ " + credit.ToString();
    }

    /// <summary>
    /// Set just as many panels active/visible as needed.
    /// Refreshs credits 
    /// </summary>
    public void RefreshPanels()
    {
        //contentDistributor has to be here otherwise nullpointer becaus Start() isn't working before this methode call.
        contentDistributor = ContentDistributor.contentDistributor;

        RefreshCredits();

        //Sets panels unused panels inactive and starts with the last panel in the list.
        int indexShopPanels = shopPanelsGO.Length - 1;
        for (int i = 0; i < (shopPanelsGO.Length - contentDistributor.scriptableObjectPackages.Length); i++)
        {
            shopPanelsGO[indexShopPanels - i].SetActive(false);
            //Debug.Log("Index : " + (indexShopPanels - i) + "/" + (shopPanelsGO.Length -1));
        }

        //Goes through every shop package in the array and refreshes title, description, icon and price.
        for (int i = 0; i < contentDistributor.scriptableObjectPackages.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
            shopPanels[i].shopItemTitle.text = contentDistributor.scriptableObjectPackages[i].title;
            shopPanels[i].shopItemDescription.text = contentDistributor.scriptableObjectPackages[i].description;
            shopPanels[i].shopItemIcon = contentDistributor.scriptableObjectPackages[i].icon;

          

            //If icon != null, show icon of package
            if (shopPanels[i].shopItemIcon != null)
            {
                FindObjectHelper.FindObjectInParent(shopPanelsGO[i], "Image").GetComponent<Image>().sprite = shopPanels[i].shopItemIcon;
            }

            
            shopPanels[i].rarity.color = Color.cyan;
            FindObjectHelper.FindObjectInParent(shopPanelsGO[i], "Background").GetComponent<Image>().color = Color.cyan;
            
            

        }
    }

 
    public void PurchaseButtonAction(int pos)
    {
        //If credit >= as price of shopItem on position pos in array.
        if (true)
        {
            //Check, if key (Effect) is in the list.
            PackageTemplate item = contentDistributor.scriptableObjectPackages[pos];
            if (contentDistributor.packagesDictionary.ContainsKey(item.id))
            {
                soundManager.PlayPayWithCoinsSound();
                //Search for effect id in array with effects that has same id as id of ShopItem.
                contentDistributor.packagesDictionary[item.id].PurchaseButtonAction(item);

            }
        }
        RefreshPanels();
    }
}
