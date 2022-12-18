using System;
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
    public SkinInventoryManager skinInventoryManager;
    public DummyButton mainButton;
    public ItemTemplate[] scriptableObjectItems;
    public SkinTemplate[] scriptableObjectSkins;
    public Dictionary<string, ItemEffect> itemsDictionary = new Dictionary<string, ItemEffect>();
    public Dictionary<string, SkinEffect> skinsDictionary = new Dictionary<string, SkinEffect>();

    //Player stuff for demo
    public ArrayList boughtItemsOfPlayer = new ArrayList();


    public static Action<ItemTemplate[]> AddItemsToProfilInfo = delegate { };
    public static Action<SkinTemplate[]> AddSkinsToProfilInfo = delegate { };


    // make sure that SetUpgrade and LoadPurchasedSkins is called before ShopManager.RefreshPanels
    // Awake is called before the application starts.
    private void Awake()
    {
        if (contentDistributor == null)
        {
            contentDistributor = this;
            CreateItems();
            CreateSkins();
            AddItemsAndSkinsToProfileInfoPanel();
            SetUpgrades(); // sets number of performed upgrade / bought item previously
            LoadPurchasedSkins();
            CollectOfflineCreditsManager.StartCollectOfflineCreditsManagerStatic();
        }
    }

    private void AddItemsAndSkinsToProfileInfoPanel()
    {
        if (!UserInfoManager.isUserInfoPanelFilled)
        {
            AddItemsToProfilInfo?.Invoke(scriptableObjectItems);
            AddSkinsToProfilInfo?.Invoke(scriptableObjectSkins);
        }
    }


    /* 
    *  Creats and adds ItemEffects to key-value-pair.
    *  Adds ItemEffect to template.
    */
    private void CreateItems()
    {
        var doubleEffect = new DoubleEffect();
        var doubleEffectTemplate = CreateItemTemplate(doubleEffect);
        itemsDictionary.Add(doubleEffect.id.ToString(), doubleEffect);

        var testEffect = new TestEffect();
        var testEffectTemplate = CreateItemTemplate(testEffect);
        itemsDictionary.Add(testEffect.id.ToString(), testEffect);

        var worker = new Worker();
        var workerTemplate = CreateItemTemplate(worker);
        itemsDictionary.Add(worker.id.ToString(), worker);

        scriptableObjectItems = new ItemTemplate[3];
        scriptableObjectItems[0] = doubleEffectTemplate;
        scriptableObjectItems[1] = testEffectTemplate;
        scriptableObjectItems[2] = workerTemplate;

    }

    /* 
    *  Creats and adds SkinEffects to key-value-pair.
    *  Adds SkinEffect to template.
    */
    private void CreateSkins()
    {
        var testSkin = new TestSkin();
        var testSkinTemplate = CreateSkinTemplate(testSkin);
        skinsDictionary.Add(testSkin.id.ToString(), testSkin);


        var testSkinTwo = new TestSkinTwo();
        var testSkinTemplate2 = CreateSkinTemplate(testSkinTwo);
        skinsDictionary.Add(testSkinTwo.id.ToString(), testSkinTwo);

        scriptableObjectSkins = new SkinTemplate[2];
        scriptableObjectSkins[0] = testSkinTemplate;
        scriptableObjectSkins[1] = testSkinTemplate2;

    }

    /* 
    *  Creates new SkinTemplate and fills fields with item values.
    */
    private SkinTemplate CreateSkinTemplate(SkinEffect skin)
    {
        SkinTemplate skinTemplate = SkinTemplate.CreateInstance<SkinTemplate>();
        skinTemplate.id = skin.id;
        skinTemplate.title = skin.id;
        skinTemplate.description = skin.description;
        skinTemplate.rarity = skin.rarity;
        skinTemplate.price = skin.price;
        skinTemplate.startPrice = skin.price;
        skinTemplate.icon = skin.icon;
        skinTemplate.fullPicture = null;
        skin.skinTemplate = skinTemplate;

        return skinTemplate;
    }

    /* 
    *  Creates new ItemTemplate and fills fields with item values.
    */
    private ItemTemplate CreateItemTemplate(ItemEffect item)
    {
        ItemTemplate itemTemplate = ItemTemplate.CreateInstance<ItemTemplate>();
        itemTemplate.id = item.id;
        itemTemplate.title = item.id;
        itemTemplate.description = item.description;
        itemTemplate.rarity = item.rarity;
        itemTemplate.price = item.price;
        itemTemplate.startPrice = item.price;
        itemTemplate.icon = item.icon;
        item.shopItem = itemTemplate;

        return itemTemplate;
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

    /// <summary>
    /// loads all of the skins which user bougtht
    /// previously
    /// </summary>
    private void LoadPurchasedSkins()
    {
        foreach (SkinTemplate skin in scriptableObjectSkins)
        {
            if (Account.IsSkinIdInSkinIdList(skin.id)){
                skinsDictionary[skin.id].PurchaseButtonAction(skin);
                if (Account.activeSkinId.Equals(skin.id))
                {
                    skinsDictionary[skin.id].EquipSkin();
                }
            }
        }
    }
}
