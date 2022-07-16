using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlassController : MonoBehaviour
{
    Vector3 mousePrevious;
    public LayerMask clickableMask;

    public float rotationFactor = 100f;
    public float killY = 10f;

    public List<GameObject> liquidDrops;
    DataManager dataManager;
    SceneChanger sceneChanger;

    bool win;

    // Start is called before the first frame update
    void Start()
    {
        liquidDrops = GameObject.FindGameObjectsWithTag("drop").ToList();
        dataManager = FindObjectOfType<DataManager>();
        sceneChanger = FindObjectOfType<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (win) return;
        if (liquidDrops.Count < 20)
        {
            Debug.Log("WIN");
            win = true;
            dataManager.DrinkGiven();
            sceneChanger.ChangeScene("OverworldScene");
        }
        var toRemove = new List<GameObject>();
        foreach(var drop in liquidDrops)
        {
            if(drop.transform.position.y < killY)
            {
                toRemove.Add(drop);
            }
        }
        foreach(var drop in toRemove)
        {
            liquidDrops.Remove(drop);
            Destroy(drop);
        }
    }

    private void OnMouseDown()
    {
        mousePrevious = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        var mouseNew = Input.mousePosition;
        var diff = mouseNew - mousePrevious;

        var currentAngles = gameObject.transform.rotation.eulerAngles;

        gameObject.transform.rotation = Quaternion.Euler(currentAngles + new Vector3(0f, 0f, diff.sqrMagnitude / -rotationFactor));

        mousePrevious = mouseNew;
    }
}
