using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPackage : PackageEffect
{
    public override string id { get; set; } = PackageNames.Package3;
    public override int price { get; set; } = 100;
    public override int realMoneyAmount { get; set; } = 1250;
    public override int creditsAmount { get; set; } = 9999;
    public override string rarity { get; set; } = Rarities.Legendary;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("Packages/BigPackage");
    public override PackageTemplate packageTemplate { get; set; }

 

}
