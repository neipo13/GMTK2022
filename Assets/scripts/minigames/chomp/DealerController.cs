using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerController : MonoBehaviour
{
    public float maxDist = 1f;

    public float sinTime = 0.7f;

    public SpriteRenderer sprite;

    public Transform spot;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(Mathf.Sin(Time.time * sinTime) * maxDist, 0f, 0f);

        sprite.flipX = gameObject.transform.position.x < 0f;

        if (sprite.flipX && spot.localPosition.x > 0f)
        {
            spot.localPosition = new Vector3(spot.localPosition.x * -1f, spot.localPosition.y, spot.localPosition.z);
        }
        else if (!sprite.flipX && spot.localPosition.x < 0f)
        {
            spot.localPosition = new Vector3(spot.localPosition.x * -1f, spot.localPosition.y, spot.localPosition.z);
        }

    }
}
