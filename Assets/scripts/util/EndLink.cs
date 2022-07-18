using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLink : MonoBehaviour
{
    bool ending = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ending) return;
        if(collision.name == "Player")
        {
            ending = true;
            var changer = FindObjectOfType<SceneChanger>();
            changer.ChangeScene("EndScene");
        }
    }
}
