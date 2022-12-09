using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestSkinTwo : SkinEffect
{

    public new string id = SkinNames.TestEffectTwo;
    public new int price = 4000;
    public new bool bought = false;
    public new string rarity = Rarities.Rare;
    public new int multiplicatorOfSkin = 20;
    public new float criticalChance = 0.02f;
    public new float criticalMultiplicator = 90;
    private GameObject mainButton;

    public override void PurchaseButtonAction(SkinTemplate skinTemplate)
    {
        this.skinTemplate = skinTemplate;
        Account.skinList.Add(this);
    }

    public override void EffectOfSkin()
    {
        ContentDistributor.contentDistributor.mainButton.multiplicatorOfSkin = multiplicatorOfSkin;
        DummyButton.criticalChance = criticalChance;
        DummyButton.criticalMultiplicator = criticalMultiplicator;

        GameObject canvasGameObject = GameObject.Find("Canvas");
        mainButton = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "MainButton");
        Waiter();
    }

    public override void EquipSkin()
    {
        Account.activeSkin = this;
        EffectOfSkin();
    }

    IEnumerator Waiter()
    {
        while (Account.activeSkin == this)
        {
            yield return new WaitForSeconds(1);
            mainButton.GetComponent<Image>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}

