using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        var player = FindObjectOfType<PlayerController>();
        if (player != null)
            DataHolder.playerPosition = player.gameObject.transform.position;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
