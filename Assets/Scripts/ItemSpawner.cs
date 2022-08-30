using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public float spawnChance = 50;
    public List<GameObject> HighChanceObjects;
    public List<GameObject> MediumChanceObjects;
    public List<GameObject> LowChanceObjects;
    public bool randomize = false;
    public int objectsToSpawn = -1;
    public List<GameObject> spawnedObjects;
    //public float spawnTime;
    //public Transform distanceObject;
    //public float distance;
    //public bool spawnOnStart = true;
    public int layerOnSpawn = 0;
    public string spawnColliderTag;
    public string despawnColliderTag;

    private bool canSpawn = true;
    //private float spawnTimeStamp = 0;

    private void SpawnObjects()
    {
        int dice = Random.Range(0, 101);

        if (dice < spawnChance)
        {
            if (randomize)
            {
                for (int i = 0; i < objectsToSpawn; i++)
                {
                    dice = Random.Range(0, 101);

                    if (dice >= 0 && dice <= 50) //High chance
                    {
                        spawnedObjects.Add(Instantiate(HighChanceObjects[Random.Range(0, HighChanceObjects.Count)], transform.position, transform.rotation));
                    }
                    else if (dice > 50 && dice <= 90) //Medium chance
                    {
                        spawnedObjects.Add(Instantiate(MediumChanceObjects[Random.Range(0, MediumChanceObjects.Count)], transform.position, transform.rotation));
                    }
                    else //Low chance
                    {
                        spawnedObjects.Add(Instantiate(LowChanceObjects[Random.Range(0, LowChanceObjects.Count)], transform.position, transform.rotation));
                    }
                }
            }
            else
            {
                int itemsSpawned = 0;
                for (int i = 0; i < HighChanceObjects.Count && itemsSpawned < objectsToSpawn; i++)
                {
                    spawnedObjects.Add(Instantiate(HighChanceObjects[i], transform.position, transform.rotation));
                    itemsSpawned++;
                }
                for (int i = 0; i < MediumChanceObjects.Count && itemsSpawned < objectsToSpawn; i++)
                {
                    spawnedObjects.Add(Instantiate(MediumChanceObjects[i], transform.position, transform.rotation));
                    itemsSpawned++;
                }
                for (int i = 0; i < LowChanceObjects.Count && itemsSpawned < objectsToSpawn; i++)
                {
                    spawnedObjects.Add(Instantiate(LowChanceObjects[i], transform.position, transform.rotation));
                    itemsSpawned++;
                }
            }

            foreach (var item in spawnedObjects)
            {
                item.layer = layerOnSpawn; //Set to freshly Spawned;
            }
        }

        canSpawn = false;
    }

    private void DestroyUnclaimedObjects()
    {
        foreach (var item in spawnedObjects)
        {
            if (item.layer == layerOnSpawn)
                Destroy(item);
        }

        spawnedObjects.Clear();

        canSpawn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(objectsToSpawn < 0)
            objectsToSpawn = HighChanceObjects.Count + MediumChanceObjects.Count + LowChanceObjects.Count;

        //SpawnObjects();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == spawnColliderTag && canSpawn)
        {
            SpawnObjects();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == despawnColliderTag && !canSpawn)
        {
            DestroyUnclaimedObjects();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time + " " + spawnTimeStamp);

        //if (Vector3.Distance(distanceObject.transform.position, transform.position) < distance)
        //{
        //    if (canSpawn)
        //    {
        //        if (Time.time - spawnTimeStamp >= spawnTime)
        //        {
        //            DestroyUnclaimedObjects();
        //
        //            SpawnObjects();
        //
        //            spawnTimeStamp = Time.time;
        //        }
        //
        //        canSpawn = false;
        //    }
        //}
        //else
        //    canSpawn = true;
    }
}
