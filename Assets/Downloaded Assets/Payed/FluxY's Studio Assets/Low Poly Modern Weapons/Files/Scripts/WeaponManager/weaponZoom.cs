using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponZoom : MonoBehaviour
{
    public float zoomChangeAmount = 0.1f;
    public float zoom = 1f;
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        HandleZoom();
    }

    void HandleZoom()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomChangeAmount * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomChangeAmount * Time.deltaTime;
        }

        zoom = Mathf.Clamp(zoom, 0, 2);

        Vector3 camVector = cam.transform.position;
        camVector.z = zoom;
        cam.transform.position = camVector;
    }
}
