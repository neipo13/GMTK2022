using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System;

public class draggable : MonoBehaviour
{
    public float yMin;
    public float yMax;
    public float offset;
    public float divsisor;
    public float stopThreshold;

    public Rigidbody2D[] slotObjects;

    public Vector3 mousePrevious;
    public bool SpunAtLeastOnce = false;

    public GameObject center;
    public string CenterItemName;

    public void Start()
    {
        slotObjects = slotObjects.OrderBy(x => Guid.NewGuid()).ToArray();

        for (int i = 0; i < slotObjects.Length; i++)
        {
            var slotItem = slotObjects[i];
            slotItem.transform.position = new Vector3(slotItem.transform.position.x, yMin + (offset * i), slotItem.transform.position.z);
        }
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < slotObjects.Length; i++)
        {
            var slotitem = slotObjects[i];
            if (slotitem.transform.position.y > yMax)
            {
                slotitem.transform.position = new Vector3(slotitem.transform.position.x, yMin, slotitem.transform.position.z);
            }
            else if (slotitem.transform.position.y < yMin)
            {
                slotitem.transform.position = new Vector3(slotitem.transform.position.x, yMax, slotitem.transform.position.z);
            }
        }

        if (slotObjects[0].velocity.sqrMagnitude < 2f)
        {
            Stop();
        }
    }
    public void OnMouseDrag()
    {
        SpunAtLeastOnce = true;
        var mouseNew = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var diff = new Vector3(0f, ((mouseNew - mousePrevious).y / divsisor) / Time.deltaTime, 0f);

        for (int i = 0; i < slotObjects.Length; i++)
        {
            var slotItem = slotObjects[i];
            slotItem.AddForce(diff, ForceMode2D.Force);
        }
    }

    public void Stop()
    {
        //pick the item closest to the middle and snap it to the grid, space the remainder out
        var minDist = float.MaxValue;
        var snapIndex = -1;
        for (int i = 0; i < slotObjects.Length; i++)
        {
            var slotItem = slotObjects[i];
            slotItem.velocity = Vector2.zero;
            var dist = Math.Abs(slotItem.transform.position.y - center.transform.position.y);
            if (dist < minDist)
            {
                minDist = dist;
                snapIndex = i;
            }
        }
        slotObjects[snapIndex].position = center.transform.position;
        for (int i = 0; i < snapIndex; i++)
        {
            //should be above the center
            var slotItem = slotObjects[i];
            var positionY = center.transform.position.y - ((snapIndex - i) * offset);
            if (positionY < yMin)
            {
                var diff = yMin - positionY;
                positionY = yMax - diff;
            }
            slotItem.transform.position = new Vector3(slotItem.transform.position.x, positionY, slotItem.transform.position.z);
        }
        for (int i = snapIndex + 1; i < slotObjects.Length; i++)
        {
            //should be above the center
            var slotItem = slotObjects[i];
            var positionY = center.transform.position.y + ((i - snapIndex) * offset);
            if (positionY > yMax)
            {
                var diff = positionY - yMax;
                positionY = yMin + diff;
            }
            slotItem.transform.position = new Vector3(slotItem.transform.position.x, positionY, slotItem.transform.position.z);
        }

        if(SpunAtLeastOnce) CenterItemName = slotObjects[snapIndex].name;
    }
}
