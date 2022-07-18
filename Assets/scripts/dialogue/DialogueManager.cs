using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Linq;

public class DialogueManager : MonoBehaviour
{

    private RectTransform rectTransform;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public GameObject Option1;
    public GameObject Option2;
    public Button option1Button;
    public Button option2Button;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;

    public float OnScreenY = -140;
    public float OffScreenY = 900;

    public Utils.SpringData positionSpring;

    private Dialogue currentDialogue;
    private DialogueLine currentLine;
    public CameraZoom zoom;

    private Queue<DialogueLine> sentences;
    public AudioSource audioSource;
    public AudioClip[] soundClips;

    public float pitchRange = 0.2f;

    private char[] charactersForSounds = new char[5] { 'a', 'e', 'i', 'o', 'u' };

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<DialogueLine>();
        rectTransform = GetComponent<RectTransform>();
        if (zoom == null) zoom = FindObjectOfType<CameraZoom>();

        positionSpring = new Utils.SpringData();
        positionSpring.goal = OffScreenY;
        positionSpring.damping = 0.6f;
        positionSpring.frequency = 20f;
    }

    private void Update()
    {
        positionSpring.current = rectTransform.anchoredPosition.y;
        positionSpring.Update(Time.deltaTime);
        rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, positionSpring.current);
    }

    public bool CheckDialoguePlaying(Dialogue dialogue)
    {
        if (dialogue == currentDialogue) return true;
        return false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        zoom?.Zoom();
        currentDialogue = dialogue;
        positionSpring.goal = OnScreenY;

        sentences.Clear();
        dialogueText.text = "";

        foreach (var sentence in dialogue.FilteredLines)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void PlayerHitNext()
    {
        if (currentLine == null) return;
        if (!string.IsNullOrWhiteSpace(currentLine.Option1?.Text) || !string.IsNullOrWhiteSpace(currentLine.Option2?.Text))
        {
            return;
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

        currentLine = sentences.Dequeue();
        if (currentLine.Option1 != null && !string.IsNullOrWhiteSpace(currentLine.Option1.Text))
        {
            option1Button.onClick?.RemoveAllListeners();
            Option1.SetActive(true);
            option1Text.SetText(currentLine.Option1.Text);
            option1Button.onClick?.AddListener(() => OptionClicked(currentLine.Option1.OnChoice));
        }
        else
        {
            Option1.SetActive(false);
        }
        if (currentLine.Option2 != null && !string.IsNullOrWhiteSpace(currentLine.Option2.Text))
        {
            option2Button.onClick?.RemoveAllListeners();
            Option2.SetActive(true);
            option2Text.SetText(currentLine.Option2.Text);
            option2Button.onClick?.AddListener(() => OptionClicked(currentLine.Option2.OnChoice));
        }
        else
        {
            Option2.SetActive(false);
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    void OptionClicked(UnityEvent ue)
    {
        ue?.Invoke();
        DisplayNextSentence();
    }

    IEnumerator TypeSentence(DialogueLine line)
    {
        nameText.text = line.name;
        dialogueText.text = "";
        foreach (char letter in line.text.ToCharArray())
        {
            if (charactersForSounds.Contains(Char.ToLower(letter)) && !audioSource.isPlaying)
            {
                if (line.defaultPitch == 0f) line.defaultPitch = 1f;//hack fix for lots of missing pitches
                audioSource.pitch = UnityEngine.Random.Range(line.defaultPitch - pitchRange, line.defaultPitch + pitchRange);
                audioSource.clip = soundClips[UnityEngine.Random.Range(0, soundClips.Length)];
                audioSource.Play();
            }
            dialogueText.text += letter;
            yield return null;
        }
        if (line.givesItem != InventoryFlag.None) DataHolder.SetItemObtained(line.givesItem);
        if (line.removesItem != InventoryFlag.None) DataHolder.RemoveObtainedItem(line.removesItem);
        if (line.flagToSet != EventFlag.None) DataHolder.SetEventComplete(line.flagToSet);
    }

    public void EndDialogue()
    {
        currentLine = null;
        positionSpring.goal = OffScreenY;
        currentDialogue = null;
        zoom?.Normal();
    }

}