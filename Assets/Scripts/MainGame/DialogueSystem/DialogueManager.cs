using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;

	private DialogueSelector dialogueSelector;
	public Animator animator;

	private Queue<string> sentences;

	private bool dialogueEnded;

    // Use this for initialization
    void Start()
	{
		dialogueEnded = false;
		sentences = new Queue<string>();
		dialogueSelector = GetComponent<DialogueSelector>();
		dialogueSelector.enabled = true;
	}

	public void StartDialogue(Dialogue dialogue)
	{
		dialogueEnded = false;
		animator.transform.gameObject.SetActive(true);
		animator.SetBool("IsOpen", true);
		
		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		dialogueEnded = true;
	}

	public bool HasDialogueEnded()
    {
		return dialogueEnded;
    }
}