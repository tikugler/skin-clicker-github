using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkin : SkinEffect
{
    public new int skinMultiplicator = 2;
    public float criticalChance = 0.3f;
    public override void PurchaseButtonAction(SkinTemplate skinTemplate)
    {
        this.skinTemplate = skinTemplate;
        ContentDistributor.contentDistributor.boughtSkinsOfPlayer.Add(this);
    }

    public override void EffectOfSkin()
    {
        ContentDistributor.contentDistributor.mainButton.skinMultiplicator = skinMultiplicator;
        DummyButton.criticalChance = criticalChance;
    }

    public override void EquipSkin()
    {
        Account.activeSkin = this;
        EffectOfSkin();
    }
}
