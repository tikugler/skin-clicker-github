using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

	public Dialogue dialogue;

	private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    public void TriggerDialogue()
	{
		dialogueManager.StartDialogue(dialogue);
	}

    public bool HasDialogueEnded()
    {
        return dialogueManager.HasDialogueEnded();
    }
}