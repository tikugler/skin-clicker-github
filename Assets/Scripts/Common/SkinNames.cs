using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is an alternative to a enum class. C# enums only support integers means no strings.
/// So this workaround is better and simpler than adding methods to a enum class.
/// The Value of the string is the title of a skin.
/// </summary>
public static class SkinNames
{
    public static readonly string TestEffect = "TestSkin";
    public static readonly string TestEffectTwo = "TestSkinTwo";
    public static readonly string Default = "Skin Clicker";
    public static readonly string Cactus = "Cactus";
    public static readonly string Cat = "Cat";
    public static readonly string Snowman = "Snowman";
    public static readonly string Owl = "Owl";
}