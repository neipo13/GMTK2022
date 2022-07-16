using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BowlingController : MonoBehaviour
{
    Rigidbody2D ball;
    public Vector2 currentPos;

    public float forceMultiplier = 100f;

    Vector2 previousPos;

    List<BallPositionData> recentPositions = new List<BallPositionData>();
    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
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

    private struct BallPositionData
    {
        public Vector2 position;
        public Vector2 previousPosition;
    }
}
