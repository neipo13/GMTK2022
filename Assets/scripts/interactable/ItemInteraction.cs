using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour, IInteractable
{
    public string InteractionText { get; set; } = "Inspect";
    public DialogueManager DialogueManager;
    public Dialogue dialogue;
    public PlayerController playerController;

    public void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }


    public void EnteredInteractionArea()
    {
        playerController.SetInteractable(this);
    }

    public void ExitInteractionArea()
    {
        playerController.UnsetInteractable();
    }

    public void Interact()
    {
        if (DialogueManager.CheckDialoguePlaying(dialogue))
        {
            DialogueManager.DisplayNextSentence();
        }
        else
        {
            DialogueManager.StartDialogue(dialogue);
        }
    }

    void OnTriggerEnter2D()
    {
        EnteredInteractionArea();
    }

    void OnTriggerExit2D()
    {
        ExitInteractionArea();
    }
}
