using System.Collections;
using System.Collections.Generic;
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
    public ItemInventoryManager itemInventoryManager;
    //public SkinManager itemInventoryManager;
    public DummyButton mainButton;
    public ItemTemplate[] scriptableObjectItems;
    public Dictionary<string, ItemEffect> itemsDictionary = new Dictionary<string, ItemEffect>();

    //Player stuff for demo
    public ArrayList boughtItemsOfPlayer = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        if (contentDistributor == null) {
            contentDistributor = this;
            CreateItems();
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
}
