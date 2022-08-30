using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCuttingManager : MonoBehaviour
{
    public Collider Terrain;
    public GameObject woodPiecePrefab;
    public List<GameObject> woodPieces;
    public PlayerLevelComponent levelComponent;
    public int expToGive = 1;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.name);
        levelComponent.exp += expToGive;
       //if (woodPieces.Count < 10)
       //    woodPieces.Add(Instantiate(woodPiecePrefab, transform.position, transform.rotation));
    }

    // Start is called before the first frame update
    void Start()
    { 
        woodPieces = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
