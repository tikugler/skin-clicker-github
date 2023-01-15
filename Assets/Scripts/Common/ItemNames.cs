using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is an alternative to a enum class. C# enums only support integers means no strings.
/// So this workaround is better and simpler than adding methods to a enum class.
/// The Value of the string is the title of an item.
/// </summary>
public static class ItemNames
{
    public static readonly string DoubleEffect = "Double Effect";
    public static readonly string Worker = "Worker";
    public static readonly string TestEffect = "Test Effect";
    public static readonly string CriticalHitEffect = "Critical Hit";
}