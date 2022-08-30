using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> EnemyPrefabs
        ;
    public bool randomize = false;
    public int objectsToSpawn = -1;
    public List<GameObject> spawnedObjects;
    public float spawnTime;
    public Transform distanceObject;
    public float distance;
    public bool spawnOnStart = true;
    public int layerOnSpawn = 0;
    public float despawnDistance = 100;
    public int maxSpawnedEnemies = 1;

    private bool canSpawn = true;
    private float spawnTimeStamp = 0;

    private void SpawnObjects()
    {
        if (randomize)
        {
            for (int i = 0; i < objectsToSpawn && spawnedObjects.Count < maxSpawnedEnemies; i++)
            {
                spawnedObjects.Add(Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)], transform.position, transform.rotation));
                spawnedObjects[spawnedObjects.Count - 1].GetComponent<EnemyManager>().followTarget = distanceObject.transform;
            }
        }
        else
        {
            int itemsSpawned = 0;
            for (int i = 0; i < EnemyPrefabs.Count && itemsSpawned < objectsToSpawn && spawnedObjects.Count < maxSpawnedEnemies; i++)
            {

                spawnedObjects.Add(Instantiate(EnemyPrefabs[i], transform.position, transform.rotation));
                spawnedObjects[spawnedObjects.Count - 1].GetComponent<EnemyManager>().followTarget = distanceObject.transform;
                itemsSpawned++;
            }
        }

        foreach (var item in spawnedObjects)
        {
            item.layer = layerOnSpawn; //Set to freshly Spawned;
        }
    }

    private void DestroyUnclaimedObjects()
    {
        foreach (var item in spawnedObjects)
        {
            if (item.layer == layerOnSpawn)
                Destroy(item);
        }

        spawnedObjects.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (objectsToSpawn < 0)
            objectsToSpawn = EnemyPrefabs.Count;

        SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time + " " + spawnTimeStamp);

        if (Vector3.Distance(distanceObject.transform.position, transform.position) < distance)
        {
            if (canSpawn)
            {
                if (Time.time - spawnTimeStamp >= spawnTime)
                {
                    //DestroyUnclaimedObjects();

                    SpawnObjects();

                    spawnTimeStamp = Time.time;
                }

                canSpawn = false;
            }
        }
        else
            canSpawn = true;

        foreach (var enemy in spawnedObjects)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) > despawnDistance)
            {
                Destroy(enemy);
                spawnedObjects.Remove(enemy);
                break;
            }
            else if (!enemy.activeInHierarchy)
            {
                Destroy(enemy);
                spawnedObjects.Remove(enemy);
                break;
            }
        }
        
    }
}
