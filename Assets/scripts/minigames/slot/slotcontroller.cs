using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotcontroller : MonoBehaviour
{
    public draggable Left;
    public draggable Right;
    public draggable Center;

    public bool won = false;

    public DialogueManager dialogueManager;
    public SceneChanger changer;

    // Update is called once per frame
    void Update()
    {
        if( !won && !string.IsNullOrWhiteSpace(Left.CenterItemName) && !string.IsNullOrWhiteSpace(Right.CenterItemName) && !string.IsNullOrWhiteSpace(Center.CenterItemName) &&
            Left.CenterItemName == Center.CenterItemName && Center.CenterItemName == Right.CenterItemName)
        {
            won = true;
            switch (Left.CenterItemName)
            {
                default:
                    dialogueManager.StartDialogue(sevenDialogue);
                    break;
            }
        }
    }

    public Dialogue barDialogue;

    public Dialogue sevenDialogue;

    private void Start()
    {
        sevenDialogue = new Dialogue();
        sevenDialogue.lines = new DialogueLine[1];
        sevenDialogue.lines[0] = new DialogueLine();
        sevenDialogue.lines[0].text = "Gladys deserves as win";
        sevenDialogue.lines[0].flagToSet = EventFlag.GladysWon;
        sevenDialogue.lines[0].Option1 = new OptionLine();
        sevenDialogue.lines[0].Option1.Text = "Nice";
        sevenDialogue.lines[0].Option1.OnChoice = new UnityEngine.Events.UnityEvent();
        sevenDialogue.lines[0].Option1.OnChoice.AddListener(() => changer.ChangeScene("OverworldScene"));


        sevenDialogue = new Dialogue();
        sevenDialogue.lines = new DialogueLine[1];
        sevenDialogue.lines[0] = new DialogueLine();
        sevenDialogue.lines[0].text = "Gladys deserves as win";
        sevenDialogue.lines[0].flagToSet = EventFlag.GladysWon;
        sevenDialogue.lines[0].Option1 = new OptionLine();
        sevenDialogue.lines[0].Option1.Text = "Nice";
        sevenDialogue.lines[0].Option1.OnChoice = new UnityEngine.Events.UnityEvent();
        sevenDialogue.lines[0].Option1.OnChoice.AddListener(() => changer.ChangeScene("OverworldScene"));

    }
}
