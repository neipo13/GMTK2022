using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTextOnOpen : MonoBehaviour
{
    EventInteraction interaction;

    public float delay = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        if (!DataHolder.CheckEventComplete(EventFlag.OpeningScene))
        {
            StartCoroutine(delayThenPlay());
        }
    }

    IEnumerator delayThenPlay()
    {
        yield return new WaitForSeconds(delay);

        interaction = GetComponent<EventInteraction>();
        interaction.Interact();
    }
}
