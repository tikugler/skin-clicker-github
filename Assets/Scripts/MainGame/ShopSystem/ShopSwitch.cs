using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSwitch : MonoBehaviour
{
    public GameObject skinsShop;
    public GameObject itemsShop;
    public GameObject skinsButton;
    public GameObject itemsButton;

    void Start()
    {
        ItemsButtonAction();
    }

    public void SkinsButtonAction()
    {
        itemsShop.SetActive(false);
        skinsShop.SetActive(true);
        itemsButton.GetComponent<Image>().color = Color.white;
        skinsButton.GetComponent<Image>().color = Color.red;
    }

    public void ItemsButtonAction()
    {
        itemsShop.SetActive(true);
        skinsShop.SetActive(false);
        skinsButton.GetComponent<Image>().color = Color.white;
        itemsButton.GetComponent<Image>().color = Color.red;
    }
}
