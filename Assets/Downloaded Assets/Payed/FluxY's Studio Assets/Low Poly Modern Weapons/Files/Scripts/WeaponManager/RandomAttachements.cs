using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttachements : MonoBehaviour
{
    public GameObject[] sightsList;
    public GameObject[] barrelsList;
    public GameObject[] gripsList;
    public GameObject[] magazinesList;
    public GameObject[] othersList;

    int rdm_s;
    int rdm_b;
    int rdm_g;
    int rdm_o;
    int rdm_m;

    private void OnEnable()
    {
        randomAttachements();
    }

    public void randomAttachements()
    {
        rdm_s = Random.Range(0, sightsList.Length);
        rdm_b = Random.Range(0, barrelsList.Length);
        rdm_g = Random.Range(0, gripsList.Length);
        rdm_o = Random.Range(0, othersList.Length);
        rdm_m = Random.Range(0, magazinesList.Length);

        for (int s = 0; s < sightsList.Length; s++)
        {
            if (s == rdm_s)
                sightsList[rdm_s].SetActive(true);
            else
                sightsList[s].SetActive(false);
        }

        for (int b = 0; b < barrelsList.Length; b++)
        {
            if (b == rdm_b)
                barrelsList[rdm_b].SetActive(true);
            else
                barrelsList[b].SetActive(false);
        }

        for (int g = 0; g < gripsList.Length; g++)
        {
            if (g == rdm_g)
                gripsList[rdm_g].SetActive(true);
            else
                gripsList[g].SetActive(false);
        }

        for (int o = 0; o < othersList.Length; o++)
        {
            if (o == rdm_o)
                othersList[rdm_o].SetActive(true);
            else
                othersList[o].SetActive(false);
        }

        for (int m = 0; m < magazinesList.Length; m++)
        {
            if (m == rdm_m)
                magazinesList[rdm_m].SetActive(true);
            else
                magazinesList[m].SetActive(false);
        }
    }
}
