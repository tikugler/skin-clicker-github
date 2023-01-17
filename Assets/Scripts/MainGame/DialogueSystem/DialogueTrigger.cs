using UnityEngine;

/// <summary>
/// Diese Klasse schaut, ob ein Dialog ausgelöst werden soll.
/// </summary>
public class DialogueTrigger : MonoBehaviour
{

	public Dialogue dialogue;

	private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    /// <summary>
    /// Startet einen bestimmten Dialog
    /// </summary>
    public void TriggerDialogue()
	{
		dialogueManager.StartDialogue(dialogue);
	}
    /// <summary>
    /// Schaut, ob der Dialog fertig ist oder nicht.
    /// </summary>
    /// <returns>True, falls der Dialog zu ende ist.</returns>
    public bool HasDialogueEnded()
    {
        return dialogueManager.HasDialogueEnded();
    }
}