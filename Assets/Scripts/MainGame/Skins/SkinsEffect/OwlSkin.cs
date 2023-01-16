using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlSkin : SkinEffect
{
    public static int skinMulti = 1200;
    public static float critChance = 0.05f;
    public static int critMulti = 90;
    public override string id { get; set; } = SkinNames.Owl;
    public override int price { get; set; } = 20000;
    public override string description { get; set; } = "Süße Eule im dunklen Wald\n" +
                                                        "Skin Multiplikator: " + skinMulti +
                                                        "\nKrit. Treffer Chance: " + critChance +
                                                        "\nKrit. Treffer Multi: " + critMulti;
    public override bool bought { get; set; } = false;
    public override string rarity { get; set; } = Rarities.Mythical;
    public override int multiplierOfSkin { get; set; } = skinMulti;
    public override float criticalChance { get; set; } = critChance;
    public override float criticalMultiplier { get; set; } = critMulti;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("ButtonSkins/owl");
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
        ContentDistributor.contentDistributor.mainButton.multiplierOfSkin = multiplierOfSkin;
        DummyButton.criticalChance = criticalChance;
        DummyButton.criticalMultiplier = criticalMultiplier;
    }

    public override void EquipSkin()
    {
        Account.ActiveSkin = this;
        EffectOfSkin();
        ContentDistributor.contentDistributor.mainButton.SetSkin(icon);
        ContentDistributor.contentDistributor.parallax.SwitchBackground(BackgroundArrays.FoggyMountain);
    }

    public override void UnequipSkin()
    {
        ContentDistributor.contentDistributor.mainButton.RemoveSkinMultiplier();
        ContentDistributor.contentDistributor.mainButton.RemoveCriticalChance(criticalChance);
        ContentDistributor.contentDistributor.mainButton.RemoveCriticalMultiplier(criticalMultiplier);
    }
}
