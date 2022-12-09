using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using UnityEngine;
using UnityEngine.UI;

/*
*  This class is used to share important objects with other classes.
*  There is only one (static) contentDistributor.
*/
public class ContentDistributor : MonoBehaviour
{
    public static ContentDistributor contentDistributor;
    public string id;
    public ShopManager shopManager;
    public ShopSkinManager shopSkinManager;
    public ItemInventoryManager itemInventoryManager;
    //public SkinManager itemInventoryManager;
    public DummyButton mainButton;
    public ItemTemplate[] scriptableObjectItems;
    public SkinTemplate[] scriptableObjectSkins;
    public Dictionary<string, ItemEffect> itemsDictionary = new Dictionary<string, ItemEffect>();
    public Dictionary<string, SkinEffect> skinsDictionary = new Dictionary<string, SkinEffect>();

    //Player stuff for demo
    public ArrayList boughtItemsOfPlayer = new ArrayList();


    // make sure that SetUpgrade is called before ShopManager.RefreshPanels
    // Awake is called before the application starts.
    private void Awake()
    {
        if (contentDistributor == null)
        {
            contentDistributor = this;
            CreateItems();
            CreateSkins();
            SetUpgrades();
        }
    }


    /* 
    *  Creats and adds ItemEffects to key-value-pair.
    *  The key is ALWAYS the exact class name of a item!
    *  Mb creat per stackable item an Array/Stack? --> Would solve stack problem
    */
    public void CreateItems()
    {
        var doubleEffect = new DoubleEffect();
        itemsDictionary.Add(doubleEffect.id.ToString(), doubleEffect);

        var testEffect = new TestEffect();
        itemsDictionary.Add(testEffect.id.ToString(), testEffect);

        var worker = new Worker();
        itemsDictionary.Add(worker.id.ToString(), worker);
    }

    /* 
    *  Creats and adds ItemEffects to key-value-pair.
    *  The key is ALWAYS the exact class name of a item!
    *  Mb creat per stackable item an Array/Stack? --> Would solve stack problem
    */
    public void CreateSkins()
    {
        var testSkin = new TestSkin();
        var testSkinTemplate = CreateSkinTemplate(testSkin);
        skinsDictionary.Add(testSkin.id.ToString(), testSkin);
        testSkinTemplate.description = "Fancy Description";

        scriptableObjectSkins = new SkinTemplate[1];
        scriptableObjectSkins[0] = testSkinTemplate;
    }

    private SkinTemplate CreateSkinTemplate(SkinEffect skin) {
        SkinTemplate skinTemplate = SkinTemplate.CreateInstance<SkinTemplate>();
        skinTemplate.id = skin.id;
        skinTemplate.title = skin.id;
        skinTemplate.rarity = skin.rarity;
        skinTemplate.price = skin.price;
        skinTemplate.icon = null; //mb like a thumbnail
        skinTemplate.fullPicture = null;
        skin.skinTemplate = skinTemplate;

        return skinTemplate;
    }

    /// <summary>
    /// sets the number of performed upgrade for each item according to Account.upgradeList
    /// </summary>
    private void SetUpgrades()
    {

        foreach (ItemTemplate item in scriptableObjectItems)
        {
            item.amount = 0; // the amount at the beginning must be 0
            item.price = item.startPrice;  // the price is equal to startPrice (initial price) if the amount is 0
                                           // performedUpgrade is 0 if item does not exists in upgradeList,
                                           // otherwise, the value in the upgradeList for related key
            Account.upgradeList.TryGetValue(item.id, out int performedUpgrade);
            for (int i = 0; i < performedUpgrade; i++)
            {
                // PurchaseButtonAction is called performedUpgrade times for related item without reducing credits
                itemsDictionary[item.id].PurchaseButtonAction(item);
            }
            itemsDictionary[item.id].shopItem = item;
        }
    }
}
