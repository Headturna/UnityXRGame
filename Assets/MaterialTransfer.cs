using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTransfer : MonoBehaviour
{
    [SerializeField] int layerToInteract;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.layer == layerToInteract && collision.transform.gameObject.tag != "MatBall")
            collision.transform.gameObject.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
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
