using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestSkinTwo : SkinEffect
{

    //public new string id = SkinNames.TestEffectTwo;
    public override string id { get; set; } = SkinNames.TestEffectTwo;
    public override int price { get; set; } = 4;
    public override string description { get; set; } = "TestSkinTwo with extra random effects";
    public override bool bought { get; set; } = false;
    public override string rarity { get; set; } = Rarities.Rare;
    public override int multiplicatorOfSkin { get; set; } = 20;
    public override float criticalChance { get; set; } = 0.02f;
    public override float criticalMultiplicator { get; set; } = 90;
    public override SkinTemplate skinTemplate { get; set; }
    private GameObject mainButton;

    public override void PurchaseButtonAction(SkinTemplate skinTemplate)
    {
        this.skinTemplate = skinTemplate;
        Account.skinList.Add(this);
        bought = true;
    }

    public override void EffectOfSkin()
    {
        ContentDistributor.contentDistributor.mainButton.multiplicatorOfSkin = multiplicatorOfSkin;
        DummyButton.criticalChance = criticalChance;
        DummyButton.criticalMultiplicator = criticalMultiplicator;

        GameObject canvasGameObject = GameObject.Find("Canvas");
        mainButton = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "MainButton");
        mainButton.GetComponent<Image>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public override void EquipSkin()
    {
        Account.activeSkin = this;
        EffectOfSkin();
    }
}

