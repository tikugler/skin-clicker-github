using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is used to share objects with other classes.
//There is only one (static) contentDistributor.
public class ContentDistributor : MonoBehaviour
{
    public static ContentDistributor contentDistributor;
    public string id;
    public ShopManager shopManager;
    public ItemInventoryManager itemInventoryManager;
    //public SkinManager itemInventoryManager;
    public DummyButton mainButton;
    public ItemTemplate[] scriptableObjectItems;
    public Dictionary<string, ItemEffect> itemsDictionary = new Dictionary<string, ItemEffect>();
    // Start is called before the first frame update

    void Start()
    {
        contentDistributor = this;
        CreateItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Creats and adds ItemEffects to key-value-pair.
    //The key is ALWAYS the exact class name of a item!
    public void CreateItems() {
        var doubleEffect = new DoubleEffect();
        itemsDictionary.Add(doubleEffect.id.ToString(), doubleEffect);

        var testEffect = new TestEffect();
        itemsDictionary.Add(testEffect.id.ToString(), testEffect);
    }
}
