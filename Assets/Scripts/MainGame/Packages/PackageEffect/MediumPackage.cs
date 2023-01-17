using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumPackage : PackageEffect
{
    public override string id { get; set; } = PackageNames.Package2;
    public override int price { get; set; } = 10;
    public override int realMoneyAmount { get; set; } = 110;
    public override int creditsAmount { get; set; } = 999;
    public override string rarity { get; set; } = Rarities.Rare;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("Packages/MediumPackage");
    public override PackageTemplate packageTemplate { get; set; }


}
