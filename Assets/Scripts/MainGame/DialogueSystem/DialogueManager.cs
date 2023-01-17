using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Der DialogueManager verwaltet, dass ein Dialog richtig abgespielt wird
/// und danach der Dialog auch richtig beendet wird.
/// </summary>
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

	/// <summary>
	/// Diese Methode startet den Dialog, der gegeben ist.
	/// </summary>
	/// <param name="dialogue">Dialog, der abgespielt werden soll</param>
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
	/// <summary>
	/// Zeigt den nächsten Satz an, wenn es einen nächsten Satz gibt.
	/// </summary>
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
	/// <summary>
	/// Schreibt den Satz Buchstabe für Buchstabe langsam.
	/// </summary>
	/// <param name="sentence">Satz zum anzeigen</param>
	/// <returns>null</returns>
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

	/// <summary>
	/// Schaut, ob der Dialog zu ende ist.
	/// </summary>
	/// <returns>true, falls dialog zu ende ist.</returns>
	public bool HasDialogueEnded()
    {
		return dialogueEnded;
    }
}