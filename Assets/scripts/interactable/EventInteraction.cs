using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInteraction : MonoBehaviour, IInteractable
{
    public DialogueManager DialogueManager;
    public Dialogue dialogue;
    public PlayerController playerController;

    public string InteractionText => "";

    public void EnteredInteractionArea()
    {
    }

    public void ExitInteractionArea()
    {
    }

    public void Interact()
    {
        if (DialogueManager == null) DialogueManager = FindObjectOfType<DialogueManager>();
        if (DialogueManager.CheckDialoguePlaying(dialogue))
        {
            DialogueManager.DisplayNextSentence();
        }
        else
        {
            DialogueManager.StartDialogue(dialogue);
        }
    }

    public void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (DialogueManager == null) DialogueManager = FindObjectOfType<DialogueManager>();
    }
}
