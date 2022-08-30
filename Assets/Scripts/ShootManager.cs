using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ShootManager : MonoBehaviour
{
    public GunManager gunManager;
    public XRSocketInteractorWithTagCheck magSocket;
    public XROffsetGrabInteractable slideGI;
    public Transform barrelTransform;
    public LineRenderer lineRenderer;
    public MagazineManger magazineManager;
    public float range  = 100f;
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    public Animator animator;
    public PlayerLevelComponent levelComponent;

    public void Shoot()
    {
        if (slideGI.interactorsSelecting.Count <= 0)
        {
            if (magSocket != null)
            {
                if (magSocket.interactablesSelected.Count > 0)
                    magazineManager = magSocket.firstInteractableSelected.transform.GetComponent<MagazineManger>();
                else
                    magazineManager = null;
            }

            if (gunManager.chambered)
            {
                if (muzzleFlash != null)
                    muzzleFlash.Play();

                if (animator != null)
                    animator.Play("Fire", -1, 0);

                gunManager.FireChamberedBullet();

                if (magazineManager != null)
                    gunManager.ChamberNewBullet(magazineManager);

                RaycastHit raycastHit;

                if (Physics.Raycast(barrelTransform.position, barrelTransform.forward, out raycastHit, range))
                {
                    Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.gameObject.tag == "Enemy")
                    {
                        raycastHit.transform.gameObject.SetActive(false);

                        if(levelComponent != null)
                        {
                            levelComponent.AddExp(5);
                        }
                    }

                    if (hitEffect != null)
                    {
                        if(raycastHit.rigidbody != null)
                        {
                            raycastHit.rigidbody.AddForce(-raycastHit.normal * 100f);
                        }

                        GameObject hitEffectGO = Instantiate(hitEffect, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
                        Destroy(hitEffectGO, 2f);
                    }
                }
            }
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
