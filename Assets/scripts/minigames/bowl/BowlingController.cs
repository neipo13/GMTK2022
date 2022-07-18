using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BowlingController : MonoBehaviour
{
    Rigidbody2D ball;
    public Vector2 currentPos;

    public float forceMultiplier = 100f;
    public float spriteSwitchVelocity = 0.2f;
    bool atRest;

    Vector2 previousPos;

    float randomRotation => Random.Range(0, 4) * 90f;

    float currentRotation;

    public float rollingFrameChangeTime = 0.2f;
    float rollingFrameChangeTimer;

    List<BallPositionData> recentPositions = new List<BallPositionData>();
    // Start is called before the first frame update

    SpriteRenderer renderer;
    public Sprite[] rollingSprites;
    public Sprite[] restSprites;
    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        previousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ball.position = new Vector2(currentPos.x, currentPos.y);

        recentPositions.Add(new BallPositionData() { position = currentPos, previousPosition = previousPos });
        if (recentPositions.Count > 5) recentPositions.RemoveAt(0);
        previousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        Vector2 diff = recentPositions.Select(x => x.position - x.previousPosition).OrderBy(x => x.sqrMagnitude).Last();
        ball.AddForce(diff * forceMultiplier);
    }

    private void Update()
    {
        if(!atRest && ball.velocity.sqrMagnitude < spriteSwitchVelocity)
        {
            atRest = true;
            renderer.sprite = restSprites[UnityEngine.Random.Range(0, restSprites.Length)];
            currentRotation = randomRotation;
            rollingFrameChangeTimer = 0f;
        }
        else if (ball.velocity.sqrMagnitude > spriteSwitchVelocity)
        {
            atRest = false;
            rollingFrameChangeTimer += Time.deltaTime;
            if(rollingFrameChangeTimer > rollingFrameChangeTime)
            {
                rollingFrameChangeTimer = 0f; //reset the timer
                renderer.sprite = rollingSprites[UnityEngine.Random.Range(0, rollingSprites.Length)];
                currentRotation = randomRotation;
            }
        }
    }

    private void LateUpdate()
    {
        renderer.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
    }

    private struct BallPositionData
    {
        public Vector2 position;
        public Vector2 previousPosition;
    }
}
