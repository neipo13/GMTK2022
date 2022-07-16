using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappear : MonoBehaviour
{
    public EventFlag disappearEvent;
    // Start is called before the first frame update
    void Start()
    {
        if (DataHolder.CheckEventComplete(disappearEvent))
        {
            gameObject.SetActive(false);
        }
    }

}
