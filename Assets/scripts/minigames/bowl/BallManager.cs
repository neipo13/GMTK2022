using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject BallPrefab;
    public GameObject BallInstance;
    public Transform ballSpawnLocation;

    bool blockDestroyed = false;

    bool won = false;
    DataManager dataManager;
    SceneChanger changer;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        changer = FindObjectOfType<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (won) return;

        if(BallInstance == null)
        {
            BallInstance = Instantiate(BallPrefab, ballSpawnLocation.transform.position, Quaternion.identity);
        }

        if (blockDestroyed)
        {
            blockDestroyed = false;
            var blocks = GameObject.FindGameObjectsWithTag("drop");
            if (blocks.Length == 0)
            {
                won = true;
                Debug.Log("win");
                dataManager.BlocksKnocked();
                changer.ChangeScene("OverworldScene");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.StartsWith("Ball"))
        {
            BallInstance = null;
            Destroy(collision.gameObject);
        }
        else if (collision.name.StartsWith("Block"))
        {
            blockDestroyed = true;
            Destroy(collision.gameObject);
        }
    }
}
