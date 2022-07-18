using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ButtonJuice : MonoBehaviour
{

    public SpringDataVec2 buttonScaleSpring;

    public Vector2 regularScale;
    public Vector2 hoverScale;
    public Vector2 pressedScale;

    public AudioSource audioSource;

    public AudioClip onHover;
    public AudioClip onClick;


    public void HoverEnter()
    {
        audioSource.clip = onHover;
        audioSource.volume = 1.0f;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
        buttonScaleSpring.goal = hoverScale;
    }

    public void HoverExit()
    {
        buttonScaleSpring.goal = regularScale;
    }

    public void Click()
    {
        audioSource.clip = onClick ;
        audioSource.volume = 1.0f;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
        buttonScaleSpring.goal = pressedScale;
    }


    private void Update()
    {
        buttonScaleSpring.Update(Time.deltaTime);
        transform.localScale = new Vector3(buttonScaleSpring.current.x, buttonScaleSpring.current.y, transform.localScale.z);
    }
}
