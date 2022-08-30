using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineManger : MonoBehaviour
{
    public int bulletsMax;
    public int bullets;
    public string bulletType;
    public GameObject bulletObject;

    public bool isEmpty()
    {
        return bullets <= 0;
    }

    public void RemoveBullet()
    {
        bullets--;

        if (bullets < 0)
            bullets = 0;

        if (isEmpty())
            bulletObject.SetActive(false);
    }

    public void AddBullet()
    {
        bullets++;
        if (bullets > bulletsMax)
            bullets = bulletsMax;

        bulletObject.SetActive(true);
    }

    public void Fill()
    {
        bullets = bulletsMax;
        bulletObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (bullets <= 0)
        {
            bulletObject.SetActive(false);
            bullets = 0;
        }
        else if (bullets > bulletsMax)
            bullets = bulletsMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
