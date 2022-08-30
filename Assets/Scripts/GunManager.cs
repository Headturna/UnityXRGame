using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GunManager : MonoBehaviour
{
    public XRSocketInteractorWithTagCheck magSocket;
    public GameObject slide;
    public XROffsetGrabInteractable slideGrabInteractable;
    public ConfigurableJoint slideJoint;
    public Transform originalTransform;
    public Transform ejectTransform;
    public Transform chamberTransform;
    public GameObject cockedBullet;
    public MagazineManger magazineManager;
    public bool invertZAxis = false;

    public bool chambered = false;
    public bool cocked = false;

    [SerializeField] Vector3 newBulletLocalScale;

    private float connectedAnchorOriginalPos;
    private bool backReached = false;

    public void FireChamberedBullet()
    {
        GameObject newBullet = Instantiate(cockedBullet, cockedBullet.transform.position, cockedBullet.transform.rotation);
        newBullet.AddComponent<Rigidbody>();
        newBullet.AddComponent<BoxCollider>();
        newBullet.GetComponent<BulletManager>().timeToDelete = 10;
        newBullet.transform.GetChild(0).gameObject.SetActive(false);
        newBullet.transform.parent = null;

        if (newBulletLocalScale.x == 0 || newBulletLocalScale.y == 0 || newBulletLocalScale.z == 0)
            newBullet.transform.localScale = new Vector3(1f, 1f, 1f);
        else
            newBullet.transform.localScale = newBulletLocalScale;

        newBullet.GetComponent<Rigidbody>().AddExplosionForce(UnityEngine.Random.Range(150f * 0.7f, 150f), (cockedBullet.transform.position - cockedBullet.transform.right * 0.3f - cockedBullet.transform.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        newBullet.GetComponent<Rigidbody>().AddTorque(new Vector3(0, UnityEngine.Random.Range(100f, 500f), UnityEngine.Random.Range(100f, 1000f)), ForceMode.Impulse);


        chambered = false;

        cockedBullet.SetActive(false);
    }

    public void ChamberNewBullet(MagazineManger magazineManager)
    {
        if (!magazineManager.isEmpty() && !chambered)
        {
            magazineManager.RemoveBullet();
            chambered = true;
            cockedBullet.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //If slide is to be uncocked at start (locked at back position)
       //if (cocked == false)
       //{
       //    //Lock slide at back position
       //    //slideJoint.xMotion = ConfigurableJointMotion.Locked;
       //    connectedAnchorOriginalPos = slideJoint.connectedAnchor.z;
       //    slideJoint.connectedAnchor = new Vector3(slideJoint.connectedAnchor.x, slideJoint.connectedAnchor.y, -0.1f);
       //}
    }

    // Update is called once per frame
    void Update()
    {
        if(invertZAxis)
        {
            //Pull back to cocking point and slide is held
            if (slide.transform.localPosition.z <= ejectTransform.localPosition.z)
            {
                //Debug.Log("Back Position!");
                backReached = true;
                //Eject bullet if in chamber
                if (cockedBullet.activeInHierarchy && chambered)
                {
                    //Eject current bullet
                    GameObject newBullet = Instantiate(cockedBullet, cockedBullet.transform.position, cockedBullet.transform.rotation);
                    newBullet.transform.parent = null;
                    newBullet.AddComponent<Rigidbody>();
                    newBullet.AddComponent<BoxCollider>();
                    newBullet.GetComponent<BulletManager>().timeToDelete = 10;

                    if(newBulletLocalScale.x == 0 || newBulletLocalScale.y == 0 || newBulletLocalScale.z == 0)
                        newBullet.transform.localScale = new Vector3(1f, 1f, 1f);
                    else
                        newBullet.transform.localScale = newBulletLocalScale;

                    chambered = false;

                    cockedBullet.SetActive(false);
                }
            }

            //Debug.Log(slide.transform.localPosition.z + " " + chamberTransform.localPosition.z);
            //If slide had reached the point of chambering a bullet
            if (slide.transform.localPosition.z >= chamberTransform.localPosition.z && backReached)
            {
                backReached = false;
                //Debug.Log("Chambered Position!");
                //If there is a mag in the gun
                if (magSocket.interactablesSelected.Count > 0 && !chambered)
                {
                    magazineManager = magSocket.firstInteractableSelected.transform.GetComponent<MagazineManger>();

                    //If mag has bullets
                    if (!magazineManager.isEmpty())
                    {
                        //Debug.Log("Chambered!");
                        //Chamber new bullet   
                        magazineManager.RemoveBullet();
                        chambered = true;
                        cockedBullet.SetActive(true);
                    }
                }
            }
        }
        else
        {
            //Pull back to cocking point and slide is held
            if (slide.transform.localPosition.z >= ejectTransform.localPosition.z)
            {
                backReached = true;
                //Debug.Log("Back Position!");
                //Eject bullet if in chamber
                if (cockedBullet.activeInHierarchy && chambered)
                {
                    //Eject current bullet
                    GameObject newBullet = Instantiate(cockedBullet, cockedBullet.transform.position, cockedBullet.transform.rotation);
                    newBullet.transform.parent = null;
                    newBullet.AddComponent<Rigidbody>();
                    newBullet.AddComponent<BoxCollider>();
                    newBullet.GetComponent<BulletManager>().timeToDelete = 10;

                    if (newBulletLocalScale.x == 0 || newBulletLocalScale.y == 0 || newBulletLocalScale.z == 0)
                        newBullet.transform.localScale = new Vector3(1f, 1f, 1f);
                    else
                        newBullet.transform.localScale = newBulletLocalScale;

                    chambered = false;
                    cockedBullet.SetActive(false);
                }
            }

            //Debug.Log(slide.transform.localPosition.z + " " + chamberTransform.localPosition.z);
            //If slide had reached the point of chambering a bullet
            if (slide.transform.localPosition.z <= chamberTransform.localPosition.z && backReached)
            {
                backReached = false;
                //Debug.Log("Chambered Position!");
                //If there is a mag in the gun
                if (magSocket.interactablesSelected.Count > 0 && !chambered)
                {
                    magazineManager = magSocket.firstInteractableSelected.transform.GetComponent<MagazineManger>();

                    //If mag has bullets
                    if (!magazineManager.isEmpty())
                    {
                        //Debug.Log("Chambered!");
                        //Chamber new bullet   
                        magazineManager.RemoveBullet();
                        chambered = true;
                        cockedBullet.SetActive(true);
                    }
                }
            }
        }
    }
}
