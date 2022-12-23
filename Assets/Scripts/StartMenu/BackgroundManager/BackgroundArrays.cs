using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BackgroundArrays
{
    public static Dictionary<string, Sprite[]> backgroundDictionary = new Dictionary<string, Sprite[]>();
    public static Sprite[] backgroundArrayDefault = new Sprite[5];
    public static Sprite[] backgroundArrayDesert = new Sprite[5];
    public static readonly string Desert = "Desert";
    //public static Sprite[] backgroundArrayGar = new Sprite[5];

    //init as GO and skip part so set sprite? 
    public static void Start()
    {
        backgroundArrayDesert[0] = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_0");
        backgroundArrayDesert[1] = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_1");
        backgroundArrayDesert[2] = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_2");
        backgroundArrayDesert[3] = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_3");
        backgroundArrayDesert[4] = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_4");
        backgroundDictionary.Add(BackgroundArrays.Desert, backgroundArrayDesert);
    }
}
