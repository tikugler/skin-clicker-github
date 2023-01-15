public static class Rarities
{
    /// <summary>
    /// This is an alternative to a enum class. C# enums only support integers means no strings.
    /// So this workaround is better and simpler than adding methods to a enum class.
    /// The Value of the string is the rarity of an item/skin.
    /// </summary>
    public static readonly string Common = "Common";
    public static readonly string Uncommon = "Uncommon";
    public static readonly string Rare = "Rare";
    public static readonly string Mythical = "Mythical";
    public static readonly string Legendary = "Legendary";
}