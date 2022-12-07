using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkin : SkinEffect
{
    public new int skinMultiplicator = 2;
    public override void PurchaseButtonAction(SkinTemplate skinTemplate)
    {
        this.skinTemplate = skinTemplate;
    }

    public override void EffectOfSkin()
    {

    }

    public override void EquipSkin()
    {

    }
}
