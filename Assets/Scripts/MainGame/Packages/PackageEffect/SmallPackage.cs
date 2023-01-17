using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPackage : PackageEffect
{
    public override string id { get; set; } = PackageNames.Package1;
    public override int price { get; set; } = 1;
    public override int realMoneyAmount { get; set; } = 10;
    public override int creditsAmount { get; set; } = 99;
    public override string rarity { get; set; } = Rarities.Common;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("Packages/SmallPackage");
    public override PackageTemplate packageTemplate { get; set; }
}
