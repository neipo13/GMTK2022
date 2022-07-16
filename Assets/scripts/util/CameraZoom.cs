using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Utils;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    public float normalOrthoSize = 2.75f;
    public float zoomOrthoSize = 2f;

    public SpringData zoomSpring;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        zoomSpring.Update(Time.deltaTime);
        cam.m_Lens.OrthographicSize = zoomSpring.current;
    }

    public void Zoom()
    {
        zoomSpring.goal = zoomOrthoSize;
    }

    public void Normal()
    {
        zoomSpring.goal = normalOrthoSize;
    }
}
