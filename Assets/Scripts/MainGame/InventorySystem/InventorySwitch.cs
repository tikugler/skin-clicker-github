using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySwitch : MonoBehaviour
{
    public GameObject skinsInventory;
    public GameObject itemsIntentory;
    public GameObject skinsButton;
    public GameObject itemsButton;

    void Start()
    {
        ItemsButtonAction();
    }

    public void SkinsButtonAction()
    {
        itemsIntentory.SetActive(false);
        skinsInventory.SetActive(true);
        itemsButton.GetComponent<Image>().color = Color.white;
        skinsButton.GetComponent<Image>().color = Color.red;
    }

    public void ItemsButtonAction()
    {
        skinsInventory.SetActive(false);
        itemsIntentory.SetActive(true);
        skinsButton.GetComponent<Image>().color = Color.white;
        itemsButton.GetComponent<Image>().color = Color.red;
    }
}
