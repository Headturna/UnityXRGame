using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] List<ItemSpawner>itemSpawners;
    [SerializeField] string triggerTag;
    [SerializeField] Collider spawnCollider;
    [SerializeField] Collider despawnCollider;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == triggerTag)
        {
            Debug.Log("Spawn!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == triggerTag)
        {
            Debug.Log("Despawn!");
        } 
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
