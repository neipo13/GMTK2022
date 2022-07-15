using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	private RectTransform rectTransform; 
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public Image nextImg;

	public float OnScreenY = -140;
	public float OffScreenY = 900;

	public Utils.SpringData positionSpring;

	private Dialogue currentDialogue;

	private Queue<DialogueLine> sentences;

	public DataManager dataManager;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<DialogueLine>();
		rectTransform = GetComponent<RectTransform>();

		positionSpring = new Utils.SpringData();
		positionSpring.goal = OffScreenY;
		positionSpring.damping = 0.6f;
		positionSpring.frequency = 20f;
	}

    private void Update()
    {
		positionSpring.current = rectTransform.anchoredPosition.y;
		positionSpring.Update(Time.deltaTime);
		rectTransform.anchoredPosition =  new Vector3(rectTransform.anchoredPosition.x, positionSpring.current);
    }

	public bool CheckDialoguePlaying(Dialogue dialogue)
    {
		if (dialogue == currentDialogue) return true;
		return false;
    }

    public void StartDialogue(Dialogue dialogue)
	{
		currentDialogue = dialogue;
		positionSpring.goal = OnScreenY;

		nameText.text = dialogue.name;
		nextImg.enabled = false;
		sentences.Clear();
		dialogueText.text = "";

		foreach (var sentence in dialogue.FilteredLines)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		nextImg.enabled = false;
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		var sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(DialogueLine line)
	{
		dialogueText.text = "";
		foreach (char letter in line.text.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
		nextImg.enabled = true;
		if (line.flagToSet != EventFlag.None) dataManager.SetEventComplete(line.flagToSet);
	}

	void EndDialogue()
	{
		positionSpring.goal = OffScreenY;
		currentDialogue = null;
	}

}