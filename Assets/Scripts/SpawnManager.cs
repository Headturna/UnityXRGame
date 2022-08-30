using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> objects;
    public bool randomize = false;
    // Start is called before the first frame update
    void Start()
    {
        if(randomize)
        {
            foreach (var obj in objects)
            {
                int rand = Random.Range(0, 2);

                if(rand > 0)
                    obj.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
