using Prime31;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;

public class PlayerController : MonoBehaviour
{
    public float moveSpeedMax = 5f;
    public float acceleration = 20f;
    private CharacterController2D controller;
    private TextMeshPro inspect;
    [SerializeField]
    private Vector2 velocity;
    [SerializeField]
    private Vector2 input;

    private SpriteRenderer sprite;

    private IInteractable currentInteractable;

    private Wobbler wobbler;

    public GameObject InteractTextObj;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController2D>();
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        wobbler = gameObject.GetComponent<Wobbler>();
        inspect = gameObject.GetComponentInChildren<TextMeshPro>();

        InteractTextObj.SetActive(false);

        gameObject.transform.position = DataHolder.playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();

        if(input == Vector2.zero)
        {
            wobbler.ShouldWobble = false;
            wobbler.ShouldSquashStretch = false;
        }
        else
        {
            wobbler.ShouldWobble = true;
            wobbler.ShouldSquashStretch = true;
        }


        if(Input.GetKeyDown(KeyCode.Space) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }



    void FixedUpdate()
    {
        velocity.x = Mathf.Lerp(velocity.x, input.x * moveSpeedMax, acceleration * Time.deltaTime);
        velocity.y = Mathf.Lerp(velocity.y, input.y * moveSpeedMax, acceleration * Time.deltaTime);
        controller.move(velocity * Time.deltaTime);
        velocity = controller.velocity;//grab velocity post movement calcs
    }


    public void SetInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;
        InteractTextObj.SetActive(true);
        inspect.text = interactable.InteractionText;
    }
    public void UnsetInteractable(IInteractable interactable) 
    {
        if (currentInteractable == interactable)
        {
            currentInteractable = null;
            InteractTextObj.SetActive(false);
        }
    }
}
