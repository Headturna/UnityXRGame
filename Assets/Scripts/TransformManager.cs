using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoBehaviour
{
    public Transform thisTransform;

    public void setRotationX(float x)
    {
        thisTransform.localRotation.Set(x, thisTransform.localRotation.y, thisTransform.localRotation.z, thisTransform.localRotation.w);
        Debug.Log("On Activate!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
