using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour {

    public float speed;

    Vector3 previousPos = Vector3.zero;
    Vector3 posDelta = Vector3.zero;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            posDelta = Input.mousePosition - previousPos;

            if (Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                transform.Rotate(transform.up, -Vector3.Dot(posDelta, -Camera.main.transform.right) * speed, Space.World);
            }
            else
            {
                transform.Rotate(transform.up, Vector3.Dot(posDelta, -Camera.main.transform.right) * speed, Space.World);
            }

            transform.Rotate(Camera.main.transform.right, Vector3.Dot(posDelta, Camera.main.transform.up) * speed, Space.World);

        }

        previousPos = Input.mousePosition;
    }
}
