using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    private bool orgIsKinematic = false;
    Rigidbody rb = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent = gameObject.transform;

        if (other.gameObject.transform.TryGetComponent<Rigidbody>(out rb))
        {
            orgIsKinematic = rb.isKinematic;
            rb.isKinematic = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.parent = null;
        rb.isKinematic = orgIsKinematic;
    }
}
