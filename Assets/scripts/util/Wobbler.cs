using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Wobbler : MonoBehaviour
{
    public GameObject obj;
    [SerializeField]
    private SpringDataVec2 squashStretchSpring;
    [SerializeField]
    private SpringData wobbleSpring;

    public Vector3 squashedScale = new Vector3(1.5f, 0.8f, 1f);
    public Vector3 stretchedScale = new Vector3(0.7f, 1.2f, 1f);
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);

    public bool ShouldSquashStretch = false;
    public bool ShouldWobble = false;

    private bool Squashing = true;
    private float squashTimer = 0f;
    public float SquashStretchInterval = 0.3f;

    public float normalRotation = 0f;
    public float rightRotation = 1f;
    public float leftRotation = -1f;

    private bool WobblingRight = true;
    private float wobbleTimer = 0f;
    public float WobbleInterval = 0.3f;


    private void Start()
    {
        squashStretchSpring.goal = normalScale;
        squashStretchSpring.current = normalScale;

        wobbleSpring.current = normalRotation;
        wobbleSpring.goal = normalRotation;
    }

    public void LateUpdate()
    {
        UpdateSquashStretch();
        UpdateWobble();
    }

    public void UpdateWobble()
    {
        if (!ShouldWobble)
        {
            wobbleSpring.goal = normalRotation;
            wobbleTimer = 0f;
        }
        else
        {
            wobbleTimer += Time.deltaTime;
            if (wobbleTimer > WobbleInterval)
            {
                wobbleTimer = 0f;
                WobblingRight = !WobblingRight;
            }
            if (WobblingRight)
            {
                wobbleSpring.goal = rightRotation;
            }
            else
            {
                wobbleSpring.goal = leftRotation;
            }
        }


        //wobbleSpring.current = obj.transform.rotation.eulerAngles.z;
        wobbleSpring.Update(Time.deltaTime);
        obj.transform.eulerAngles = new Vector3(0f, 0f, wobbleSpring.current - normalRotation);
    }

    public void UpdateSquashStretch()
    {
        if (!ShouldSquashStretch)
        {
            squashStretchSpring.goal = normalScale;
            Squashing = true;
            squashTimer = 0f;
        }
        else
        {
            squashTimer += Time.deltaTime;
            if (squashTimer > SquashStretchInterval)
            {
                squashTimer = 0f;
                Squashing = !Squashing;
            }
            if (Squashing)
            {
                squashStretchSpring.goal = squashedScale;
            }
            else
            {
                squashStretchSpring.goal = stretchedScale;
            }
        }


        squashStretchSpring.current = obj.transform.localScale;
        squashStretchSpring.Update(Time.deltaTime);
        obj.transform.localScale = new Vector3(squashStretchSpring.current.x, squashStretchSpring.current.y, 1f);
    }
}
