using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to share important objects with other classes.
/// There is only one (static) contentDistributor.
/// </summary>
public class ContentDistributor : MonoBehaviour
{
    public static ContentDistributor contentDistributor;
    public string id;
    public ShopManager shopManager;
    public ShopSkinManager shopSkinManager;
    public ItemInventoryManager itemInventoryManager;
    public SkinInventoryManager skinInventoryManager;
    public DummyButton mainButton;
    public Parallax parallax;
    public ItemTemplate[] scriptableObjectItems;
    public SkinTemplate[] scriptableObjectSkins;
    public PackageTemplate[] scriptableObjectPackages;
    public Dictionary<string, ItemEffect> itemsDictionary = new Dictionary<string, ItemEffect>();
    public Dictionary<string, SkinEffect> skinsDictionary = new Dictionary<string, SkinEffect>();
    public Dictionary<string, PackageEffect> packagesDictionary = new Dictionary<string, PackageEffect>();


    //Player stuff for demo
    public ArrayList boughtItemsOfPlayer = new ArrayList();


    public static Action<ItemTemplate[]> AddItemsToProfilInfo = delegate { };
    public static Action<SkinTemplate[]> AddSkinsToProfilInfo = delegate { };

    /// <summary>
    /// make sure that SetUpgrade and LoadPurchasedSkins is called before ShopManager.RefreshPanels
    /// Awake is called before the application starts.
    /// </summary>
    private void Awake()
    {
        if (contentDistributor == null)
        {
            contentDistributor = this;
            CreateItems();
            CreateSkins();
            CreatePackages();

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


    /// <summary>
    /// Adds new items to scriptableObjectItems.
    /// Creates templates for shop/inventory.
    /// </summary>
    private void CreateItems()
    {
        var doubleEffect = new DoubleEffect();
        var doubleEffectTemplate = CreateItemTemplate(doubleEffect);

        var testEffect = new TestEffect();
        var testEffectTemplate = CreateItemTemplate(testEffect);

        var worker = new Worker();
        var workerTemplate = CreateItemTemplate(worker);

        var criticalHit = new CriticalHitEffect();
        var criticalHitTemplate = CreateItemTemplate(criticalHit);

        scriptableObjectItems = new ItemTemplate[4];
        scriptableObjectItems[0] = doubleEffectTemplate;
        scriptableObjectItems[1] = testEffectTemplate;
        scriptableObjectItems[2] = workerTemplate;
        scriptableObjectItems[3] = criticalHitTemplate;

    }

    /// <summary>
    /// Adds new items to scriptableObjectItems.
    /// Creates templates for shop/inventory.
    /// </summary>
    private void CreateSkins()
    {
        var defaultSkin = new DefaultSkin();
        var defaultSkinTemplate = CreateSkinTemplate(defaultSkin);

        var snowmanSkin = new SnowmanSkin();
        var snowmanSkinTemplate = CreateSkinTemplate(snowmanSkin);

        var catSkin = new CatSkin();
        var catSkinTemplate = CreateSkinTemplate(catSkin);

        var cactusSkin = new CactusSkin();
        var cactusSkinTemplate = CreateSkinTemplate(cactusSkin);

        var owlSkin = new OwlSkin();
        var owlSkinTemplate = CreateSkinTemplate(owlSkin);

        var testSkin = new TestSkin();
        var testSkinTemplate = CreateSkinTemplate(testSkin);

        var testSkinTwo = new TestSkinTwo();
        var testSkinTemplate2 = CreateSkinTemplate(testSkinTwo);

        scriptableObjectSkins = new SkinTemplate[7];
        scriptableObjectSkins[0] = defaultSkinTemplate;
        scriptableObjectSkins[1] = snowmanSkinTemplate;
        scriptableObjectSkins[2] = catSkinTemplate;
        scriptableObjectSkins[3] = cactusSkinTemplate;
        scriptableObjectSkins[4] = owlSkinTemplate;
        scriptableObjectSkins[5] = testSkinTemplate;
        scriptableObjectSkins[6] = testSkinTemplate2;

    }

    /// <summary>
    /// Adds new items to scriptableObjectPackages.
    /// Creates templates for shop/inventory.
    /// </summary>
    private void CreatePackages()
    {
        var smallPackage = new SmallPackage();
        var packageSmallTemplate = CreatePackageTemplate(smallPackage);

        var mediumPackage = new MediumPackage();
        var packageMediumTemplate = CreatePackageTemplate(mediumPackage);

        var bigPackage = new BigPackage();
        var packageBigTemplate = CreatePackageTemplate(bigPackage);

        scriptableObjectPackages = new PackageTemplate[3];
        scriptableObjectPackages[0] = packageSmallTemplate;
        scriptableObjectPackages[1] = packageMediumTemplate;
        scriptableObjectPackages[2] = packageBigTemplate;




    }

    /// <summary>
    /// Creates new SkinTemplate and fills fields with item values.
    /// </summary>
    /// <param name="package"></param>
    /// <returns>SkinTemplate with values of the skin</returns>
    private PackageTemplate CreatePackageTemplate(PackageEffect package)
    {
        PackageTemplate packageTemplate = PackageTemplate.CreateInstance<PackageTemplate>();
        packageTemplate.id = package.id;
        packageTemplate.title = package.id;
        packageTemplate.description = package.GetDescription();
        packageTemplate.price = package.price;
        packageTemplate.startPrice = package.price;
        packageTemplate.icon = package.icon;
        packageTemplate.fullPicture = null;
        package.packageTemplate = packageTemplate;

        packagesDictionary.Add(package.id.ToString(), package);

        return packageTemplate;
    }

    /// <summary>
    /// Creates new SkinTemplate and fills fields with item values.
    /// </summary>
    /// <param name="skin"></param>
    /// <returns>SkinTemplate with values of the skin</returns>
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

        skinsDictionary.Add(skin.id.ToString(), skin);

        return skinTemplate;
    }

    /// <summary>
    /// Creates new ItemTemplate and fills fields with item values.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>ItemTemplate with values of the item</returns>
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

        itemsDictionary.Add(item.id.ToString(), item);

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
            if (Account.IsSkinIdInSkinIdList(skin.id))
            {
                skinsDictionary[skin.id].PurchaseButtonAction(skin);
                if (Account.activeSkinId.Equals(skin.id))
                {
                    skinsDictionary[skin.id].EquipSkin();
                }
            }
        }
    }
}
