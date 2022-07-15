using Prime31;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerController : MonoBehaviour
{
    public float moveSpeedMax = 5f;
    public float acceleration = 20f;
    private CharacterController2D controller;
    [SerializeField]
    private Vector2 velocity;
    [SerializeField]
    private Vector2 input;

    private SpriteRenderer sprite;
    [SerializeField]
    private SpringDataVec2 squashStretchSpring;

    private IInteractable currentInteractable;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController2D>();
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();

        squashStretchSpring = new SpringDataVec2();
        squashStretchSpring.goal = new Vector2(1f, 1f);
        squashStretchSpring.damping = 0.6f;
        squashStretchSpring.frequency = 10f;
        squashStretchSpring.current = new Vector2(1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();

        squashStretchSpring.current = sprite.gameObject.transform.localScale;
        squashStretchSpring.Update(Time.deltaTime);
        sprite.gameObject.transform.localScale = new Vector3(squashStretchSpring.current.x, squashStretchSpring.current.y, 1f);


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
    }
    public void UnsetInteractable() 
    { 
        currentInteractable = null;
    }
}
