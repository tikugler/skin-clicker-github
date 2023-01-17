using UnityEngine;

/// <summary>
/// Diese Klasse speichert, welche Attribute (Text) ein Dialog haben sollte.
/// </summary>
[System.Serializable]
public class Dialogue
{

	public string name;

	[TextArea(3, 10)]
	public string[] sentences;

}