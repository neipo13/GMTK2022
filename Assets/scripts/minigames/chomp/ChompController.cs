using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ChompController : MonoBehaviour
{
    SpriteRenderer sprite;

    public Sprite OpenSprite;
    public Sprite MissSprite;

    public Transform chompSpot;

    public bool gotEm = false;
    public LayerMask chompSpotMask;

    public DialogueManager dialogueManager;
    public SceneChanger changer;

    private Dialogue winDialogue;

    public SpringDataVec2 chompSpring;

    public Vector2 goalScale;

    public Vector2 chompScale;

    public AudioSource source;
    public AudioClip clip;

    public float vol;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (dialogueManager == null) dialogueManager = FindObjectOfType<DialogueManager>();


        winDialogue = new Dialogue();
        winDialogue.lines = new DialogueLine[2];
        winDialogue.lines[0] = new DialogueLine();
        winDialogue.lines[0].text = "OUCH! Something bit me!";
        winDialogue.lines[0].name = "Dealer";
        winDialogue.lines[0].Option1 = new OptionLine();
        winDialogue.lines[0].Option1.Text = ">";
        winDialogue.lines[0].Option1.OnChoice = new UnityEngine.Events.UnityEvent();

        winDialogue.lines[1] = new DialogueLine();
        winDialogue.lines[1].name = "";
        winDialogue.lines[1].text = "The dealer scurrys away holding his behind and leaving a stack of chips on the table.";
        winDialogue.lines[1].flagToSet = EventFlag.DealerGone;
        winDialogue.lines[1].removesItem = InventoryFlag.Dentures;
        winDialogue.lines[1].Option2 = new OptionLine();
        winDialogue.lines[1].Option2.Text = "Yoink";
        winDialogue.lines[1].Option2.OnChoice = new UnityEngine.Events.UnityEvent();
        winDialogue.lines[1].Option2.OnChoice.AddListener(() => changer.ChangeScene("OverworldScene"));

        chompSpring.current = goalScale;
        chompSpring.goal = goalScale;
    }

    void Update()
    {
        chompSpring.Update(Time.deltaTime);
        gameObject.transform.localScale = new Vector3(chompSpring.current.x, chompSpring.current.y, gameObject.transform.localScale.z);
        if (gotEm) transform.position = chompSpot.position;

        sprite.flipX = transform.position.x < 0f;
    }

    private void OnMouseDrag()
    {
        if (gotEm) return;
        sprite.sprite = OpenSprite;
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    private void OnMouseUp()
    {
        source.clip = clip;
        source.volume = vol;
        source.Play();
        chompSpring.current = chompScale;
        //try chomp
        var hit = Physics2D.OverlapCircle(transform.position, 0.2f, chompSpotMask);
        if(hit != null)
        {
            gotEm = true;
            chompSpot = hit.transform;
            dialogueManager.StartDialogue(winDialogue);
        }
        else
        {
            sprite.sprite = MissSprite;
        }
    }
}
