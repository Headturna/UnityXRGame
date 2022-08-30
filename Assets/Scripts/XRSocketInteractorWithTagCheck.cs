using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorWithTagCheck : XRSocketInteractor
{
    public string allowedTag = string.Empty;
    public int attachmentLayerChange = -1;
    public GameObject setParentOnAttach;
    public bool directAttach = false;
    public bool canEject = false;
    public bool disableGrabOnSelect = false;
    [SerializeField] float freeLocationMaxDistance = 0f;
    [SerializeField] GameObject freeLocationParent;
    [SerializeField] XRSimpleInteractable secondGrabPoint = null;
    [SerializeField] Transform transformToSet;
    [SerializeField] Transform transformToSetOriginalParent;
    [SerializeField] string newTransformName;
    [SerializeField] string newSecondGrabPointName;
    [SerializeField] Transform secondGrabPointParent;
    [SerializeField] XRSimpleInteractableSecondGrabPoint originalSecondGrabPoint;

    private Transform socketAttachment = null;
    private Transform socketAttachmentOriginalParent = null;
    private Vector3 originalSecondGrabPointPosition;

    private Vector3 originalTransformToSetPosition;
    private Quaternion originalTransformToSetRotation;
    private Transform transformToSetLastParent;

    private Transform originalSGPParent;
    private Vector3 originalSGPPos;
    private Quaternion originalSGPRot;
    private Vector3 originalSGPOffset;
    private float originalSGPADM;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.CompareTag(allowedTag);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.CompareTag(allowedTag);
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (directAttach)
        {
            DirectAttach(args); 
        }
        base.OnHoverEntered(args);
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (attachmentLayerChange >= 0)
            args.interactableObject.transform.gameObject.layer = attachmentLayerChange;

        base.OnSelectEntering(args);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (disableGrabOnSelect)
            args.interactableObject.transform.GetComponent<XROffsetGrabInteractable>().selectDisabled = true;

        SetParent();
        SetAttachmentLayer(attachmentLayerChange);

        //Ignore collision
        
        Debug.Log(gameObject.transform.parent.gameObject.GetComponent<Collider>());
        Debug.Log(args.interactableObject.transform.gameObject.GetComponent<Collider>());

        Physics.IgnoreCollision(gameObject.transform.parent.gameObject.GetComponent<Collider>(), args.interactableObject.transform.gameObject.GetComponent<Collider>(), true);

        //if(args.interactableObject.transform.gameObject.GetComponent<XROffsetGrabInteractable>().secondGrabTransform != null)
        //{
        //    Vector3 secondGrabPosition = args.interactableObject.transform.gameObject.GetComponent<XROffsetGrabInteractable>().secondGrabTransform.localPosition;
        //    if (secondGrabPoint != null)
        //    {
        //        originalSecondGrabPointPosition = secondGrabPoint.transform.localPosition;
        //
        //        if (secondGrabPosition == null)
        //            secondGrabPosition = args.interactableObject.transform.localPosition;
        //
        //        secondGrabPoint.transform.localPosition = secondGrabPosition;
        //    }
        //}

        if (transformToSet != null && newTransformName != string.Empty)
        {
            Transform newTransform = args.interactableObject.transform.Find(newTransformName);

            if(newTransform != null)
            {
                Debug.Log(args.interactableObject.transform);
                Debug.Log(newTransform);
                Debug.Log(transformToSet);

                transformToSetLastParent = transformToSet.parent;
                originalTransformToSetPosition = transformToSet.localPosition;
                originalTransformToSetRotation = transformToSet.localRotation;

                transformToSet.parent = newTransform.transform;
                transformToSet.transform.localPosition = new Vector3(0f, 0f, 0f);
                transformToSet.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);

                //originalTransformToSet = transformToSet.position;

                //float dist = Vector3.Distance(transformToSet.position, newTransform.position);
                //Vector3 newVec = transformToSet.position.normalized * dist;
                //transformToSet.position += newVec;
            }
        }

        if (originalSecondGrabPoint != null && secondGrabPointParent != null && newSecondGrabPointName != string.Empty)
        {
            Transform child = args.interactableObject.transform.Find(newSecondGrabPointName);
            XRSimpleInteractableSecondGrabPoint sgpComponent = child.GetComponent<XRSimpleInteractableSecondGrabPoint>();

            if (sgpComponent != null)
            {
                originalSGPParent = originalSecondGrabPoint.transform.parent;
                originalSGPPos = originalSecondGrabPoint.transform.localPosition;
                originalSGPRot = originalSecondGrabPoint.transform.localRotation;
                originalSGPOffset = originalSecondGrabPoint.offset;
                originalSGPADM = originalSecondGrabPoint.activeDistanceMax;

                originalSecondGrabPoint.transform.parent = child.transform;
                originalSecondGrabPoint.transform.localPosition = new Vector3(0f, 0f, 0f);
                originalSecondGrabPoint.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
                originalSecondGrabPoint.offset = sgpComponent.offset;
                originalSecondGrabPoint.activeDistanceMax = sgpComponent.activeDistanceMax;

                //originalTransformToSet = transformToSet.position;

                //float dist = Vector3.Distance(transformToSet.position, newTransform.position);
                //Vector3 newVec = transformToSet.position.normalized * dist;
                //transformToSet.position += newVec;
            }
        }

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        if (attachmentLayerChange >= 0)
            args.interactableObject.transform.gameObject.layer = 11; //Set to ungrabbed on exit
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (disableGrabOnSelect)
            args.interactableObject.transform.GetComponent<XROffsetGrabInteractable>().selectDisabled = false;

       //if (secondGrabPoint != null)
       //{
       //    secondGrabPoint.transform.localPosition = originalSecondGrabPointPosition;
       //}

        if (transformToSet != null && newTransformName != string.Empty)
        {
            Transform newTransform = args.interactableObject.transform.Find(newTransformName);
        
            if (newTransform != null)
            {
                if (transformToSetOriginalParent != null)
                    transformToSet.parent = transformToSetOriginalParent.transform;
                else
                    transformToSet.parent = transformToSetLastParent.transform;

                transformToSet.localPosition = originalTransformToSetPosition;
                transformToSet.localRotation = originalTransformToSetRotation;
            }
        }

        if (originalSecondGrabPoint != null && secondGrabPointParent != null && newSecondGrabPointName != string.Empty)
        {
            Transform child = args.interactableObject.transform.Find(newSecondGrabPointName);
            XRSimpleInteractableSecondGrabPoint sgpComponent = child.GetComponent<XRSimpleInteractableSecondGrabPoint>(); 

            if (sgpComponent != null)
            {
                originalSecondGrabPoint.transform.parent = originalSGPParent.transform;
                originalSecondGrabPoint.transform.localPosition = originalSGPPos;
                originalSecondGrabPoint.transform.localRotation = originalSGPRot;
                originalSecondGrabPoint.offset = originalSGPOffset;
                originalSecondGrabPoint.activeDistanceMax = originalSGPADM;
            }
        }

        RemoveParent();
        //Restart collision
        Physics.IgnoreCollision(gameObject.transform.parent.gameObject.GetComponent<Collider>(), args.interactableObject.transform.gameObject.GetComponent<Collider>(), false);

        base.OnSelectExited(args);
    }

    public void DirectAttach(HoverEnterEventArgs args)
    {
        IXRInteractor interactor = args.interactableObject.GetOldestInteractorHovering();
        
        interactor.transform.gameObject.SetActive(false);
        
        args.interactableObject.transform.parent = setParentOnAttach.transform;
        
        Transform grabTransform = args.interactableObject.transform.Find("GrabTransform");
        if (grabTransform != null)
        {
            args.interactableObject.transform.position = attachTransform.transform.position + grabTransform.localPosition;
            args.interactableObject.transform.rotation = attachTransform.transform.rotation;
        }
        ////else
        ////    args.interactableObject.transform.position = attachTransform.position;
        //
        ////if(grabTransform != null)
        ////    args.interactableObject.transform.Find("GrabTransform").transform.localPosition = attachTransform.localPosition;
        ////else
        ////    args.interactableObject.transform.localPosition = attachTransform.localPosition;
        //// args.interactableObject.transform.rotation = attachTransform.rotation;
        ////args.interactableObject.transform.rotation = attachTransform.rotation;
        //
        
        interactor.transform.gameObject.SetActive(true);
    }

    public void DisableAttachmentInteractor()
    {
        //Debug.Log("Disable " + firstInteractableSelected.transform.name);

        firstInteractableSelected.transform.GetComponent<XROffsetGrabInteractable>().selectDisabled = true;
    }

    public void EnableAttachmentInteractor()
    {
        //Debug.Log("Enable " + firstInteractableSelected.transform.name);

        firstInteractableSelected.transform.GetComponent<XROffsetGrabInteractable>().selectDisabled = false;
    }

    public void SetAttachmentLayer(int layerID)
    {
        if (layerID > 0)
        {
            if(firstInteractableSelected != null)
                firstInteractableSelected.transform.gameObject.layer = layerID;
        }
    }

    public void SetParent()
    {
        if (setParentOnAttach != null)
        {
            if (firstInteractableSelected != null)
            {
                socketAttachment = firstInteractableSelected.transform;

                //Debug.Log("Socket Attachment Set!");

                if (socketAttachment.parent != null)
                {
                    socketAttachmentOriginalParent = socketAttachment.parent;

                    //Debug.Log("Original Parent Set!" + socketAttachmentOriginalParent);
                }

                socketAttachment.parent = setParentOnAttach.transform;

                //Debug.Log("Socket Attachment Parent Set!" + socketAttachment.parent);
            }
        }
    }

    public void RemoveParent()
    {
        if (setParentOnAttach != null)
        {
            if (socketAttachmentOriginalParent != null)
            {
                firstInteractableSelected.transform.parent = socketAttachmentOriginalParent;

                //Debug.Log("Socket Attachment Original Parent Reverted!" + firstInteractableSelected.transform.parent);
            }
            else
            {
                //Debug.Log("Socket Attachment Parent Set To Null!");

                firstInteractableSelected.transform.parent = null;
            }
        }
    }

    public void ForceDeselect(IXRSelectInteractable interactable)
    {
        interactionManager.CancelInteractableSelection(interactable);
    }

    public void EjectAttachment()
    {
        //Check what is happening here? Why parent? Seems wrong...
       if(canEject && firstInteractableSelected.transform.gameObject.GetComponent<XROffsetGrabInteractable>().interactorsSelecting.Count > 0)
       {
           XRBaseInteractable parentInteracable = gameObject.transform.parent.GetComponent<XROffsetGrabInteractable>();
           if(parentInteracable.firstInteractorSelecting != null)
            {
                //IXRSelectInteractable fis = firstInteractableSelected;
                RemoveParent();
                SetAttachmentLayer(11);
                EnableAttachmentInteractor();
                ForceDeselect(firstInteractableSelected);
                //fis.transform.GetComponent<Rigidbody>().AddForce(new Vector3(2000, 3000, 2440), ForceMode.Impulse);
            }
       }
    }

    public void FixedUpdate()
    {
        
    }

    public void Update()
    {
        //Allow hovered interactable to be set at any position
        if(hasHover && freeLocationMaxDistance > 0f && freeLocationParent != null)
        {
            if(freeLocationParent.GetComponent<XROffsetGrabInteractable>().isSelected)
            {
                foreach (var ih in interactablesHovered)
                {
                   if(ih.transform.GetComponent<XROffsetGrabInteractable>().isSelected)
                    {
                        Transform orgParent = ih.transform.parent;
                        ih.transform.parent = gameObject.transform;

                        if (Mathf.Abs(transform.localPosition.z - ih.transform.localPosition.z) < freeLocationMaxDistance)
                        {
                            attachTransform.localPosition = new Vector3(attachTransform.localPosition.x, attachTransform.localPosition.y, ih.transform.localPosition.z);
                        }

                        Debug.Log(gameObject.transform.parent + " Hovering!");
                        ih.transform.parent = orgParent;
                    }
                } 
            }  
        }

        //Input and setting layer on selected object
        if (firstInteractableSelected != null)
        {
            SetAttachmentLayer(attachmentLayerChange);

            var inputDevices = new List<InputDevice>();
            InputDevices.GetDevices(inputDevices);
            foreach (var device in inputDevices)
            {
                device.TryGetFeatureValue(CommonUsages.primaryButton, out bool xButtonPressed);

                if (xButtonPressed)
                {
                    EjectAttachment();
                }
            }
        }   
    }
}
