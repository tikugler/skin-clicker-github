using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestSkinTwo : SkinEffect
{

    public static int skinMulti = 20;
    public static float critChance = 0.02f;
    public static int critMulti = 90;
    public override string id { get; set; } = SkinNames.TestEffectTwo;
    public override int price { get; set; } = 4;
    public override string description { get; set; } = "Low Crit-Chance, high reward, random color, much wow\nSkin Multiplicator: " + skinMulti + "\nCritical Chance: " + critChance + "\nCritical Multiplicator: " + critMulti;
    public override bool bought { get; set; } = false;
    public override string rarity { get; set; } = Rarities.Rare;
    public override int multiplicatorOfSkin { get; set; } = skinMulti;
    public override float criticalChance { get; set; } = critChance;
    public override float criticalMultiplicator { get; set; } = critMulti;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("BerkanF.Nur");
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
        Parallax.SwitchBackground(BackgroundArrays.Snow);
        ContentDistributor.contentDistributor.mainButton.SetSkin(icon);
        Account.ActiveSkin = this;
        EffectOfSkin();
    }
}

