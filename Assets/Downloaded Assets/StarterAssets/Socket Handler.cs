using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketHandler : MonoBehaviour
{
    public GameObject parent;
    public GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        child.transform.parent = parent.transform;
    }

    private void Update()
    {
        
    }
}
