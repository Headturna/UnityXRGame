using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public GameObject[] weapons;
    public float turnSpeed = 5;
    private int index;

    void Start()
    {
        weapons = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in weapons)
            go.SetActive(false);

        if (weapons[0])
            weapons[0].SetActive(true);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
    }

    public void WeaponLeft()
    {
        weapons[index].SetActive(false);

        index--;
        if (index < 0)
            index = weapons.Length - 1;

        weapons[index].SetActive(true);
    }

    public void WeaponRight()
    {
        weapons[index].SetActive(false);

        index++;
        if (index == weapons.Length)
            index = 0;

        weapons[index].SetActive(true);
    }

    public void RandomAttachements()
    {
        if(weapons[index].GetComponent<RandomAttachements>())
            weapons[index].GetComponent<RandomAttachements>().randomAttachements();
    }
}
