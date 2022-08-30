using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    float timeCreated;
    public float timeToDelete;

    // Start is called before the first frame update
    void Start()
    {
        timeCreated = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToDelete > 0f)
        {
            if (Time.time >= timeCreated + timeToDelete)
                Destroy(gameObject);
        }
    }
}
