using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float speed = 1;
    public Transform followTarget;
    public float maxFollowRange = 20;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(followTarget != null)
        {
            if(Vector3.Distance(transform.position, followTarget.transform.position) <= maxFollowRange)
            {
                transform.LookAt(followTarget);
                transform.position = Vector3.MoveTowards(transform.position, followTarget.position, speed * Time.deltaTime);
            }
        }
    }
}
