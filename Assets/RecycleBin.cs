using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBin : MonoBehaviour
{
    public CraftingManager craftingManager;


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
        Craftable craftable;
        if (other.gameObject.TryGetComponent(out craftable))
        {
            Debug.Log(craftable.materials.Count);
            Debug.Log(craftable.amounts.Count);
            craftingManager.Scrap(craftable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
