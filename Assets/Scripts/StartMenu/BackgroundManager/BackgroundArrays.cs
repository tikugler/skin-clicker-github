using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BackgroundArrays
{
    public static Dictionary<string, Sprite[]> backgroundDictionary = new Dictionary<string, Sprite[]>();
    public static Sprite[] backgroundArrayDefault = new Sprite[5];
    public static Sprite[] backgroundArrayDesert = new Sprite[5];
    public static Sprite[] backgroundArrayGraveyard = new Sprite[5];
    public static Sprite[] backgroundArraySnow = new Sprite[5];
    public static readonly string Default = "Default";
    public static readonly string Desert = "Desert";
    public static readonly string Graveyard = "Graveyard";
    public static readonly string Snow = "Snow";
    public static bool itemsCreated = false;
    //public static Sprite[] backgroundArrayGar = new Sprite[5];

    //init as GO and skip part so set sprite? 
    public static void Start()
    {
        if (!itemsCreated)
        {
            backgroundArrayDefault[0] = Resources.Load<Sprite>("Backgtounds/!_Moutain/Layer_0");
            backgroundArrayDefault[1] = Resources.Load<Sprite>("Backgtounds/!_Moutain/Layer_1");
            backgroundArrayDefault[2] = Resources.Load<Sprite>("Backgtounds/!_Moutain/Layer_2");
            backgroundArrayDefault[3] = Resources.Load<Sprite>("Backgtounds/!_Moutain/Layer_3");
            backgroundArrayDefault[4] = Resources.Load<Sprite>("Backgtounds/!_Moutain/Layer_4");
            backgroundDictionary.Add(BackgroundArrays.Default, backgroundArrayDefault);

            backgroundArrayDesert[0] = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_0");
            backgroundArrayDesert[1] = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_1");
            backgroundArrayDesert[2] = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_2");
            backgroundArrayDesert[3] = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_3");
            backgroundArrayDesert[4] = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_4");
            backgroundDictionary.Add(BackgroundArrays.Desert, backgroundArrayDesert);

            backgroundArrayGraveyard[0] = Resources.Load<Sprite>("Backgtounds/3_Graveyard/Layer_0");
            backgroundArrayGraveyard[1] = Resources.Load<Sprite>("Backgtounds/3_Graveyard/Layer_1");
            backgroundArrayGraveyard[2] = Resources.Load<Sprite>("Backgtounds/3_Graveyard/Layer_2");
            backgroundArrayGraveyard[3] = Resources.Load<Sprite>("Backgtounds/3_Graveyard/Layer_3");
            backgroundArrayGraveyard[4] = Resources.Load<Sprite>("Backgtounds/3_Graveyard/Layer_4");
            backgroundDictionary.Add(BackgroundArrays.Graveyard, backgroundArrayGraveyard);

            backgroundArraySnow[0] = Resources.Load<Sprite>("Backgtounds/4_Snow/Layer_0");
            backgroundArraySnow[1] = Resources.Load<Sprite>("Backgtounds/4_Snow/Layer_1");
            backgroundArraySnow[2] = Resources.Load<Sprite>("Backgtounds/4_Snow/Layer_2");
            backgroundArraySnow[3] = Resources.Load<Sprite>("Backgtounds/4_Snow/Layer_3");
            backgroundArraySnow[4] = Resources.Load<Sprite>("Backgtounds/4_Snow/Layer_4");
            backgroundDictionary.Add(BackgroundArrays.Snow, backgroundArraySnow);

            itemsCreated = true;
        }
    }
}
